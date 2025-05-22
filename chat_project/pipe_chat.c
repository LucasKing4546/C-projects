//
// Created by Lucas on 5/22/2025.
//
#define _GNU_SOURCE
#define _POSIX_C_SOURCE 200809L

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/wait.h>
#include <signal.h>
#include <fcntl.h>
#include <errno.h>

#define BUFFER_SIZE 1024
#define MAX_USERNAME 32

// Global variables
pid_t child_pid = -1;

void cleanup_handler(int sig) {
    printf("\nChat application shutting down...\n");
    if (child_pid > 0) {
        kill(child_pid, SIGTERM);
        wait(NULL);
    }
    exit(0);
}

void user_process(int read_fd, int write_fd, const char* username, const char* other_user) {
    char message[BUFFER_SIZE];
    char display_buffer[BUFFER_SIZE];
    fd_set read_fds;
    int max_fd;
    ssize_t bytes_read;

    // Make stdin non-blocking
    int flags = fcntl(STDIN_FILENO, F_GETFL);
    fcntl(STDIN_FILENO, F_SETFL, flags | O_NONBLOCK);

    printf("\n=== Welcome to Pipe Chat, %s! ===\n", username);
    printf("You are chatting with %s\n", other_user);
    printf("Type your messages and press Enter to send\n");
    printf("Type 'quit' to exit\n");
    printf("=====================================\n\n");

    while (1) {
        FD_ZERO(&read_fds);
        FD_SET(STDIN_FILENO, &read_fds);
        FD_SET(read_fd, &read_fds);

        max_fd = (read_fd > STDIN_FILENO) ? read_fd : STDIN_FILENO;

        // Wait for input from either stdin or the pipe
        int activity = select(max_fd + 1, &read_fds, NULL, NULL, NULL);

        if (activity < 0) {
            if (errno == EINTR) continue;
            perror("select");
            break;
        }

        // Check for message from other user
        if (FD_ISSET(read_fd, &read_fds)) {
            bytes_read = read(read_fd, display_buffer, BUFFER_SIZE - 1);
            if (bytes_read <= 0) {
                printf("\n%s has disconnected\n", other_user);
                break;
            }

            display_buffer[bytes_read] = '\0';

            // Check for quit message
            if (strstr(display_buffer, "QUIT_SIGNAL") != NULL) {
                printf("\n%s has left the chat\n", other_user);
                break;
            }

            printf("\r%s: %s", other_user, display_buffer);
            printf("%s> ", username);
            fflush(stdout);
        }

        // Check for input from user
        if (FD_ISSET(STDIN_FILENO, &read_fds)) {
            if (fgets(message, BUFFER_SIZE, stdin) != NULL) {
                // Remove newline if present
                message[strcspn(message, "\n")] = '\0';

                if (strlen(message) == 0) continue;

                // Check for quit command
                if (strcmp(message, "quit") == 0) {
                    write(write_fd, "QUIT_SIGNAL\n", 12);
                    printf("Goodbye!\n");
                    break;
                }

                // Send message to other user
                strcat(message, "\n");
                if (write(write_fd, message, strlen(message)) == -1) {
                    perror("write");
                    break;
                }

                printf("%s> ", username);
                fflush(stdout);
            }
        }
    }
}

int main() {
    int pipe1[2], pipe2[2];  // pipe1: child->parent, pipe2: parent->child
    pid_t pid;

    // Set up signal handler
    signal(SIGINT, cleanup_handler);
    signal(SIGTERM, cleanup_handler);

    printf("Starting Pipe Chat Application...\n");

    // Create pipes
    if (pipe(pipe1) == -1) {
        perror("pipe1");
        exit(1);
    }

    if (pipe(pipe2) == -1) {
        perror("pipe2");
        close(pipe1[0]);
        close(pipe1[1]);
        exit(1);
    }

    printf("Pipes created successfully\n");

    // Fork to create child process
    pid = fork();
    child_pid = pid;

    if (pid == -1) {
        perror("fork");
        exit(1);
    }

    if (pid == 0) {
        // Child process (User 1)
        close(pipe1[0]);  // Close read end of pipe1
        close(pipe2[1]);  // Close write end of pipe2

        user_process(pipe2[0], pipe1[1], "Alice", "Bob");

        close(pipe1[1]);
        close(pipe2[0]);
        exit(0);
    } else {
        // Parent process (User 2)
        close(pipe1[1]);  // Close write end of pipe1
        close(pipe2[0]);  // Close read end of pipe2

        // Give child process time to set up
        sleep(1);

        user_process(pipe1[0], pipe2[1], "Bob", "Alice");

        close(pipe1[0]);
        close(pipe2[1]);

        // Wait for child to finish
        int status;
        wait(&status);
        printf("Chat session ended\n");
    }

    return 0;
}