//
// Created by Lucas on 5/27/2025.
//

#ifndef REMOVECOMMAND_H
#define REMOVECOMMAND_H
#pragma once
#include "Command.h"
#include "../../models/BudgetItem.h"
#include <vector>

class RemoveCommand : public Command {
public:
    RemoveCommand(std::vector<BudgetItem>& items, int itemId);

    void execute() override;
    void undo() override;

private:
    std::vector<BudgetItem>& m_items;
    int m_itemId;
    BudgetItem m_removedItem;
    size_t m_removedIndex;
    bool m_executed;
};
#endif //REMOVECOMMAND_H
