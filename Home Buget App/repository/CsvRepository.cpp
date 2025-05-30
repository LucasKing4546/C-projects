//
// Created by Lucas on 5/26/2025.
//
#include "CsvRepository.h"
#include <QFile>
#include <QTextStream>
#include <QDir>
#include <QFileInfo>

CsvRepository::CsvRepository(const QString& filename) : m_filename(filename) {}

bool CsvRepository::save(const std::vector<BudgetItem>& items) {
    QFile file(m_filename);
    if (!file.open(QIODevice::WriteOnly | QIODevice::Text)) {
        return false;
    }

    QTextStream out(&file);
    out << "id,description,amount,date,type,category\n";

    for (const auto& item : items) {
        out << item.toCsvString() << "\n";
    }

    return true;
}

std::vector<BudgetItem> CsvRepository::load() {
    std::vector<BudgetItem> items;
    QFile file(m_filename);

    if (!file.open(QIODevice::ReadOnly | QIODevice::Text)) {
        return items;
    }

    QTextStream in(&file);
    QString line = in.readLine(); // Skip header

    while (!in.atEnd()) {
        line = in.readLine();
        if (!line.isEmpty()) {
            BudgetItem item = BudgetItem::fromCsvString(line);
            if (item.getId() != 0) {
                items.push_back(item);
            }
        }
    }

    return items;
}

bool CsvRepository::isValid() const {
    QFile file(m_filename);
    return file.exists() || QDir().mkpath(QFileInfo(file).absolutePath());
}