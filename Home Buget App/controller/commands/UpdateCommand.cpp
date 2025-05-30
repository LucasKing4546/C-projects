//
// Created by Lucas on 5/27/2025.
//
#include "UpdateCommand.h"
#include <algorithm>

UpdateCommand::UpdateCommand(std::vector<BudgetItem>& items, int itemId, const BudgetItem& newItem)
    : m_items(items), m_itemId(itemId), m_newItem(newItem), m_executed(false) {}

void UpdateCommand::execute() {
    if (!m_executed) {
        auto it = std::find_if(m_items.begin(), m_items.end(),
                               [this](const BudgetItem& item) {
                                   return item.getId() == m_itemId;
                               });
        if (it != m_items.end()) {
            m_oldItem = *it;
            *it = m_newItem;
            m_executed = true;
        }
    }
}

void UpdateCommand::undo() {
    if (m_executed) {
        auto it = std::find_if(m_items.begin(), m_items.end(),
                               [this](const BudgetItem& item) {
                                   return item.getId() == m_newItem.getId();
                               });
        if (it != m_items.end()) {
            *it = m_oldItem;
        }
        m_executed = false;
    }
}