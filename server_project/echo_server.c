// Feature test macros - MUST be before any includes
#define GNU_SOURCE
#define POSIX_C_SOURCE 200809L
#define USE_POSIX
#define USE_POSIX2

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <fcntl.h>
#include <sys/stat.h>
#include <sys/types.h>
#include <signal.h>
#include <errno.h>

// Explicit function declaration if header doesn't provide it
#ifdef __cplusplus
extern "C" {
#endif
extern int mkfifo(const char *pathname, mode_t mode);
#ifdef __cplusplus
}
#endif

#define FIFO_REQUEST "/tmp/echo_request"
#define FIFO_RESPONSE "/tmp/echo_response"
#define BUFFER_SIZE 1024

// Global variables for cleanup
int server_running = 1;

void cleanup_handler(int sig) {
    printf("\nServer shutting down...\n");
    server_running = 0;
    unlink(FIFO_REQUEST);
    unlink(FIFO_RESPONSE);
    exit(0);
}

int main() {
    int fd_request, fd_response;
    char buffer[BUFFER_SIZE];
    ssize_t bytes_read;

    // Set up signal handler for graceful shutdown
    signal(SIGINT, cleanup_handler);
    signal(SIGTERM, cleanup_handler);

    printf("Echo Server Starting...\n");

    // Remove existing FIFOs if they exist
    unlink(FIFO_REQUEST);
    unlink(FIFO_RESPONSE);

    // Create named pipes (FIFOs)
    if (mkfifo(FIFO_REQUEST, 0666) == -1) {
        perror("mkfifo request");
        exit(1);
    }

    if (mkfifo(FIFO_RESPONSE, 0666) == -1) {
        perror("mkfifo response");
        unlink(FIFO_REQUEST);
        exit(1);
    }

    printf("Named pipes created successfully\n");
    printf("Waiting for clients...\n");

    while (server_running) {
        // Open request FIFO for reading (blocks until client connects)
        fd_request = open(FIFO_REQUEST, O_RDONLY);
        if (fd_request == -1) {
            if (errno == EINTR) continue;  // Interrupted by signal
            perror("open request FIFO");
            break;
        }

        // Open response FIFO for writing
        fd_response = open(FIFO_RESPONSE, O_WRONLY);
        if (fd_response == -1) {
            perror("open response FIFO");
            close(fd_request);
            break;
        }

        printf("Client connected!\n");

        // Communication loop with current client
        while (server_running) {
            bytes_read = read(fd_request, buffer, BUFFER_SIZE - 1);

            if (bytes_read <= 0) {
                if (bytes_read == 0) {
                    printf("Client disconnected\n");
                } else {
                    perror("read from request FIFO");
                }
                break;
            }

            buffer[bytes_read] = '\0';

            // Check for quit command
            if (strncmp(buffer, "quit", 4) == 0) {
                printf("Client sent quit command\n");
                break;
            }

            printf("Received: %s", buffer);

            // Echo the message back
            char response[BUFFER_SIZE];
            snprintf(response, BUFFER_SIZE, "Echo: %s", buffer);

            if (write(fd_response, response, strlen(response)) == -1) {
                perror("write to response FIFO");
                break;
            }
        }

        close(fd_request);
        close(fd_response);
        printf("Waiting for next client...\n");
    }

    // Cleanup
    unlink(FIFO_REQUEST);
    unlink(FIFO_RESPONSE);
    printf("Server terminated\n");

    return 0;
}