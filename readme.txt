В папке Docs - документ с кратким описанием MathPanel
В папке scipts - тестовые скрипты 
В папке Bin\Release находятся программа MathPanel.exe и конфигурационный файл MathPanel.exe.config
В нем надо поправить строки
    <!--path to framework -->
    <add key="netframworkpath" value="C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\" />
    <!--login, password to https://www.pvobr.ru -->
    <add key="user" value="" />
    <add key="pass" value="" />

Перед первым запуском MathPanel.exe надо выполнить "2.reg" для добавления ключей в регистр.
При переходе по кнопке "График" возможно потребуется разблокировать контент веб-страницы.

API
Пример рисования текста и линий вместе с геом.объектами дан в "scripts\test36_text_3d.cs"

Нужен пакет
Npgsql.4.1.3.1

Bin/Debug и Bin/Release скопировать
graphix.js, jquery-1.11.1.min.js, test_graph.htm, ru.local, scripts/template.cs


