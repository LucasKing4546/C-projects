//
// Created by Lucas on 5/28/2025.
//

#ifndef COMPOSITEFILTER_H
#define COMPOSITEFILTER_H
#pragma once
#include "FilterStrategy.h"
#include <memory>
#include <vector>

enum class LogicalOperator {
    AND,
    OR
};

class CompositeFilter : public FilterStrategy {
public:
    CompositeFilter(LogicalOperator op);
    void addFilter(std::unique_ptr<FilterStrategy> filter);
    std::vector<BudgetItem> filter(const std::vector<BudgetItem>& items) const override;

private:
    LogicalOperator m_operator;
    std::vector<std::unique_ptr<FilterStrategy>> m_filters;
};
#endif //COMPOSITEFILTER_H
