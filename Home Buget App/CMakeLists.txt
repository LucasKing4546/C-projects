cmake_minimum_required(VERSION 3.30)
project(Home_Buget_App)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_AUTOUIC ON)

set(CMAKE_PREFIX_PATH "C:/Qt/6.9.0/mingw_64/lib/cmake")

find_package(Qt6 COMPONENTS
        Core
        Gui
        Widgets
        REQUIRED)

# Source files
set(SOURCES
        main.cpp
        models/BudgetItem.cpp
        repository/CsvRepository.cpp
        repository/JsonRepository.cpp
        controller/BudgetController.cpp
        controller/commands/AddCommand.cpp
        controller/commands/RemoveCommand.cpp
        controller/commands/UpdateCommand.cpp
        controller/filters/CategoryFilter.cpp
        controller/filters/AmountFilter.cpp
        controller/filters/DateFilter.cpp
        controller/filters/CompositeFilter.cpp
        ui/MainWindow.cpp
)

# Header files
set(HEADERS
        models/BudgetItem.h
        repository/BaseRepository.h
        repository/CsvRepository.h
        repository/JsonRepository.h
        controller/BudgetController.h
        controller/commands/Command.h
        controller/commands/AddCommand.h
        controller/commands/RemoveCommand.h
        controller/commands/UpdateCommand.h
        controller/filters/FilterStrategy.h
        controller/filters/CategoryFilter.h
        controller/filters/AmountFilter.h
        controller/filters/DateFilter.h
        controller/filters/CompositeFilter.h
        ui/MainWindow.h
)

add_executable(Home_Buget_App ${SOURCES} ${HEADERS})

target_link_libraries(Home_Buget_App
        Qt::Core
        Qt::Gui
        Qt::Widgets
)

# Optional: Build tests
option(BUILD_TESTS "Build tests" OFF)
if(BUILD_TESTS)
    find_package(GTest REQUIRED)

    set(TEST_SOURCES
            tests/test_repository.cpp
            tests/test_controller.cpp
            models/BudgetItem.cpp
            repository/CsvRepository.cpp
            repository/JsonRepository.cpp
            controller/BudgetController.cpp
            controller/commands/AddCommand.cpp
            controller/commands/RemoveCommand.cpp
            controller/commands/UpdateCommand.cpp
            controller/filters/CategoryFilter.cpp
            controller/filters/AmountFilter.cpp
            controller/filters/DateFilter.cpp
            controller/filters/CompositeFilter.cpp
    )

    add_executable(BudgetTrackerTests ${TEST_SOURCES})
    target_link_libraries(BudgetTrackerTests
            GTest::gtest_main
            Qt::Core
    )
endif()

# Windows-specific DLL copying (your existing code)
if (WIN32 AND NOT DEFINED CMAKE_TOOLCHAIN_FILE)
    set(DEBUG_SUFFIX)
    if (MSVC AND CMAKE_BUILD_TYPE MATCHES "Debug")
        set(DEBUG_SUFFIX "d")
    endif()
    set(QT_INSTALL_PATH "${CMAKE_PREFIX_PATH}")
    if(NOT EXISTS "${QT_INSTALL_PATH}/bin")
        set(QT_INSTALL_PATH "${QT_INSTALL_PATH}/..")
        if(NOT EXISTS "${QT_INSTALL_PATH}/bin")
            set(QT_INSTALL_PATH "${QT_INSTALL_PATH}/..")
        endif()
    endif()
    if(EXISTS "${QT_INSTALL_PATH}/plugins/platforms/qwindows${DEBUG_SUFFIX}.dll")
        add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD
                COMMAND ${CMAKE_COMMAND} -E make_directory
                "$<TARGET_FILE_DIR:${PROJECT_NAME}>/plugins/platforms/")
        add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD
                COMMAND ${CMAKE_COMMAND} -E copy
                "${QT_INSTALL_PATH}/plugins/platforms/qwindows${DEBUG_SUFFIX}.dll"
                "$<TARGET_FILE_DIR:${PROJECT_NAME}>/plugins/platforms/")
    endif()
    foreach(QT_LIB Core Gui Widgets )
        add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD
                COMMAND ${CMAKE_COMMAND} -E copy
                "${QT_INSTALL_PATH}/bin/Qt6${QT_LIB}${DEBUG_SUFFIX}.dll"
                "$<TARGET_FILE_DIR:${PROJECT_NAME}>")
    endforeach(QT_LIB)
endif()