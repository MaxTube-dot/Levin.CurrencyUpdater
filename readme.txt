��������� �� �������
1) ��������� CurrencyUpdaterHost - ������.
2) ��������� WinFormsClient - ������.

���������:
1)������ CurrencyUpdaterHost
   �������� ���������� �������� �����.
2)������ CurrencyUpdater 
   ��������� ������ ���������� InvalidDateFault, InvalidDateFault, InvalidServerFault.
   ��������� ������:
                    GetCurrency(string charCode) - �������� �������� ������ �� ������� ����. 
                    GetCurrencyForDayCharCode(DateTime date, string charCode) - �������� �������� ������ �� ������������ ����. 
                    GetCurrencyForDay(DateTime date) - �������� �������� ���� ����� � ������������ ����. 