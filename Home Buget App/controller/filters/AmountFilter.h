//
// Created by Lucas on 5/28/2025.
//

#ifndef AMOUNTFILTER_H
#define AMOUNTFILTER_H
#pragma once
#include "FilterStrategy.h"

enum class AmountComparison {
    LESS_THAN,
    GREATER_THAN,
    EQUAL_TO,
    BETWEEN
};

class AmountFilter : public FilterStrategy {
public:
    AmountFilter(AmountComparison comparison, double value1, double value2 = 0.0);
    std::vector<BudgetItem> filter(const std::vector<BudgetItem>& items) const override;

private:
    AmountComparison m_comparison;
    double m_value1;
    double m_value2;
};
#endif //AMOUNTFILTER_H
