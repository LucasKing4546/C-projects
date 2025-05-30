//
// Created by Lucas on 5/27/2025.
//

#ifndef ADDCOMMAND_H
#define ADDCOMMAND_H
#pragma once
#include "Command.h"
#include "../../models/BudgetItem.h"
#include <vector>

class AddCommand : public Command {
public:
    AddCommand(std::vector<BudgetItem>& items, const BudgetItem& item);

    void execute() override;
    void undo() override;

private:
    std::vector<BudgetItem>& m_items;
    BudgetItem m_item;
    bool m_executed;
};
#endif //ADDCOMMAND_H
