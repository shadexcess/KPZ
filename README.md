## Вимоги
- .NET 8.0 SDK або новіше

## Встановлення залежностей
1. Клонуйте репозиторій:
   `git clone <LINK>`
2. Перейдіть в директорію із проектом.
3. У папці з проектом (де файл з розширенням .csproj) відкрийте консоль та встановіть всі залежності проекту:
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

`reportgenerator -reports:"lab1.Tests/TestResults/*/coverage.cobertura.xml" -targetdir:"lab1.Tests/CoverageReport" -reporttypes:Html`

## Docker
Щоб зібрати Docker-образ для утиліти, виконайте корені проекту:

`docker build -t lab1-app .`

Для запуску програми з передачею вхідного файлу використовуйте команду вигляду:

`docker run --rm --volume "<шлях до локальної папки>:<шлях всередині контейнера>" lab1-app <шлях до файлу всередині контейнера>`

наприклад:

`docker run --rm --volume "D:\course3\SEM2\КПЗ\lab1\KPZ\lab1\data:/data" lab1-app /data/file.txt`