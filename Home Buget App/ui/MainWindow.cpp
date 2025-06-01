//
// Created by Lucas on 5/29/2025.
//
#include "MainWindow.h"

#include <QVBoxLayout>
#include <QHBoxLayout>
#include <QFormLayout>
#include <QInputDialog>
#include <QMessageBox>
#include <QLineEdit>
#include <QDate>
#include <QFileDialog>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , m_table(new QTableWidget(this))
    , m_addBtn(new QPushButton("Add Item", this))
    , m_updateBtn(new QPushButton("Update Selected", this))
    , m_removeBtn(new QPushButton("Remove Selected", this))
    , m_undoBtn(new QPushButton("Undo", this))
    , m_saveBtn(new QPushButton("Save", this))
    , m_loadBtn(new QPushButton("Load", this))
    , m_typeFilter(new QComboBox(this))
    , m_categoryFilter(new QComboBox(this))
    , m_fromDate(new QDateEdit(this))
    , m_toDate(new QDateEdit(this))
    , m_minAmount(new QDoubleSpinBox(this))
    , m_maxAmount(new QDoubleSpinBox(this))
    , m_applyFilterBtn(new QPushButton("Apply Filters", this))
    , m_clearFilterBtn(new QPushButton("Clear Filters", this))
{
    // Controller setup
    m_controller = std::make_unique<BudgetController>(
        std::make_unique<CsvRepository>("data.csv"), this
    );

    // Central widget and main layout
    QWidget* central = new QWidget(this);
    auto* mainLayout = new QVBoxLayout(central);

    // --- Filter bar setup ---
    m_typeFilter->addItems({ "All", "INCOME", "EXPENSE" });
    m_categoryFilter->addItem("All");
    for (int c = int(Category::FOOD); c <= int(Category::OTHER); ++c) {
        m_categoryFilter->addItem(BudgetItem::categoryToString(Category(c)));
    }
    m_fromDate->setCalendarPopup(true);
    m_toDate->setCalendarPopup(true);
    m_fromDate->setDate(QDate::currentDate().addMonths(-1));
    m_toDate->setDate(QDate::currentDate());
    m_minAmount->setRange(-1e6, 1e6);
    m_maxAmount->setRange(-1e6, 1e6);
    m_minAmount->setValue(0);
    m_maxAmount->setValue(0);

    auto* filterWidget = new QWidget(central);
    auto* filterLayout = new QFormLayout();
    filterLayout->addRow("Type:",       m_typeFilter);
    filterLayout->addRow("Category:",   m_categoryFilter);
    filterLayout->addRow("Date from:",  m_fromDate);
    filterLayout->addRow("Date to:",    m_toDate);
    filterLayout->addRow("Min amount:", m_minAmount);
    filterLayout->addRow("Max amount:", m_maxAmount);

    // Create button layout
    auto* buttonLayout = new QHBoxLayout();
    buttonLayout->addWidget(m_applyFilterBtn);
    buttonLayout->addWidget(m_clearFilterBtn);

    // Combine form layout and buttons in a vertical layout
    auto* filterMainLayout = new QVBoxLayout(filterWidget);
    filterMainLayout->addLayout(filterLayout);
    filterMainLayout->addLayout(buttonLayout);

    mainLayout->addWidget(filterWidget);

    // --- Table setup ---
    m_table->setColumnCount(6);
    m_table->setHorizontalHeaderLabels({
        "ID", "Description", "Amount", "Date", "Type", "Category"
    });
    m_table->hideColumn(0);
    m_table->horizontalHeader()->setStretchLastSection(true);
    mainLayout->addWidget(m_table);
    mainLayout->addWidget(m_addBtn);
    mainLayout->addWidget(m_updateBtn);
    mainLayout->addWidget(m_removeBtn);
    mainLayout->addWidget(m_undoBtn);
    mainLayout->addWidget(m_saveBtn);
    mainLayout->addWidget(m_loadBtn);

    setCentralWidget(central);
    setWindowTitle("Home Budget Tracker");

    // --- Signals & slots ---
    connect(m_addBtn,    &QPushButton::clicked, this, &MainWindow::onAddClicked);
    connect(m_updateBtn, &QPushButton::clicked, this, &MainWindow::onUpdateClicked);
    connect(m_removeBtn, &QPushButton::clicked, this, &MainWindow::onRemoveClicked);
    connect(m_undoBtn,   &QPushButton::clicked, this, &MainWindow::onUndoClicked);
    connect(m_saveBtn,   &QPushButton::clicked, this, &MainWindow::onSaveClicked);
    connect(m_loadBtn,   &QPushButton::clicked, this, &MainWindow::onLoadClicked);
    connect(m_applyFilterBtn, &QPushButton::clicked, this, &MainWindow::onApplyFilterClicked);
    connect(m_clearFilterBtn, &QPushButton::clicked, this, &MainWindow::onClearFilterClicked);
    connect(m_controller.get(), &BudgetController::dataChanged,
            this, &MainWindow::refreshTable);

}

