//
// Created by Lucas on 5/27/2025.
//
#include "AddCommand.h"
#include <algorithm>

AddCommand::AddCommand(std::vector<BudgetItem>& items, const BudgetItem& item)
    : m_items(items), m_item(item), m_executed(false) {}

void AddCommand::execute() {
    if (!m_executed) {
        m_items.push_back(m_item);
        m_executed = true;
    }
}

void AddCommand::undo() {
    if (m_executed) {
        auto it = std::find_if(m_items.begin(), m_items.end(),
                               [this](const BudgetItem& item) {
                                   return item.getId() == m_item.getId();
                               });
        if (it != m_items.end()) {
            m_items.erase(it);
        }
        m_executed = false;
    }
}