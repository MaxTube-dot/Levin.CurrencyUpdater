using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Получить актуальный курс вальты. 
        /// </summary>
        /// <param name="charCode">Код валюты.</param>
        /// <returns>Числовое значение курса валюты к рублю. Если 0 - валюта не найдена или не существует.</returns>
        public double GetCurrency(string charCode)
        {
            if (String.IsNullOrEmpty(charCode))
            {
                return 0;
            }

            ValCurs valutes = Request();

            string valueValute = valutes.Valute.Where(x => x.CharCode.ToLower().Contains(charCode.ToLower())).Select(x => x.Value).FirstOrDefault();

            if (valueValute == null)
            {
                valueValute = "0";
            }

            return Convert.ToDouble(valueValute);
        }

        /// <summary>
        /// Метод реализующий запрос к http://www.cbr.ru/scripts/XML_daily.asp для получения актуального курса валют к рублю.
        /// </summary>
        /// <returns>Курс валют к рублю.</returns>
        private ValCurs Request()
        {
            string url = "http://www.cbr.ru/scripts/XML_daily.asp";

            XmlSerializer serializer = new XmlSerializer(typeof(ValCurs));

            ValCurs valutesCurs = new ValCurs();

            using (XmlTextReader reader = new XmlTextReader(url))
            {
                valutesCurs = (ValCurs)serializer.Deserialize(reader);
            }

            return valutesCurs;
        }
    }
}
