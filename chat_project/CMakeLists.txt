cmake_minimum_required(VERSION 3.30)
project(ipc_chat_demo)

set(CMAKE_CXX_STANDARD 20)

add_executable(chat_project pipe_chat.c)
