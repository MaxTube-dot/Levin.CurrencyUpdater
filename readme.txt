Иструкция по запуску
1) Запустить CurrencyUpdaterHost - сервер.
2) Запустить WinFormsClient - клиент.

Изменения:
1)Проект CurrencyUpdaterHost
   Перехват исключения занятого порта.
2)Проект CurrencyUpdater 
   Добавлены классы исключений InvalidDateFault, InvalidDateFault, InvalidServerFault.
   Добавлены методы:
                    GetCurrency(string charCode) - получить значение валюты на текущий день. 
                    GetCurrencyForDayCharCode(DateTime date, string charCode) - получить значение валюты на определенный день. 
                    GetCurrencyForDay(DateTime date) - получить значения всех валют в определенный день. 