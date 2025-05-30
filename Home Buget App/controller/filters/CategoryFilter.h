//
// Created by Lucas on 5/28/2025.
//
#ifndef CATEGORYFILTER_H
#define CATEGORYFILTER_H
#pragma once
#include "FilterStrategy.h"

class CategoryFilter : public FilterStrategy {
public:
    explicit CategoryFilter(Category category);
    std::vector<BudgetItem> filter(const std::vector<BudgetItem>& items) const override;

private:
    Category m_category;
};
#endif //CATEGORYFILTER_H
