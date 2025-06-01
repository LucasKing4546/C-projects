#include <gtest/gtest.h>
#include <QApplication>
#include "ui/MainWindow.h"

int main(int argc, char *argv[])
{
    ::testing::InitGoogleTest(&argc, argv);

    int test_result = RUN_ALL_TESTS();
    if (test_result != 0) {
        return test_result;
    }

    QApplication app(argc, argv);
    MainWindow w;
    w.show();
    return app.exec();
}
