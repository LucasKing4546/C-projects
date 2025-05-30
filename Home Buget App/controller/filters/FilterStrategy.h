//
// Created by Lucas on 5/27/2025.
//

#ifndef FILTERSTRATEGY_H
#define FILTERSTRATEGY_H
#pragma once
#include "../../models/BudgetItem.h"
#include <vector>

class FilterStrategy {
public:
    virtual ~FilterStrategy() = default;
    virtual std::vector<BudgetItem> filter(const std::vector<BudgetItem>& items) const = 0;
};
#endif //FILTERSTRATEGY_H
