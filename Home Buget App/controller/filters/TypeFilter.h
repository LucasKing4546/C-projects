//
// Created by Lucas on 5/28/2025.
//

#ifndef TYPEFILTER_H
#define TYPEFILTER_H
#pragma once
#include "FilterStrategy.h"

class TypeFilter : public FilterStrategy {
public:
    explicit TypeFilter(TransactionType type);
    std::vector<BudgetItem> filter(const std::vector<BudgetItem>& items) const override;

private:
    TransactionType m_type;
};
#endif //TYPEFILTER_H