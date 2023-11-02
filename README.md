# DeliveryOrder
Back: ASP.NET 7
Front: ASP.NET MVC
Database: Microsoft SQL

Для запуска приложение:
1. В файле /DeliveryOrder/appsettings.json изменить значение параметра "DbDelivery" на Ваш путь к серверу microsoft sql server, если есть локальный сервер, можно не изменять путь (должен подойти).
2. Запустить приложение
3. При запуске создаться БД, найти БД "resurs" и выполнить в нём запросы из файла /Application/Init.sql (создание хранимой процедуры, добавление тестовых, начальных данных).
