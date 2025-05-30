#define _GNU_SOURCE

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <fcntl.h>
#include <sys/stat.h>
#include <sys/types.h>

#define FIFO_REQUEST "/tmp/echo_request"
#define FIFO_RESPONSE "/tmp/echo_response"
#define BUFFER_SIZE 1024

int main() {
    int fd_request, fd_response;
    char message[BUFFER_SIZE];
    char response[BUFFER_SIZE];
    ssize_t bytes_read;

    printf("Echo Client Starting...\n");
    printf("Type 'quit' to exit\n");

    // Open request FIFO for writing
    fd_request = open(FIFO_REQUEST, O_WRONLY);
    if (fd_request == -1) {
        perror("open request FIFO");
        printf("Make sure the server is running!\n");
        exit(1);
    }

    // Open response FIFO for reading
    fd_response = open(FIFO_RESPONSE, O_RDONLY);
    if (fd_response == -1) {
        perror("open response FIFO");
        close(fd_request);
        exit(1);
    }

    printf("Connected to server!\n");
    printf("Enter messages (press Enter to send):\n");

    while (1) {
        printf("> ");
        fflush(stdout);

        // Read user input
        if (fgets(message, BUFFER_SIZE, stdin) == NULL) {
            printf("\nExiting...\n");
            break;
        }

        // Send message to server
        if (write(fd_request, message, strlen(message)) == -1) {
            perror("write to request FIFO");
            break;
        }

        // Check if user wants to quit
        if (strncmp(message, "quit", 4) == 0) {
            printf("Goodbye!\n");
            break;
        }

        // Read response from server
        bytes_read = read(fd_response, response, BUFFER_SIZE - 1);
        if (bytes_read <= 0) {
            printf("Server disconnected\n");
            break;
        }

        response[bytes_read] = '\0';
        printf("%s\n", response);
    }

    close(fd_request);
    close(fd_response);

    return 0;
}