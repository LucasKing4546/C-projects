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
#include <QFileDialog>
#include <QMessageBox>
#include <QRadioButton>
#include <QButtonGroup>
#include <QGroupBox>
#include <memory>

#include "../repository/CsvRepository.h"
#include "../repository/JsonRepository.h"
#include "../controller/BudgetController.h"
#include "../models/BudgetItem.h"

// Add filter strategy includes
#include "../controller/filters/FilterStrategy.h"
#include "../controller/filters/CompositeFilter.h"
#include "../controller/filters/CategoryFilter.h"
#include "../controller/filters/DateFilter.h"
#include "../controller/filters/AmountFilter.h"
#include "../controller/filters/TypeFilter.h"

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
    void onSaveClicked();
    void onLoadClicked();
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
    QPushButton*    m_saveBtn;
    QPushButton*    m_loadBtn;

    // filter controls
    QComboBox*      m_typeFilter;
    QComboBox*      m_categoryFilter;
    QDateEdit*      m_fromDate;
    QDateEdit*      m_toDate;
    QDoubleSpinBox* m_minAmount;
    QDoubleSpinBox* m_maxAmount;
    QPushButton*    m_applyFilterBtn;
    QPushButton*    m_clearFilterBtn;

    // AND/OR logic controls
    QRadioButton*   m_andRadio;
    QRadioButton*   m_orRadio;
    QButtonGroup*   m_logicGroup;
    QGroupBox*      m_logicGroupBox;
};