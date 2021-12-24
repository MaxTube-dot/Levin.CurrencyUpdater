using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CurrencyUpdater
{
    /// <summary>
    /// Класс отвечающий за логику работы сервиса WCF. 
    /// </summary>
    public class CurrencyUpdate : ICurrencyUpdate
    {

        /// <summary>
        /// Получить актуальный(на данный момент времени) курс вальты в рублях. 
        /// </summary>
        /// <param name="charCode">Код валюты.</param>
        /// <returns>Числовое значение курса валюты к рублю.</returns>
        public double GetCurrency(string charCode)
        {
            double cost = RequestCurrencys(DateTime.Today, charCode);

            return cost;
        }


        /// <summary>
        /// Получение стоимости определенной валюты в определенный день в рублях.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="charCode"></param>
        /// <returns>Числовое значение курса валюты к рублю.</returns>
        public double GetCurrencyForDayCharCode(DateTime date, string charCode)
        {
            double cost = RequestCurrencys(date, charCode);

            return cost;
        }


        /// <summary>
        /// Получение ссылки на объекта ValCurs(содержащего все курсы валют), на опреденный день.
        /// </summary>
        /// <param name="date">День для которого необходимо получить курсы валют.</param>
        /// <returns>Ссылка объекта ValCurs, содержащего все курсы валют.</returns>
        public ValCurs GetCurrencyForDay(DateTime date)
        {
            ValCurs curs = Request(date);

            return curs;
        }


        /// <summary>
        /// Метод реализующий запрос к http://www.cbr.ru/ для получения курса валют к рублю на определенный день.
        /// </summary>
        /// <returns>Курс валют к рублю.</returns>
        private ValCurs Request(DateTime needlyDate)
        {

            string dateOfFirstCurrency = "01.07.1992";   /// Предположим до этой даты, данных о курсе валют не существует!

            DateTime firstCurrency = Convert.ToDateTime(dateOfFirstCurrency);

            if (needlyDate > DateTime.Now || needlyDate < firstCurrency)
            {
                throw new FaultException<InvalidDateFault>(new InvalidDateFault("Не существует данных для запрашиваемой даты!"));
            }

            string date = needlyDate.ToString("d");

            string url = $"http://www.cbr.ru/scripts/XML_daily.asp?date_req={date}";

            XmlSerializer serializer = new XmlSerializer(typeof(ValCurs));

            ValCurs valutesCurs = new ValCurs();

            try
            {
                using (XmlTextReader reader = new XmlTextReader(url))
                {
                    valutesCurs = (ValCurs)serializer.Deserialize(reader);
                }
            }
            catch (InvalidOperationException ex) ///Узнать исключение недоступного сервиса.
            {
                if (ex.InnerException != null)
                {
                    var exx =  ex.InnerException;

                    throw new FaultException<InvalidServerFault>(new InvalidServerFault("Сервер http://www.cbr.ru не доступен!"));
                }

                throw new FaultException<InvalidServerFault>(new InvalidServerFault("Сервер http://www.cbr.ru/ не содержит данных для этой даты или валюты!"));
            }

            return valutesCurs;
        }

        /// <summary>
        /// Получение курса определенной валюты для опереденного для.
        /// </summary>
        /// <param name="needlyDate">ДЕнь для которого необходимо узнать курс валют.</param>
        /// <param name="charCode">Код валютыю</param>
        /// <returns>Численное значение стоимости валюты к рублю.</returns>
        private double RequestCurrencys(DateTime needlyDate, string charCode)
        {
            ValCurs curs = Request(needlyDate);

            if (String.IsNullOrEmpty(charCode) || String.IsNullOrWhiteSpace(charCode))
            {
                throw new FaultException<InvalidCurrencyFault>(new InvalidCurrencyFault("Код валюты равен null, пустой или содержит пробелы!"));
            }

            string value;

            try
            {
                 value = curs.Valute.Where(x => x.CharCode.ToLower() == charCode.ToLower()).Select(x => x.Value).FirstOrDefault();
            }
            catch (NullReferenceException) 
            {
                throw new FaultException<InvalidCurrencyFault>(new InvalidCurrencyFault("Валюта не найдена в списке валют!"));
            }

            if (String.IsNullOrEmpty(value))
            {
                throw new  FaultException<InvalidCurrencyFault>(new InvalidCurrencyFault("Валюта не найдена в списке валют!"));
            }

            if (double.TryParse(value, out double result))
            {
                return result;
            }
            else
            {
                throw new FaultException<InvalidCurrencyFault>(new InvalidCurrencyFault("Некорректное численное значение валюты(стоимость)!"));
            }
        }
    }

    

}
