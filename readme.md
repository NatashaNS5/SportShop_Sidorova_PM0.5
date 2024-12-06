# СпортТовары

Приложение для спортивных товаров позволяет пользователям легко и быстро взаимодействовать с доступным ассортиментом. Оно включает функции входа в систему, просмотр каталога товаров, оформление заказов, а также возможность администрирования, включая редактирование информации о товарах. Удобный интерфейс и тщательно продуманный функционал делают приложение комфортным для всех категорий пользователей.

## Начало работы

Эти инструкции предоставят вам копию проекта и помогут запустить на Вашем локальном компьютере.

### Необходимые требования

1. **Операционная система**: Windows 10 либо Windows 11.  
2. **Среда разработки**: Visual Studio 2022 (доступна для загрузки с [официального сайта](https://visualstudio.microsoft.com/)).  
  - Убедитесь, что при установке выбраны рабочие нагрузки для .NET Framework 4.8.  
3. **Дополнительное ПО**:  
  - Git для работы с репозиторием (доступен на [официальном сайте](https://git-scm.com/)).  
4. **Место на диске**: минимум 5 ГБ свободного пространства.  
5. **Оперативная память**: не менее 8 ГБ для устойчивой работы Visual Studio.

### Установка

1. **Подготовка инструментов**  
  a. Скачайте Visual Studio 2022 с [официального сайта](https://visualstudio.microsoft.com/) и установите её.  
  b. Во время установки выберите рабочую нагрузку **Разработка на .NET Framework 4.8**.  
  c. Установите Git для работы с репозиторием, загрузив его с [официального сайта](https://git-scm.com/).  

2. **Клонирование репозитория**  
  a. Откройте терминал (Git Bash, Command Prompt или PowerShell).  
  b. Введите команду:  
     ```bash
     git clone <ссылка_на_репозиторий>
     ```  
  c. Перейдите в каталог проекта:  
     ```bash
     cd <название_папки>
     ```  

3. **Настройка зависимостей**  
  a. Проект использует систему управления зависимостями, NuGet, выполните команду для восстановления пакетов:  
     ```bash
     dotnet restore
     ```  

4. **Открытие проекта**  
  a. Запустите Visual Studio 2022.  
  b. В меню выберите **Открыть проект или решение** и укажите путь к файлу проекта (.sln) в папке с репозиторием.  

5. **Конфигурация проекта**  
  a. Установите подходящую конфигурацию сборки в Visual Studio: **Debug** (для тестирования) или **Release** (для продакшена).  
  b. Настройте параметры запуска, если приложение зависит от локального сервера или базы данных.  

6. **Запуск приложения**  
  a. В Visual Studio нажмите кнопку **Запустить** или **используйте клавишу F5**.  
  b. Дождитесь завершения сборки — приложение автоматически откроется.  

7. **Проверка функциональности**  
  a. Убедитесь, что приложение запускается корректно и основные функции, такие как авторизация, просмотр каталога и оформление заказов работают.  
  b. При необходимости отредактируйте параметры в конфигурационных файлах, например, `App.config`, для настройки базы данных или других компонентов.  

Ваш проект готов к использованию!

## Авторы

* **Наталья Сидорова** - *Initial work* - [NatashaNS5](https://github.com/NatashaNS5)
