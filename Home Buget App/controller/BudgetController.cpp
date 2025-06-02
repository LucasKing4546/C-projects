//
// Created by Lucas on 5/28/2025.
//

#include "BudgetController.h"
#include "commands/AddCommand.h"
#include "commands/RemoveCommand.h"
#include "commands/UpdateCommand.h"
#include <algorithm>

BudgetController::BudgetController(std::unique_ptr<BaseRepository> repository, QObject* parent)
    : QObject(parent), m_repository(std::move(repository)) {
    load();
}

bool BudgetController::addItem(const BudgetItem& item) {
    BudgetItem newItem = item;
    newItem.setId(getNextId());

    auto command = std::make_unique<AddCommand>(m_items, newItem);
    executeCommand(std::move(command));

    emit itemAdded(newItem);
    emit dataChanged();
    return true;
}

bool BudgetController::removeItem(int itemId) {
    auto it = std::find_if(m_items.begin(), m_items.end(),
                           [itemId](const BudgetItem& item) {
                               return item.getId() == itemId;
                           });

    if (it == m_items.end()) {
        return false;
    }

    auto command = std::make_unique<RemoveCommand>(m_items, itemId);
    executeCommand(std::move(command));

    emit itemRemoved(itemId);
    emit dataChanged();
    return true;
}

bool BudgetController::updateItem(int itemId, const BudgetItem& newItem) {
    auto it = std::find_if(m_items.begin(), m_items.end(),
                           [itemId](const BudgetItem& item) {
                               return item.getId() == itemId;
                           });

    if (it == m_items.end()) {
        return false;
    }

    BudgetItem updatedItem = newItem;
    updatedItem.setId(itemId);

    auto command = std::make_unique<UpdateCommand>(m_items, itemId, updatedItem);
    executeCommand(std::move(command));

    emit itemUpdated(updatedItem);
    emit dataChanged();
    return true;
}

std::vector<BudgetItem> BudgetController::getAllItems() const {
    return m_items;
}

std::vector<BudgetItem> BudgetController::getFilteredItems(const FilterStrategy& filter) const {
    return filter.filter(m_items);
}

BudgetItem BudgetController::getItemById(int id) const {
    auto it = std::find_if(m_items.begin(), m_items.end(),
                           [id](const BudgetItem& item) {
                               return item.getId() == id;
                           });

    if (it != m_items.end()) {
        return *it;
    }

    return BudgetItem(); // Return default constructed item if not found
}

bool BudgetController::undo() {
    if (m_undoStack.empty()) {
        return false;
    }

    auto command = std::move(m_undoStack.top());
    m_undoStack.pop();

    command->undo();
    m_redoStack.push(std::move(command));

    emit dataChanged();
    return true;
}

bool BudgetController::redo() {
    if (m_redoStack.empty()) {
        return false;
    }

    auto command = std::move(m_redoStack.top());
    m_redoStack.pop();

    command->execute();
    m_undoStack.push(std::move(command));

    emit dataChanged();
    return true;
}

bool BudgetController::canUndo() const {
    return !m_undoStack.empty();
}

bool BudgetController::canRedo() const {
    return !m_redoStack.empty();
}

bool BudgetController::save() {
    if (!m_repository || !m_repository->isValid()) {
        return false;
    }

    return m_repository->save(m_items);
}

bool BudgetController::load() {
    if (!m_repository || !m_repository->isValid()) {
        return false;
    }

    m_items = m_repository->load();
    emit dataChanged();
    return true;
}

double BudgetController::getTotalIncome() const {
    double total = 0.0;
    for (const auto& item : m_items) {
        if (item.getType() == TransactionType::INCOME) {
            total += item.getAmount();
        }
    }
    return total;
}

double BudgetController::getTotalExpenses() const {
    double total = 0.0;
    for (const auto& item : m_items) {
        if (item.getType() == TransactionType::EXPENSE) {
            total += item.getAmount();
        }
    }
    return total;
}

double BudgetController::getBalance() const {
    return getTotalIncome() - getTotalExpenses();
}

void BudgetController::executeCommand(std::unique_ptr<Command> command) {
    command->execute();

    // Clear redo stack when a new command is executed
    while (!m_redoStack.empty()) {
        m_redoStack.pop();
    }

    m_undoStack.push(std::move(command));
}

int BudgetController::getNextId() const {
    int maxId = 0;
    for (const auto& item : m_items) {
        if (item.getId() > maxId) {
            maxId = item.getId();
        }
    }
    return maxId + 1;
}