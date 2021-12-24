using System.Runtime.Serialization;

namespace CurrencyUpdater
{
    /// <summary>
    /// Указывает на ошибку в коде валюты.
    /// </summary>
    [DataContract]
    class InvalidCurrencyFault
    {
        [DataMember]
        public string CustomError;
        public InvalidCurrencyFault()
        {
        }
        public InvalidCurrencyFault(string error)
        {
            CustomError = error;
        }

    }
}
