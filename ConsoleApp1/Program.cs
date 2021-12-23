using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            ValCurs valutes = Request();

            var valueValute = valutes.Valute.Where(x => x.CharCode == "GBP").Select(x => x.Value).FirstOrDefault();

  
        }

        private static ValCurs Request()
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
