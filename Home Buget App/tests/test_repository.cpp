//
// Created by Lucas on 5/30/2025.
//
#include <gtest/gtest.h>
#include "../controller/BudgetController.h"
#include "../repository/CsvRepository.h"
#include "../controller/filters/CategoryFilter.h"
#include "../controller/filters/AmountFilter.h"
#include "../controller/filters/DateFilter.h"
#include "../controller/filters/TypeFilter.h"
#include <QTemporaryFile>
#include <memory>

class BudgetControllerTest : public ::testing::Test {
protected:
    void SetUp() override {
        // Create temporary file for repository
        tempFile = std::make_unique<QTemporaryFile>();
        ASSERT_TRUE(tempFile->open());
        filename = tempFile->fileName();
        tempFile->close();

        // Create controller with CSV repository
        auto repo = std::make_unique<CsvRepository>(filename);
        controller = std::make_unique<BudgetController>(std::move(repo));

        // Create test items:
        //   - testItem1: INCOME  (Salary)
        //   - testItem2: EXPENSE (Groceries)
        //   - testItem3: EXPENSE (Movie)
        testItem1 = BudgetItem(0, "Salary",    3000.0, QDate(2025, 1, 1), TransactionType::INCOME,    Category::INCOME_SALARY);
        testItem2 = BudgetItem(0, "Groceries", 150.0,  QDate(2025, 1, 2), TransactionType::EXPENSE,   Category::FOOD);
        testItem3 = BudgetItem(0, "Movie",     25.0,   QDate(2025, 1, 3), TransactionType::EXPENSE,   Category::ENTERTAINMENT);
    }

    std::unique_ptr<QTemporaryFile> tempFile;
    QString filename;
    std::unique_ptr<BudgetController> controller;
    BudgetItem testItem1, testItem2, testItem3;
};

// Basic CRUD Operations
TEST_F(BudgetControllerTest, AddItem) {
    EXPECT_TRUE(controller->addItem(testItem1));

    auto items = controller->getAllItems();
    EXPECT_EQ(items.size(), 1);
    EXPECT_EQ(items[0].getDescription(), "Salary");
    EXPECT_EQ(items[0].getId(), 1); // Should auto-assign ID
}

TEST_F(BudgetControllerTest, AddMultipleItems) {
    EXPECT_TRUE(controller->addItem(testItem1));
    EXPECT_TRUE(controller->addItem(testItem2));
    EXPECT_TRUE(controller->addItem(testItem3));

    auto items = controller->getAllItems();
    EXPECT_EQ(items.size(), 3);

    // Check IDs are assigned correctly
    EXPECT_EQ(items[0].getId(), 1);
    EXPECT_EQ(items[1].getId(), 2);
    EXPECT_EQ(items[2].getId(), 3);
}

TEST_F(BudgetControllerTest, RemoveItem) {
    controller->addItem(testItem1);
    controller->addItem(testItem2);

    EXPECT_TRUE(controller->removeItem(1));

    auto items = controller->getAllItems();
    EXPECT_EQ(items.size(), 1);
    EXPECT_EQ(items[0].getDescription(), "Groceries");
}

TEST_F(BudgetControllerTest, RemoveNonExistentItem) {
    EXPECT_FALSE(controller->removeItem(999));
}

TEST_F(BudgetControllerTest, UpdateItem) {
    controller->addItem(testItem1);

    BudgetItem updatedItem(0, "Updated Salary", 3500.0, QDate(2025, 1, 1), TransactionType::INCOME, Category::INCOME_SALARY);

    EXPECT_TRUE(controller->updateItem(1, updatedItem));

    auto item = controller->getItemById(1);
    EXPECT_EQ(item.getDescription(), "Updated Salary");
    EXPECT_DOUBLE_EQ(item.getAmount(), 3500.0);
}

TEST_F(BudgetControllerTest, UpdateNonExistentItem) {
    BudgetItem updatedItem(0, "Test", 100.0, QDate(2025, 1, 1), TransactionType::INCOME, Category::INCOME_SALARY);
    EXPECT_FALSE(controller->updateItem(999, updatedItem));
}

TEST_F(BudgetControllerTest, GetItemById) {
    controller->addItem(testItem1);

    auto item = controller->getItemById(1);
    EXPECT_EQ(item.getDescription(), "Salary");
    EXPECT_DOUBLE_EQ(item.getAmount(), 3000.0);
}

TEST_F(BudgetControllerTest, GetNonExistentItemById) {
    auto item = controller->getItemById(999);
    EXPECT_EQ(item.getId(), 0); // Default constructed item
}

// Undo/Redo Operations
TEST_F(BudgetControllerTest, UndoAddItem) {
    controller->addItem(testItem1);
    EXPECT_EQ(controller->getAllItems().size(), 1);

    EXPECT_TRUE(controller->undo());
    EXPECT_EQ(controller->getAllItems().size(), 0);
}

