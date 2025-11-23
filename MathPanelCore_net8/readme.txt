MathPanelCore_net8 - решение для Linux на базе .Net Core 8.

Предположим, пакет MathPanel установлен в ~/develop/MathPanel.

Соответственно, MathPanelCore_net8 в ~/develop/MathPanel/MathPanelCore_net8 .

Предполагаем работу с VS Code. Ваполняем команды
cd ~/develop/MathPanel/MathPanelCore_net8
editor

или запускаем VS Code, Open Folder, открываем ~/develop/MathPanel/MathPanelCore_net8 .

Запустить решение ConsoleApp1.sln из MathPanelCore_net8.
Запустить выполнение, например Debug.
Ошибка, не хватает файла MathPanelCore.config в папке ~/develop/MathPanel/MathPanelCore_net8/ConsoleApp1/bin/Debug/net8.0,

скопировать туда MathPanelCore.config из ConsoleApp1,
снова запустить, сервер стартует успешно.

Запустить в браузере MathPanelCore.htm из папки ~/develop/MathPanel/MathPanelCore_net8
(в строке браузера file:///home/USER/develop/MathPanel/MathPanelCore_net8/MathPanelCore.htm)
(home/USER - зависит от конкретного компьтера).
Быстрый старт - нажать "Выполнить", потом "Графика", должна быть красная линия по диагонали.

Далее в окно скриптов можно копировать тексты скриптов из папки ~/develop/MathPanel/MathPanelCore_net8/pictures или ~/develop/MathPanel/MathPanelCore_net8/scripts. 

Нажать "Выполнить", потом "Графика.

Т.е.интерфейс пользователя в MathPanelCore.htm, которая взаимодействует с сервером ~/develop/MathPanel/MathPanelCore_net8/ConsoleApp1/bin/Debug/net8.0/ConsoleApp1 .



