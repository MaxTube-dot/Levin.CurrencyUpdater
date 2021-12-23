using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CurrencyUpdater
{
 
    [ServiceContract]
    public interface ICurrencyUpdate
    {
        /// <summary>
        /// Получить актуальный курс вальты. 
        /// </summary>
        /// <param name="charCode">Код валюты.</param>
        /// <returns>Числовое значение курса валюты к рублю.</returns>
        [OperationContract]
        double GetCurrency(string charCode);
    }
}
