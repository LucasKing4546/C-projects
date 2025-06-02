//
// Created by Lucas on 5/28/2025.
//
#include "CompositeFilter.h"
#include <algorithm>
#include <set>

CompositeFilter::CompositeFilter(LogicalOperator op) : m_operator(op) {}

void CompositeFilter::addFilter(std::unique_ptr<FilterStrategy> filter) {
    m_filters.push_back(std::move(filter));
}

std::vector<BudgetItem> CompositeFilter::filter(const std::vector<BudgetItem>& items) const {
    if (m_filters.empty()) {
        return items;
    }

    if (m_operator == LogicalOperator::AND) {
        std::vector<BudgetItem> result = items;
        for (const auto& filter : m_filters) {
            result = filter->filter(result);
        }
        return result;
    } else { // OR
        std::set<int> resultIds;
        std::vector<BudgetItem> result;

        for (const auto& filter : m_filters) {
            auto filtered = filter->filter(items);
            for (const auto& item : filtered) {
                if (resultIds.find(item.getId()) == resultIds.end()) {
                    resultIds.insert(item.getId());
                    result.push_back(item);
                }
            }
        }
        return result;
    }
}
