//
// Created by Lucas on 5/28/2025.
//
#include "TypeFilter.h"
#include <algorithm>

TypeFilter::TypeFilter(TransactionType type) : m_type(type) {}

std::vector<BudgetItem> TypeFilter::filter(const std::vector<BudgetItem>& items) const {
    std::vector<BudgetItem> filtered;
    std::copy_if(items.begin(), items.end(), std::back_inserter(filtered),
                 [this](const BudgetItem& item) {
                     return item.getType() == m_type;
                 });
    return filtered;
}