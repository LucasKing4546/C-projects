//
// Created by Lucas on 5/28/2025.
//
#include "CategoryFilter.h"
#include <algorithm>

CategoryFilter::CategoryFilter(Category category) : m_category(category) {}

std::vector<BudgetItem> CategoryFilter::filter(const std::vector<BudgetItem>& items) const {
    std::vector<BudgetItem> filtered;
    std::copy_if(items.begin(), items.end(), std::back_inserter(filtered),
                 [this](const BudgetItem& item) {
                     return item.getCategory() == m_category;
                 });
    return filtered;
}