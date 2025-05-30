//
// Created by Lucas on 5/29/2025.
//
#pragma once

#include <QMainWindow>
#include <QTableWidget>
#include <QHeaderView>
#include <QTableWidgetItem>
#include <QPushButton>
#include <QComboBox>
#include <QDateEdit>
#include <QDoubleSpinBox>
#include <memory>

#include "../repository/CsvRepository.h"
#include "../controller/BudgetController.h"
#include "../models/BudgetItem.h"

class MainWindow : public QMainWindow {
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = nullptr);
    ~MainWindow() = default;

    private slots:
        void onAddClicked();
    void onRemoveClicked();
    void onUpdateClicked();
    void onUndoClicked();
    void onApplyFilterClicked();
    void onClearFilterClicked();
    void refreshTable();

private:
    void populateTable(const std::vector<BudgetItem>& items);

    std::unique_ptr<BudgetController> m_controller;
    QTableWidget*   m_table;
    QPushButton*    m_addBtn;
    QPushButton*    m_updateBtn;
    QPushButton*    m_removeBtn;
    QPushButton*    m_undoBtn;

    // filter controls
    QComboBox*      m_typeFilter;
    QComboBox*      m_categoryFilter;
    QDateEdit*      m_fromDate;
    QDateEdit*      m_toDate;
    QDoubleSpinBox* m_minAmount;
    QDoubleSpinBox* m_maxAmount;
    QPushButton*    m_applyFilterBtn;
    QPushButton*    m_clearFilterBtn;
};
