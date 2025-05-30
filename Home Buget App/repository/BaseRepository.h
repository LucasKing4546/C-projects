//
// Created by Lucas on 5/26/2025.
//

#ifndef BASEREPOSITORY_H
#define BASEREPOSITORY_H
#pragma once
#include <vector>
#include "../models/BudgetItem.h"

class BaseRepository {
public:
  virtual ~BaseRepository() = default;

  virtual bool save(const std::vector<BudgetItem>& items) = 0;
  virtual std::vector<BudgetItem> load() = 0;
  virtual bool isValid() const = 0;
};
#endif //BASEREPOSITORY_H
