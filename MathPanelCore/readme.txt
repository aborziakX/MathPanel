Работа с решением на базе .Net Core

Установить проект MathPanel из Гит, например в C:\GitMathPanel
Запустить решение MathPanelCore.sln из C:\GitMathPanel\MathPanelCore\MathPanelCore
Запустить выполнение, например Debug
Ошибка, не хватает файла MathPanelCore.config в папке bin\Debug\netcoreapp3.1
(C:\GitMathPanel\MathPanelCore\MathPanelCore\bin\Debug\netcoreapp3.1)

скопировать туда MathPanelCore.config
снова запустить, сервер стартует успешно

запустить в браузере MathPanelCore.htm
Быстрый старт - нажать "Выполнить", потом "Графика", должна быть красная линия по диагонали.
Далее в окно скриптов можно копировать тексты скраптов из папки C:\GitMathPanel\MathPanelCore\scripts . нажать "Выполнить", потом "Графика.