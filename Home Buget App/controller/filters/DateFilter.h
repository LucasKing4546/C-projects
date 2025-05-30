//
// Created by Lucas on 5/28/2025.
//

#ifndef DATEFILTER_H
#define DATEFILTER_H
#pragma once
#include "FilterStrategy.h"
#include <QDate>

enum class DateComparison {
    BEFORE,
    AFTER,
    ON,
    BETWEEN
};

class DateFilter : public FilterStrategy {
public:
    DateFilter(DateComparison comparison, const QDate& date1, const QDate& date2 = QDate());
    std::vector<BudgetItem> filter(const std::vector<BudgetItem>& items) const override;

private:
    DateComparison m_comparison;
    QDate m_date1;
    QDate m_date2;
};
#endif //DATEFILTER_H
