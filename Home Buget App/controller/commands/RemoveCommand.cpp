//
// Created by Lucas on 5/27/2025.
//
#include "RemoveCommand.h"
#include <algorithm>

RemoveCommand::RemoveCommand(std::vector<BudgetItem>& items, int itemId)
    : m_items(items), m_itemId(itemId), m_removedIndex(0), m_executed(false) {}

void RemoveCommand::execute() {
    if (!m_executed) {
        auto it = std::find_if(m_items.begin(), m_items.end(),
                               [this](const BudgetItem& item) {
                                   return item.getId() == m_itemId;
                               });
        if (it != m_items.end()) {
            m_removedItem = *it;
            m_removedIndex = std::distance(m_items.begin(), it);
            m_items.erase(it);
            m_executed = true;
        }
    }
}

void RemoveCommand::undo() {
    if (m_executed) {
        m_items.insert(m_items.begin() + m_removedIndex, m_removedItem);
        m_executed = false;
    }
}