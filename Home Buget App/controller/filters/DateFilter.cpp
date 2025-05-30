//
// Created by Lucas on 5/28/2025.
//

#include "DateFilter.h"
#include <algorithm>

DateFilter::DateFilter(DateComparison comparison, const QDate& date1, const QDate& date2)
    : m_comparison(comparison), m_date1(date1), m_date2(date2) {}

std::vector<BudgetItem> DateFilter::filter(const std::vector<BudgetItem>& items) const {
    std::vector<BudgetItem> filtered;

    std::copy_if(items.begin(), items.end(), std::back_inserter(filtered),
                 [this](const BudgetItem& item) {
                     QDate itemDate = item.getDate();
                     switch (m_comparison) {
                         case DateComparison::BEFORE:
                             return itemDate < m_date1;
                         case DateComparison::AFTER:
                             return itemDate > m_date1;
                         case DateComparison::ON:
                             return itemDate == m_date1;
                         case DateComparison::BETWEEN:
                             return itemDate >= m_date1 && itemDate <= m_date2;
                         default:
                             return true;
                     }
                 });

    return filtered;
}