//
// Created by Lucas on 5/28/2025.
//

#ifndef BUDGETCONTROLLER_H
#define BUDGETCONTROLLER_H
#pragma once
#include "../models/BudgetItem.h"
#include "../repository/BaseRepository.h"
#include "commands/Command.h"
#include "filters/FilterStrategy.h"
#include <memory>
#include <vector>
#include <stack>
#include <QObject>

class BudgetController : public QObject {
    Q_OBJECT

public:
    explicit BudgetController(std::unique_ptr<BaseRepository> repository, QObject* parent = nullptr);

    // CRUD operations
    bool addItem(const BudgetItem& item);
    bool removeItem(int itemId);
    bool updateItem(int itemId, const BudgetItem& newItem);

    // Query operations
    std::vector<BudgetItem> getAllItems() const;
    std::vector<BudgetItem> getFilteredItems(const FilterStrategy& filter) const;
    BudgetItem getItemById(int id) const;

    // Undo/Redo operations
    bool undo();
    bool redo();
    bool canUndo() const;
    bool canRedo() const;

    // Persistence
    bool save();
    bool load();

    // Statistics
    double getTotalIncome() const;
    double getTotalExpenses() const;
    double getBalance() const;

    signals:
    void dataChanged();
    void itemAdded(const BudgetItem& item);
    void itemRemoved(int itemId);
    void itemUpdated(const BudgetItem& item);

private:
    void executeCommand(std::unique_ptr<Command> command);
    int getNextId() const;

    std::unique_ptr<BaseRepository> m_repository;
    std::vector<BudgetItem> m_items;
    std::stack<std::unique_ptr<Command>> m_undoStack;
    std::stack<std::unique_ptr<Command>> m_redoStack;
};
#endif //BUDGETCONTROLLER_H
