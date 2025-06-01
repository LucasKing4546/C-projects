//
// Created by Lucas on 5/26/2025.
//

#ifndef BUGETITEM_H
#define BUGETITEM_H
#pragma once
#include <QString>
#include <QDate>
#include <QJsonObject>

enum class TransactionType {
    INCOME,
    EXPENSE
};

enum class Category {
    FOOD,
    TRANSPORTATION,
    HOUSING,
    ENTERTAINMENT,
    HEALTHCARE,
    UTILITIES,
    SHOPPING,
    EDUCATION,
    SAVINGS,
    INCOME_SALARY,
    INCOME_OTHER,
    OTHER
};

class BudgetItem {
public:
    BudgetItem();
    BudgetItem(int id, const QString& description, double amount,
               const QDate& date, TransactionType type, Category category);

    // Getters
    int getId() const { return m_id; }
    QString getDescription() const { return m_description; }
    double getAmount() const { return m_amount; }
    QDate getDate() const { return m_date; }
    TransactionType getType() const { return m_type; }
    Category getCategory() const { return m_category; }

    // Setters
    void setId(int id) { m_id = id; }
    void setDescription(const QString& description) { m_description = description; }
    void setAmount(double amount) { m_amount = amount; }
    void setDate(const QDate& date) { m_date = date; }
    void setType(TransactionType type) { m_type = type; }
    void setCategory(Category category) { m_category = category; }

    // Serialization
    QString toCsvString() const;
    static BudgetItem fromCsvString(const QString& csvLine);
    QJsonObject toJson() const;
    static BudgetItem fromJson(const QJsonObject& json);

    // Utility methods
    static QString typeToString(TransactionType type);
    static TransactionType stringToType(const QString& str);
    static QString categoryToString(Category category);
    static Category stringToCategory(const QString& str);

private:
    int m_id;
    QString m_description;
    double m_amount;
    QDate m_date;
    TransactionType m_type;
    Category m_category;
};
#endif //BUGETITEM_H