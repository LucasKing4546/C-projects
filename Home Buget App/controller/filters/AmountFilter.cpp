//
// Created by Lucas on 5/28/2025.
//
#include "AmountFilter.h"
#include <algorithm>

AmountFilter::AmountFilter(AmountComparison comparison, double value1, double value2)
    : m_comparison(comparison), m_value1(value1), m_value2(value2) {}

std::vector<BudgetItem> AmountFilter::filter(const std::vector<BudgetItem>& items) const {
    std::vector<BudgetItem> filtered;

    std::copy_if(items.begin(), items.end(), std::back_inserter(filtered),
                 [this](const BudgetItem& item) {
                     double amount = item.getAmount();
                     switch (m_comparison) {
                         case AmountComparison::LESS_THAN:
                             return amount < m_value1;
                         case AmountComparison::GREATER_THAN:
                             return amount > m_value1;
                         case AmountComparison::EQUAL_TO:
                             return qAbs(amount - m_value1) < 0.01;
                         case AmountComparison::BETWEEN:
                             return amount >= m_value1 && amount <= m_value2;
                         default:
                             return true;
                     }
                 });

    return filtered;
}