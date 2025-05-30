//
// Created by Lucas on 5/26/2025.
//

#ifndef JSONREPOSITORY_H
#define JSONREPOSITORY_H
#include "BaseRepository.h"
#include <QString>

class JsonRepository : public BaseRepository {
public:
    explicit JsonRepository(const QString& filename);

    bool save(const std::vector<BudgetItem>& items) override;
    std::vector<BudgetItem> load() override;
    bool isValid() const override;

private:
    QString m_filename;
};
#endif //JSONREPOSITORY_H
