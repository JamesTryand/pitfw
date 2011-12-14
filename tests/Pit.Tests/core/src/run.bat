jsbeautifier.py -o ..\web\js\pit.core.js bin\debug\pit.core.js

rem Tests JS file generation
jsbeautifier.py -o ..\web\tests\core_tests.js bin\debug\core_tests.js
jsbeautifier.py -o ..\web\tests\core2_tests.js bin\debug\core2_tests.js
jsbeautifier.py -o ..\web\tests\jsliterals.js bin\debug\jsliterals.js