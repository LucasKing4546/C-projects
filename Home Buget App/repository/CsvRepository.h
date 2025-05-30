//
// Created by Lucas on 5/26/2025.
//

#ifndef CSVREPOSITORY_H
#define CSVREPOSITORY_H
#pragma once
#include "BaseRepository.h"
#include <QString>

class CsvRepository : public BaseRepository {
public:
    explicit CsvRepository(const QString& filename);

    bool save(const std::vector<BudgetItem>& items) override;
    std::vector<BudgetItem> load() override;
    bool isValid() const override;

private:
    QString m_filename;
};
#endif //CSVREPOSITORY_H
