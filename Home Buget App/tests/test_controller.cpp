//
// Created by Lucas on 5/30/2025.
//
#include <gtest/gtest.h>
#include "../repository/CsvRepository.h"
#include "../repository/JsonRepository.h"
#include "../models/BudgetItem.h"
#include <QTemporaryFile>
#include <QDir>

class RepositoryTest : public ::testing::Test {
protected:
    void SetUp() override {
        // Create test data
        testItems.emplace_back(1, "Test Income", 1000.0, QDate(2025, 1, 15), TransactionType::INCOME, Category::INCOME_SALARY);
        testItems.emplace_back(2, "Test Expense", 50.0, QDate(2025, 1, 16), TransactionType::EXPENSE, Category::FOOD);
        testItems.emplace_back(3, "Another Expense", 25.5, QDate(2025, 1, 17), TransactionType::EXPENSE, Category::ENTERTAINMENT);
    }

    std::vector<BudgetItem> testItems;
};

// CSV Repository Tests
TEST_F(RepositoryTest, CsvRepository_SaveAndLoad) {
    QTemporaryFile tempFile;
    ASSERT_TRUE(tempFile.open());
    QString filename = tempFile.fileName();
    tempFile.close();

    CsvRepository repo(filename);

    // Test save
    EXPECT_TRUE(repo.save(testItems));

    // Test load
    auto loadedItems = repo.load();
    EXPECT_EQ(loadedItems.size(), testItems.size());

    // Verify first item
    EXPECT_EQ(loadedItems[0].getId(), testItems[0].getId());
    EXPECT_EQ(loadedItems[0].getDescription(), testItems[0].getDescription());
    EXPECT_DOUBLE_EQ(loadedItems[0].getAmount(), testItems[0].getAmount());
    EXPECT_EQ(loadedItems[0].getType(), testItems[0].getType());
    EXPECT_EQ(loadedItems[0].getCategory(), testItems[0].getCategory());
}

TEST_F(RepositoryTest, CsvRepository_IsValid) {
    QTemporaryFile tempFile;
    ASSERT_TRUE(tempFile.open());
    QString filename = tempFile.fileName();

    CsvRepository repo(filename);
    EXPECT_TRUE(repo.isValid());
}

TEST_F(RepositoryTest, CsvRepository_LoadEmpty) {
    QTemporaryFile tempFile;
    ASSERT_TRUE(tempFile.open());
    QString filename = tempFile.fileName();
    tempFile.close();

    CsvRepository repo(filename);
    auto items = repo.load();
    EXPECT_TRUE(items.empty());
}

// JSON Repository Tests
TEST_F(RepositoryTest, JsonRepository_SaveAndLoad) {
    QTemporaryFile tempFile;
    ASSERT_TRUE(tempFile.open());
    QString filename = tempFile.fileName();
    tempFile.close();

    JsonRepository repo(filename);

    // Test save
    EXPECT_TRUE(repo.save(testItems));

    // Test load
    auto loadedItems = repo.load();
    EXPECT_EQ(loadedItems.size(), testItems.size());

    // Verify first item
    EXPECT_EQ(loadedItems[0].getId(), testItems[0].getId());
    EXPECT_EQ(loadedItems[0].getDescription(), testItems[0].getDescription());
    EXPECT_DOUBLE_EQ(loadedItems[0].getAmount(), testItems[0].getAmount());
    EXPECT_EQ(loadedItems[0].getType(), testItems[0].getType());
    EXPECT_EQ(loadedItems[0].getCategory(), testItems[0].getCategory());
}

TEST_F(RepositoryTest, JsonRepository_IsValid) {
    QTemporaryFile tempFile;
    ASSERT_TRUE(tempFile.open());
    QString filename = tempFile.fileName();

    JsonRepository repo(filename);
    EXPECT_TRUE(repo.isValid());
}

TEST_F(RepositoryTest, JsonRepository_LoadEmpty) {
    QTemporaryFile tempFile;
    ASSERT_TRUE(tempFile.open());
    QString filename = tempFile.fileName();
    tempFile.close();

    JsonRepository repo(filename);
    auto items = repo.load();
    EXPECT_TRUE(items.empty());
}