cmake_minimum_required(VERSION 3.30)
project(Home_Buget_App LANGUAGES CXX)

# 1) Use C++20
set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# 2) Enable Qt’s auto‐tools (moc, uic, rcc)
set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTOUIC ON)
set(CMAKE_AUTORCC ON)

# 3) Point CMake at your Qt installation (adjust if needed)
set(CMAKE_PREFIX_PATH "C:/Qt/6.9.0/mingw_64/lib/cmake")

# 4) Find the Qt6 modules you need
find_package(Qt6 REQUIRED COMPONENTS
        Core
        Gui
        Widgets
)

# 5) List your application’s sources and headers
set(APP_SOURCES
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
        controller/filters/TypeFilter.cpp
        ui/MainWindow.cpp
)

set(APP_HEADERS
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
        controller/filters/TypeFilter.h
        ui/MainWindow.h
)

# 6) Create the single executable target
add_executable(Home_Buget_App
        ${APP_SOURCES}
        ${APP_HEADERS}   # Headers included so Qt’s moc/uic can see Q_OBJECT, etc.
)

# 7) Link against Qt6 in keyword form (PRIVATE)
target_link_libraries(Home_Buget_App PRIVATE
        Qt::Core
        Qt::Gui
        Qt::Widgets
)

# --------------------------------------------------
# 8) Optionally “embed” GoogleTest into this same exe
# --------------------------------------------------
option(BUILD_TESTS "When ON, compile in and run GTest unit tests before launching the GUI" OFF)
if (BUILD_TESTS)
    include(FetchContent)

    # 8.1 Declare Googletest (pin to an exact tag on GitHub)
    FetchContent_Declare(
            googletest
            GIT_REPOSITORY https://github.com/google/googletest.git
            GIT_TAG        v1.14.0
    )
    # 8.2 Download & configure GoogleTest
    FetchContent_MakeAvailable(googletest)

    # 8.3 List all test .cpp files plus implementation .cpps they depend on
    set(TEST_SOURCES
            tests/test_repository.cpp
            tests/test_controller.cpp
            #
            # Also compile your implementation files so the test code can link against them
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

    # 8.4 Add those TEST_SOURCES into Home_Buget_App (PRIVATE)
    target_sources(Home_Buget_App PRIVATE ${TEST_SOURCES})

    # 8.5 Link Home_Buget_App against gtest (PRIVATE)
    #     (We do NOT use gtest_main here, because our main() is in main.cpp)
    target_link_libraries(Home_Buget_App PRIVATE
            gtest
            Qt::Core   # only if tests reference Qt types (QString, QDate, etc.)
    )

    # 8.6 On non‐Windows, link pthread for GTest
    if (NOT WIN32)
        target_link_libraries(Home_Buget_App PRIVATE pthread)
    endif()

    # 8.7 Register the executable as a CTest test
    enable_testing()
    add_test(NAME AllTests COMMAND Home_Buget_App)
endif()

# --------------------------------------------------
# 9) Windows‐only: copy Qt6 DLLs next to the executable
# --------------------------------------------------
if (WIN32 AND NOT DEFINED CMAKE_TOOLCHAIN_FILE)
    set(DEBUG_SUFFIX "")
    if (MSVC AND CMAKE_BUILD_TYPE MATCHES "Debug")
        set(DEBUG_SUFFIX "d")
    endif()

    set(QT_INSTALL_PATH "${CMAKE_PREFIX_PATH}")
    if (NOT EXISTS "${QT_INSTALL_PATH}/bin")
        set(QT_INSTALL_PATH "${QT_INSTALL_PATH}/..")
        if (NOT EXISTS "${QT_INSTALL_PATH}/bin")
            set(QT_INSTALL_PATH "${QT_INSTALL_PATH}/..")
        endif()
    endif()

    if (EXISTS "${QT_INSTALL_PATH}/plugins/platforms/qwindows${DEBUG_SUFFIX}.dll")
        add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD
                COMMAND ${CMAKE_COMMAND} -E make_directory
                "$<TARGET_FILE_DIR:${PROJECT_NAME}>/plugins/platforms/"
        )
        add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD
                COMMAND ${CMAKE_COMMAND} -E copy
                "${QT_INSTALL_PATH}/plugins/platforms/qwindows${DEBUG_SUFFIX}.dll"
                "$<TARGET_FILE_DIR:${PROJECT_NAME}>/plugins/platforms/"
        )
    endif()

    foreach(QT_LIB Core Gui Widgets)
        add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD
                COMMAND ${CMAKE_COMMAND} -E copy
                "${QT_INSTALL_PATH}/bin/Qt6${QT_LIB}${DEBUG_SUFFIX}.dll"
                "$<TARGET_FILE_DIR:${PROJECT_NAME}>"
        )
    endforeach()
endif()