TEST_F(BudgetControllerTest, UndoRemoveItem) {
    controller->addItem(testItem1);
    controller->removeItem(1);
    EXPECT_EQ(controller->getAllItems().size(), 0);

    EXPECT_TRUE(controller->undo());
    EXPECT_EQ(controller->getAllItems().size(), 1);
}

TEST_F(BudgetControllerTest, RedoOperation) {
    controller->addItem(testItem1);
    controller->undo();
    EXPECT_EQ(controller->getAllItems().size(), 0);

    EXPECT_TRUE(controller->redo());
    EXPECT_EQ(controller->getAllItems().size(), 1);
}

TEST_F(BudgetControllerTest, CanUndoRedo) {
    EXPECT_FALSE(controller->canUndo());
    EXPECT_FALSE(controller->canRedo());

    controller->addItem(testItem1);
    EXPECT_TRUE(controller->canUndo());
    EXPECT_FALSE(controller->canRedo());

    controller->undo();
    EXPECT_FALSE(controller->canUndo());
    EXPECT_TRUE(controller->canRedo());
}

// Statistics
TEST_F(BudgetControllerTest, GetTotalIncome) {
    controller->addItem(testItem1); // 3000.0 income
    controller->addItem(testItem2); // 150.0 expense

    EXPECT_DOUBLE_EQ(controller->getTotalIncome(), 3000.0);
}

TEST_F(BudgetControllerTest, GetTotalExpenses) {
    controller->addItem(testItem1); // 3000.0 income
    controller->addItem(testItem2); // 150.0 expense
    controller->addItem(testItem3); // 25.0 expense

    EXPECT_DOUBLE_EQ(controller->getTotalExpenses(), 175.0);
}

TEST_F(BudgetControllerTest, GetBalance) {
    controller->addItem(testItem1); // 3000.0 income
    controller->addItem(testItem2); // 150.0 expense
    controller->addItem(testItem3); // 25.0 expense

    EXPECT_DOUBLE_EQ(controller->getBalance(), 2825.0); // 3000 - 175
}

// Filter Tests
TEST_F(BudgetControllerTest, CategoryFilter) {
    controller->addItem(testItem1); // SALARY
    controller->addItem(testItem2); // FOOD
    controller->addItem(testItem3); // ENTERTAINMENT

    CategoryFilter filter(Category::FOOD);
    auto filtered = controller->getFilteredItems(filter);

    EXPECT_EQ(filtered.size(), 1);
    EXPECT_EQ(filtered[0].getDescription(), "Groceries");
}

TEST_F(BudgetControllerTest, AmountFilter) {
    controller->addItem(testItem1); // 3000.0
    controller->addItem(testItem2); // 150.0
    controller->addItem(testItem3); // 25.0

    AmountFilter filter(AmountComparison::GREATER_THAN, 100.0);
    auto filtered = controller->getFilteredItems(filter);

    EXPECT_EQ(filtered.size(), 2); // Salary and Groceries
}

TEST_F(BudgetControllerTest, DateFilter) {
    controller->addItem(testItem1); // 2025-01-01
    controller->addItem(testItem2); // 2025-01-02
    controller->addItem(testItem3); // 2025-01-03

    DateFilter filter(DateComparison::ON, QDate(2025, 1, 2));
    auto filtered = controller->getFilteredItems(filter);

    EXPECT_EQ(filtered.size(), 1);
    EXPECT_EQ(filtered[0].getDescription(), "Groceries");
}

// NEW: TypeFilter Test (filters by TransactionType)
TEST_F(BudgetControllerTest, TypeFilter) {
    controller->addItem(testItem1); // INCOME
    controller->addItem(testItem2); // EXPENSE
    controller->addItem(testItem3); // EXPENSE

    // Keep only EXPENSE items
    TypeFilter filter(TransactionType::EXPENSE);
    auto filtered = controller->getFilteredItems(filter);

    // Should return exactly the two expense items: "Groceries" and "Movie"
    EXPECT_EQ(filtered.size(), 2);
    EXPECT_EQ(filtered[0].getDescription(), "Groceries");
    EXPECT_EQ(filtered[1].getDescription(), "Movie");
}

// Persistence
TEST_F(BudgetControllerTest, SaveAndLoad) {
    controller->addItem(testItem1);
    controller->addItem(testItem2);

    EXPECT_TRUE(controller->save());

    // Create new controller with the same repository
    auto repo = std::make_unique<CsvRepository>(filename);
    auto newController = std::make_unique<BudgetController>(std::move(repo));

    auto items = newController->getAllItems();
    EXPECT_EQ(items.size(), 2);
}
