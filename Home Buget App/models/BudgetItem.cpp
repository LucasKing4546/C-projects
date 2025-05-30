//
// Created by Lucas on 5/26/2025.
//
#include "BudgetItem.h"
#include <QStringList>
#include <QJsonDocument>
BudgetItem::BudgetItem()

    : m_id(0), m_amount(0.0), m_type(TransactionType::EXPENSE), m_category(Category::OTHER) {}

BudgetItem::BudgetItem(int id, const QString& description, double amount,
                       const QDate& date, TransactionType type, Category category) {
    m_id = id;
    m_description = description;
    m_amount = amount;
    m_date = date;
    m_type = type;
    m_category = category;
}

QString BudgetItem::typeToString(TransactionType type) {
    switch (type) {
        case TransactionType::EXPENSE: return "EXPENSE";
        case TransactionType::INCOME: return "INCOME";
        default: return "EXPENSE";
    }
}

TransactionType BudgetItem::stringToType(const QString& str) {
    if (str == "EXPENSE") {
        return TransactionType::EXPENSE;
    }
    return TransactionType::INCOME;
}

QString BudgetItem::categoryToString(Category category) {  // Added BudgetItem:: scope
    switch (category) {
        case Category::OTHER: return "OTHER";
        case Category::FOOD: return "FOOD";
        case Category::TRANSPORTATION: return "TRANSPORTATION";
        case Category::HOUSING: return "HOUSING";
        case Category::ENTERTAINMENT: return "ENTERTAINMENT";
        case Category::HEALTHCARE: return "HEALTHCARE";
        case Category::UTILITIES: return "UTILITIES";
        case Category::SHOPPING: return "SHOPPING";
        case Category::EDUCATION: return "EDUCATION";
        case Category::SAVINGS: return "SAVINGS";
        case Category::INCOME_SALARY: return "INCOME_SALARY";
        case Category::INCOME_OTHER: return "INCOME_OTHER";
        default: return "OTHER";
    }
}

Category BudgetItem::stringToCategory(const QString& str) {
    if (str == "OTHER") return Category::OTHER;
    if (str == "FOOD") return Category::FOOD;
    if (str == "TRANSPORTATION") return Category::TRANSPORTATION;
    if (str == "HOUSING") return Category::HOUSING;
    if (str == "ENTERTAINMENT") return Category::ENTERTAINMENT;
    if (str == "HEALTHCARE") return Category::HEALTHCARE;
    if (str == "UTILITIES") return Category::UTILITIES;
    if (str == "SHOPPING") return Category::SHOPPING;
    if (str == "EDUCATION") return Category::EDUCATION;
    if (str == "SAVINGS") return Category::SAVINGS;
    if (str == "INCOME_SALARY") return Category::INCOME_SALARY;
    if (str == "INCOME_OTHER") return Category::INCOME_OTHER;
    return Category::OTHER;
}

QString BudgetItem::toCsvString() const {
    return QString("%1,%2,%3,%4,%5,%6")
        .arg(QString::number(m_id))
        .arg(m_description)
        .arg(QString::number(m_amount, 'f', 2))
        .arg(m_date.toString("yyyy-MM-dd"))
        .arg(typeToString(m_type))
        .arg(categoryToString(m_category));
}

BudgetItem BudgetItem::fromCsvString(const QString& csvLine) {
    QStringList parts = csvLine.split(',');
    if (parts.size() < 6) {
        throw std::invalid_argument("Invalid CSV line format");
    }

    int id = parts[0].toInt();
    const QString& description = parts[1];
    double amount = parts[2].toDouble();
    QDate date = QDate::fromString(parts[3], "yyyy-MM-dd");
    TransactionType type = stringToType(parts[4]);
    Category category = stringToCategory(parts[5]);

    return BudgetItem(id, description, amount, date, type, category);
}

QJsonObject BudgetItem::toJson() const {
    QJsonObject obj;
    obj["id"] = m_id;
    obj["description"] = m_description;
    obj["amount"] = m_amount;
    obj["date"] = m_date.toString(Qt::ISODate);
    obj["type"] = typeToString(m_type);
    obj["category"] = categoryToString(m_category);
    return obj;
}

BudgetItem BudgetItem::fromJson(const QJsonObject& json) {
    if (!json.contains("id") || !json.contains("description") || !json.contains("amount") ||
        !json.contains("date") || !json.contains("type") || !json.contains("category")) {
        throw std::invalid_argument("Invalid JSON object");
    }

    int id = json["id"].toInt();
    QString description = json["description"].toString();
    double amount = json["amount"].toDouble();
    QDate date = QDate::fromString(json["date"].toString(), Qt::ISODate);
    TransactionType type = stringToType(json["type"].toString());
    Category category = stringToCategory(json["category"].toString());

    return BudgetItem(id, description, amount, date, type, category);
}