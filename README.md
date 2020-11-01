# ParcelAutomatWebApi
REST API для постамата.

# Требования для запуска сервера
1. Установить утилиту `dotnet` или Docker

# Как запустить сервер стандартными возможностями ?
1. Заходим через консоль в папку с решением `ParcelAutomatWebApi.sln`
2. Пишем в консоли `dotnet build`
3. Запускаем сервер с помощью команды `dotnet .\WebApi\bin\Debug\netcoreapp3.1\WebApi.dll`

# Как запустить сервер с помощью Docker ?
1. Перейдите в папку `ParcelAutomatWebApi`
2. Выполнить команду в консоли `docker-compose up`

# Просмотр Swagger
Запускаем сервер по описанию выше и переходим по ссылке: http://localhost:5000/swagger/index.html
