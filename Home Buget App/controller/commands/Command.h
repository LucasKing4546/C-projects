//
// Created by Lucas on 5/27/2025.
//

#ifndef COMMAND_H
#define COMMAND_H
#pragma once
class Command {
public:
    virtual ~Command() = default;
    virtual void execute() = 0;
    virtual void undo() = 0;
};
#endif //COMMAND_H
