cmake_minimum_required(VERSION 3.16)
project(ipc_server_demo C)

set(CMAKE_C_STANDARD 11)

# Add feature test macros
add_definitions(-D_GNU_SOURCE -D_POSIX_C_SOURCE=200809L)

# Echo server executable
add_executable(echo_server echo_server.c)

# Echo client executable
add_executable(echo_client echo_client.c)