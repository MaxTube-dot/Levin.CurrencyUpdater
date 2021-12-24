using System;
using System.Collections.Generic;
using System.Linq;
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
        [FaultContract(typeof(InvalidCurrencyFault))]
        [FaultContract(typeof(InvalidServerFault))]
        double GetCurrency(string charCode);

        /// <summary>
        /// Курс всех валют на опереденную дату к рублю.
        /// </summary>
        /// <param name="date">Дата на которую необходимо узнать курс валют.</param>
        /// <returns>Массив ValCursValute[] содержащий все курсы валют к рублю на дату date</returns>
         [OperationContract]
        [FaultContract(typeof(InvalidDateFault))]
        [FaultContract(typeof(InvalidServerFault))]
        ValCurs GetCurrencyForDay(DateTime date);

        /// <summary>
        /// Получить курс определенной валюты к рублю, на опеределенную дату date.
        /// </summary>
        /// <param name="date">Дата для которой необходимо вычислить курс валют.</param>
        /// <param name="charCode">Код валюты.</param>
        /// <returns>Численно значение курса валюты charCode к рублю.</returns>
        [OperationContract]
        [FaultContract(typeof(InvalidCurrencyFault))]
        [FaultContract(typeof(InvalidDateFault))]
        [FaultContract(typeof(InvalidServerFault))]
        double GetCurrencyForDayCharCode(DateTime date, string charCode);
    }
}
