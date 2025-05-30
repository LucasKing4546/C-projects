//
// Created by Lucas on 5/27/2025.
//

#ifndef UPDATECOMMAND_H
#define UPDATECOMMAND_H
#pragma once
#include "Command.h"
#include "../../models/BudgetItem.h"
#include <vector>

class UpdateCommand : public Command {
public:
    UpdateCommand(std::vector<BudgetItem>& items, int itemId, const BudgetItem& newItem);

    void execute() override;
    void undo() override;

private:
    std::vector<BudgetItem>& m_items;
    int m_itemId;
    BudgetItem m_newItem;
    BudgetItem m_oldItem;
    bool m_executed;
};
#endif //UPDATECOMMAND_H
