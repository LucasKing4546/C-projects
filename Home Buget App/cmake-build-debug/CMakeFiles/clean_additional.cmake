# Additional clean files
cmake_minimum_required(VERSION 3.16)

if("${CONFIG}" STREQUAL "" OR "${CONFIG}" STREQUAL "Debug")
  file(REMOVE_RECURSE
  "CMakeFiles\\Home_Buget_App_autogen.dir\\AutogenUsed.txt"
  "CMakeFiles\\Home_Buget_App_autogen.dir\\ParseCache.txt"
  "Home_Buget_App_autogen"
  )
endif()
