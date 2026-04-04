## Вимоги
- .NET 8.0 SDK або новіше

## Встановлення залежностей
1. Клонуйте репозиторій:
   `git clone <LINK>`
2. Перейдіть в директорію із проектом.
3. У папці з проектом (де файл з розширенням .csproj) відкрийте консоль та введіть команду:
   `dotnet restore`

## Запуск проекту
1. Перейдіть в директорію із проектом.
2. Виконайте команду в терміналі:
   `dotnet run <назва файлу>`

Наприклад:
   `dotnet run file.txt`

де file.txt - файл із залежностями. У випадку подібного запису важливо, щоб файл був в тій самій директорії, де й відкрито термінал. Інакше потрібно замість назви файлу прописати шлях до нього.

## Запуск перевірки лінтером
Лінтер StyleCop автоматично запускається під час компіляції або запуску проекту. 
Для компіляції відкрийте термінал в папці проекту і введіть: 
`dotnet build`

## Запуск тестів
`dotnet test`

або для запуску з покриттям тестів:

`dotnet test --collect:"XPlat Code Coverage"`

## Генерація HTML-звіту покриття коду тестами
Встановіть ReportGenerator:

`dotnet tool install -g dotnet-reportgenerator-globaltool`

генерація звіту:

`reportgenerator -reports:"<шлях до папки з тестами>/TestResults/{guid}/coverage.cobertura.xml" -targetdir:"<шлях до місця збереження html звіту>" -reporttypes:Html`

наприклад:

'reportgenerator -reports:"lab1.Tests/TestResults/*/coverage.cobertura.xml" -targetdir:"lab1.Tests/CoverageReport" -reporttypes:Html'
