cmake_minimum_required(VERSION 3.16)
project(ipc_chat_demo C)

set(CMAKE_C_STANDARD 11)

# Add feature test macros
add_definitions(-D_GNU_SOURCE -D_POSIX_C_SOURCE=200809L)

# Chat executable
add_executable(pipe_chat pipe_chat.c)