void MainWindow::populateTable(const std::vector<BudgetItem>& items) {
    m_table->clearContents();
    m_table->setRowCount(int(items.size()));
    for (int row = 0; row < int(items.size()); ++row) {
        const auto& it = items[row];
        m_table->setItem(row, 0, new QTableWidgetItem(QString::number(it.getId())));
        m_table->setItem(row, 1, new QTableWidgetItem(it.getDescription()));
        m_table->setItem(row, 2, new QTableWidgetItem(QString::number(it.getAmount())));
        m_table->setItem(row, 3, new QTableWidgetItem(it.getDate().toString("yyyy-MM-dd")));
        m_table->setItem(row, 4, new QTableWidgetItem(BudgetItem::typeToString(it.getType())));
        m_table->setItem(row, 5, new QTableWidgetItem(BudgetItem::categoryToString(it.getCategory())));
    }
    m_undoBtn->setEnabled(m_controller->canUndo());
}

void MainWindow::refreshTable() {
    populateTable(m_controller->getAllItems());
}

void MainWindow::onAddClicked() {
    bool ok;
    QString desc = QInputDialog::getText(this, "Add Item", "Description:",
                                         QLineEdit::Normal, QString(), &ok);
    if (!ok || desc.isEmpty()) return;

    double amount = QInputDialog::getDouble(this, "Add Item", "Amount:",
                                            0.0, -1e6, 1e6, 2, &ok);
    if (!ok) return;

    QString dateStr = QInputDialog::getText(this, "Add Item", "Date (YYYY-MM-DD):",
                                             QLineEdit::Normal, QDate::currentDate().toString("yyyy-MM-dd"), &ok);
    if (!ok) return;
    QDate date = QDate::fromString(dateStr, "yyyy-MM-dd");
    if (!date.isValid()) {
        QMessageBox::warning(this, "Invalid Date", "Date format should be YYYY-MM-DD.");
        return;
    }

    QString typeStr = QInputDialog::getItem(this, "Add Item", "Type:",
                                             {"INCOME","EXPENSE"}, 1, false, &ok);
    if (!ok) return;

    QStringList cats;
    for (int c = int(Category::FOOD); c <= int(Category::OTHER); ++c)
        cats << BudgetItem::categoryToString(Category(c));
    QString catStr = QInputDialog::getItem(this, "Add Item", "Category:",
                                           cats, 0, false, &ok);
    if (!ok) return;

    BudgetItem newItem(0, desc, amount, date,
                       BudgetItem::stringToType(typeStr),
                       BudgetItem::stringToCategory(catStr));
    m_controller->addItem(newItem);
}

