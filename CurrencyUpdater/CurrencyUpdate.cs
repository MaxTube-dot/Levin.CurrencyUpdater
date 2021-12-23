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
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class CurrencyUpdate : ICurrencyUpdate
    {
      
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
