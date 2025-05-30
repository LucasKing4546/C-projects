//
// Created by Lucas on 5/26/2025.
//
#include "JsonRepository.h"
#include <QJsonDocument>
#include <QJsonArray>
#include <QJsonObject>
#include <QDir>
#include <QFileInfo>

JsonRepository::JsonRepository(const QString& filename) : m_filename(filename) {}

bool JsonRepository::save(const std::vector<BudgetItem>& items) {
    QJsonArray jsonArray;

    for (const auto& item : items) {
        jsonArray.append(item.toJson());
    }

    QJsonObject root;
    root["budget_items"] = jsonArray;

    QJsonDocument doc(root);

    QFile file(m_filename);
    if (!file.open(QIODevice::WriteOnly)) {
        return false;
    }

    file.write(doc.toJson());
    return true;
}

std::vector<BudgetItem> JsonRepository::load() {
    std::vector<BudgetItem> items;
    QFile file(m_filename);

    if (!file.open(QIODevice::ReadOnly)) {
        return items;
    }

    QByteArray data = file.readAll();
    QJsonDocument doc = QJsonDocument::fromJson(data);

    if (!doc.isObject()) {
        return items;
    }

    QJsonObject root = doc.object();
    QJsonArray jsonArray = root["budget_items"].toArray();

    for (const auto& value : jsonArray) {
        if (value.isObject()) {
            BudgetItem item = BudgetItem::fromJson(value.toObject());
            items.push_back(item);
        }
    }

    return items;
}

bool JsonRepository::isValid() const {
    QFile file(m_filename);
    return file.exists() || QDir().mkpath(QFileInfo(file).absolutePath());
}