void MainWindow::onUpdateClicked() {
    auto sel = m_table->selectionModel()->selectedRows();
    if (sel.isEmpty()) return;
    int row = sel.first().row();
    int id  = m_table->item(row, 0)->text().toInt();
    auto item = m_controller->getItemById(id);

    bool ok;
    QString desc = QInputDialog::getText(this, "Update Item", "Description:",
                                         QLineEdit::Normal, item.getDescription(), &ok);
    if (!ok) return;

    double amount = QInputDialog::getDouble(this, "Update Item", "Amount:",
                                            item.getAmount(), -1e6, 1e6, 2, &ok);
    if (!ok) return;

    QString dateStr = QInputDialog::getText(this, "Update Item", "Date (YYYY-MM-DD):",
                                             QLineEdit::Normal, item.getDate().toString("yyyy-MM-dd"), &ok);
    if (!ok) return;
    QDate date = QDate::fromString(dateStr, "yyyy-MM-dd");
    if (!date.isValid()) {
        QMessageBox::warning(this, "Invalid Date", "Date format should be YYYY-MM-DD.");
        return;
    }

    QString typeStr = QInputDialog::getItem(this, "Update Item", "Type:",
                                             {"INCOME","EXPENSE"},
                                             item.getType() == TransactionType::INCOME ? 0 : 1,
                                             false, &ok);
    if (!ok) return;

    QStringList cats;
    for (int c = int(Category::FOOD); c <= int(Category::OTHER); ++c)
        cats << BudgetItem::categoryToString(Category(c));
    QString catStr = QInputDialog::getItem(this, "Update Item", "Category:",
                                           cats,
                                           cats.indexOf(BudgetItem::categoryToString(item.getCategory())),
                                           false, &ok);
    if (!ok) return;

    BudgetItem updated(id, desc, amount, date,
                       BudgetItem::stringToType(typeStr),
                       BudgetItem::stringToCategory(catStr));
    m_controller->updateItem(id, updated);
}

void MainWindow::onRemoveClicked() {
    auto sel = m_table->selectionModel()->selectedRows();
    if (sel.isEmpty()) return;
    int row = sel.first().row();
    int id  = m_table->item(row, 0)->text().toInt();
    m_controller->removeItem(id);
}

void MainWindow::onUndoClicked() {
    m_controller->undo();
}

void MainWindow::onSaveClicked() {
    QString fileName = QFileDialog::getSaveFileName(
        this,
        "Save Budget Data",
        "budget_data.csv",
        "CSV Files (*.csv);;JSON Files (*.json);;All Files (*)"
    );

    if (fileName.isEmpty()) {
        return;
    }

    std::unique_ptr<BaseRepository> repository;

    if (fileName.endsWith(".json", Qt::CaseInsensitive)) {
        repository = std::make_unique<JsonRepository>(fileName);
    } else {
        repository = std::make_unique<CsvRepository>(fileName);
    }

    if (repository->save(m_controller->getAllItems())) {
        QMessageBox::information(this, "Save Successful",
                                "Budget data saved successfully to: " + fileName);
    } else {
        QMessageBox::warning(this, "Save Failed",
                            "Failed to save budget data to: " + fileName);
    }
}

void MainWindow::onLoadClicked() {
    QString fileName = QFileDialog::getOpenFileName(
        this,
        "Load Budget Data",
        "",
        "CSV Files (*.csv);;JSON Files (*.json);;All Files (*)"
    );

    if (fileName.isEmpty()) {
        return;
    }

    std::unique_ptr<BaseRepository> repository;

    if (fileName.endsWith(".json", Qt::CaseInsensitive)) {
        repository = std::make_unique<JsonRepository>(fileName);
    } else {
        repository = std::make_unique<CsvRepository>(fileName);
    }

    if (!repository->isValid()) {
        QMessageBox::warning(this, "Load Failed",
                            "Selected file is invalid or cannot be accessed: " + fileName);
        return;
    }

    auto loadedItems = repository->load();

    if (loadedItems.empty()) {
        QMessageBox::information(this, "Load Result",
                                "No data loaded from file (file may be empty): " + fileName);
        return;
    }

    // Ask user if they want to replace current data or append
    QMessageBox::StandardButton reply = QMessageBox::question(
        this,
        "Load Data",
        QString("Loaded %1 items from file.\n\nDo you want to:\n"
                "Yes - Replace current data\n"
                "No - Append to current data\n"
                "Cancel - Cancel load operation")
            .arg(loadedItems.size()),
        QMessageBox::Yes | QMessageBox::No | QMessageBox::Cancel
    );

    if (reply == QMessageBox::Cancel) {
        return;
    }

    if (reply == QMessageBox::Yes) {
        // Replace current data - create new controller with loaded data
        m_controller = std::make_unique<BudgetController>(
            std::make_unique<CsvRepository>("data.csv"), this
        );

        // Add all loaded items
        for (const auto& item : loadedItems) {
            m_controller->addItem(item);
        }

        // Reconnect signal
        connect(m_controller.get(), &BudgetController::dataChanged,
                this, &MainWindow::refreshTable);
    } else {
        // Append to current data
        for (const auto& item : loadedItems) {
            m_controller->addItem(item);
        }
    }

    refreshTable();

    QMessageBox::information(this, "Load Successful",
                            QString("Successfully loaded %1 items from: %2")
                                .arg(loadedItems.size()).arg(fileName));
}

