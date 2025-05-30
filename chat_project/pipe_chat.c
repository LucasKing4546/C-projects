#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/wait.h>

#define BUFFER_SIZE 256

int main() {
    int pipe1[2], pipe2[2];  // Two pipes for two-way communication
    pid_t pid;
    char message[BUFFER_SIZE];

    printf("=== Simple Pipe Chat ===\n");

    // Create the pipes
    if (pipe(pipe1) == -1 || pipe(pipe2) == -1) {
        printf("Error creating pipes\n");
        exit(1);
    }

    // Fork to create two processes
    pid = fork();

    if (pid == -1) {
        printf("Error creating child process\n");
        exit(1);
    }

    if (pid == 0) {
        // CHILD PROCESS (Alice)
        close(pipe1[0]);  // Alice doesn't read from pipe1
        close(pipe2[1]);  // Alice doesn't write to pipe2

        printf("\n--- Alice's turn ---\n");
        printf("Alice: Type a message (or 'quit' to exit): ");

        while (1) {
            // Alice sends a message
            fgets(message, BUFFER_SIZE, stdin);
            message[strcspn(message, "\n")] = '\0';  // Remove newline

            if (strcmp(message, "quit") == 0) {
                write(pipe1[1], "quit", 4);
                break;
            }

            write(pipe1[1], message, strlen(message) + 1);

            // Alice waits for Bob's response
            read(pipe2[0], message, BUFFER_SIZE);

            if (strcmp(message, "quit") == 0) {
                printf("Bob left the chat.\n");
                break;
            }

            printf("Bob says: %s\n\n", message);
            printf("Alice: ");
        }

        close(pipe1[1]);
        close(pipe2[0]);

    } else {
        // PARENT PROCESS (Bob)
        close(pipe1[1]);  // Bob doesn't write to pipe1
        close(pipe2[0]);  // Bob doesn't read from pipe2

        while (1) {
            // Bob waits for Alice's message
            read(pipe1[0], message, BUFFER_SIZE);

            if (strcmp(message, "quit") == 0) {
                printf("Alice left the chat.\n");
                break;
            }

            printf("Alice says: %s\n\n", message);

            // Bob sends a response
            printf("Bob: Type a response (or 'quit' to exit): ");
            fgets(message, BUFFER_SIZE, stdin);
            message[strcspn(message, "\n")] = '\0';  // Remove newline

            if (strcmp(message, "quit") == 0) {
                write(pipe2[1], "quit", 4);
                break;
            }

            write(pipe2[1], message, strlen(message) + 1);
        }

        close(pipe1[0]);
        close(pipe2[1]);

        // Wait for child to finish
        wait(NULL);
        printf("\nChat ended.\n");
    }

    return 0;
}