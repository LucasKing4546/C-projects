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

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , m_table(new QTableWidget(this))
    , m_addBtn(new QPushButton("Add Item", this))
    , m_updateBtn(new QPushButton("Update Selected", this))
    , m_removeBtn(new QPushButton("Remove Selected", this))
    , m_undoBtn(new QPushButton("Undo", this))
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

    setCentralWidget(central);
    setWindowTitle("Home Budget Tracker");

    // --- Signals & slots ---
    connect(m_addBtn,    &QPushButton::clicked, this, &MainWindow::onAddClicked);
    connect(m_updateBtn, &QPushButton::clicked, this, &MainWindow::onUpdateClicked);
    connect(m_removeBtn, &QPushButton::clicked, this, &MainWindow::onRemoveClicked);
    connect(m_undoBtn,   &QPushButton::clicked, this, &MainWindow::onUndoClicked);
    connect(m_applyFilterBtn, &QPushButton::clicked, this, &MainWindow::onApplyFilterClicked);
    connect(m_clearFilterBtn, &QPushButton::clicked, this, &MainWindow::onClearFilterClicked);
    connect(m_controller.get(), &BudgetController::dataChanged,
            this, &MainWindow::refreshTable);

    // Initial load
    m_controller->load();
    refreshTable();
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
    m_controller->save();
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
    m_controller->save();
}

void MainWindow::onRemoveClicked() {
    auto sel = m_table->selectionModel()->selectedRows();
    if (sel.isEmpty()) return;
    int row = sel.first().row();
    int id  = m_table->item(row, 0)->text().toInt();
    m_controller->removeItem(id);
    m_controller->save();
}

void MainWindow::onUndoClicked() {
    if (m_controller->undo())
        m_controller->save();
}

void MainWindow::onApplyFilterClicked() {
    auto items = m_controller->getAllItems();  // start from full list :contentReference[oaicite:2]{index=2}

    // type filter
    QString t = m_typeFilter->currentText();
    if (t != "All") {
        items.erase(
            std::remove_if(items.begin(), items.end(),
                [&](const BudgetItem& i){ return BudgetItem::typeToString(i.getType()) != t; }),
            items.end()
        );
    }

    // category filter
    QString c = m_categoryFilter->currentText();
    if (c != "All") {
        items.erase(
            std::remove_if(items.begin(), items.end(),
                [&](const BudgetItem& i){ return BudgetItem::categoryToString(i.getCategory()) != c; }),
            items.end()
        );
    }

    // date range filter
    QDate from = m_fromDate->date();
    QDate to   = m_toDate->date();
    items.erase(
        std::remove_if(items.begin(), items.end(),
            [&](const BudgetItem& i){ return i.getDate() < from || i.getDate() > to; }),
        items.end()
    );

    // amount range filter (0 means no-upper-limit if max == 0)
    double minA = m_minAmount->value();
    double maxA = m_maxAmount->value();
    items.erase(
        std::remove_if(items.begin(), items.end(),
            [&](const BudgetItem& i){
                bool below = i.getAmount() < minA;
                bool above = (maxA > 0 && i.getAmount() > maxA);
                return below || above;
            }),
        items.end()
    );

    populateTable(items);
}

void MainWindow::onClearFilterClicked() {
    m_typeFilter->setCurrentIndex(0);
    m_categoryFilter->setCurrentIndex(0);
    m_fromDate->setDate(QDate::currentDate().addMonths(-1));
    m_toDate->setDate(QDate::currentDate());
    m_minAmount->setValue(0);
    m_maxAmount->setValue(0);
    refreshTable();
}