void MainWindow::onApplyFilterClicked() {
    try {
        // Create a composite filter with AND logic (all conditions must be met)
        auto compositeFilter = std::make_unique<CompositeFilter>(LogicalOperator::AND);
        bool hasActiveFilters = false;

        // 1. Add Type Filter
        QString selectedType = m_typeFilter->currentText();
        if (selectedType != "All") {
            TransactionType type = BudgetItem::stringToType(selectedType);
            auto typeFilter = std::make_unique<TypeFilter>(type);
            compositeFilter->addFilter(std::move(typeFilter));
            hasActiveFilters = true;
        }

        // 2. Add Category Filter
        QString selectedCategory = m_categoryFilter->currentText();
        if (selectedCategory != "All") {
            Category category = BudgetItem::stringToCategory(selectedCategory);
            auto categoryFilter = std::make_unique<CategoryFilter>(category);
            compositeFilter->addFilter(std::move(categoryFilter));
            hasActiveFilters = true;
        }

        // 3. Add Date Range Filter
        QDate fromDate = m_fromDate->date();
        QDate toDate = m_toDate->date();

        // Validate date range
        if (fromDate > toDate) {
            QMessageBox::warning(this, "Invalid Date Range",
                                "From date cannot be later than To date.");
            return;
        }

        // Always apply date filter with current range
        auto dateFilter = std::make_unique<DateFilter>(DateComparison::BETWEEN, fromDate, toDate);
        compositeFilter->addFilter(std::move(dateFilter));
        hasActiveFilters = true;

        // 4. Add Amount Range Filter
        double minAmount = m_minAmount->value();
        double maxAmount = m_maxAmount->value();

        // Validate amount range
        if (maxAmount > 0 && minAmount > maxAmount) {
            QMessageBox::warning(this, "Invalid Amount Range",
                                "Minimum amount cannot be greater than maximum amount.");
            return;
        }

        if (minAmount > 0 || maxAmount > 0) {
            if (maxAmount > 0) {
                // Use BETWEEN comparison when both min and max are specified
                auto amountFilter = std::make_unique<AmountFilter>(AmountComparison::BETWEEN, minAmount, maxAmount);
                compositeFilter->addFilter(std::move(amountFilter));
            } else {
                // Only minimum amount specified
                auto amountFilter = std::make_unique<AmountFilter>(AmountComparison::GREATER_THAN, minAmount - 0.01);
                compositeFilter->addFilter(std::move(amountFilter));
            }
            hasActiveFilters = true;
        }

        // Apply the composite filter and display results
        if (hasActiveFilters) {
            auto filteredItems = m_controller->getFilteredItems(*compositeFilter);
            populateTable(filteredItems);

        } else {
            // No filters applied, show all items
            refreshTable();
        }

    } catch (const std::exception& e) {
        QMessageBox::critical(this, "Filter Error",
                             QString("An error occurred while applying filters: %1").arg(e.what()));
        // Fallback to showing all items
        refreshTable();
    }
}

// Bonus: Enhanced Clear Filters method
void MainWindow::onClearFilterClicked() {
    // Reset all filter controls to default values
    m_typeFilter->setCurrentIndex(0);           // "All"
    m_categoryFilter->setCurrentIndex(0);       // "All"
    m_fromDate->setDate(QDate::currentDate().addMonths(-1));
    m_toDate->setDate(QDate::currentDate());
    m_minAmount->setValue(0);
    m_maxAmount->setValue(0);

    // Show all items
    refreshTable();
}