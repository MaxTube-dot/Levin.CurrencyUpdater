using System.Runtime.Serialization;

namespace CurrencyUpdater
{
    /// <summary>
    /// Указывает на ошибку в дате.
    /// </summary>
    [DataContract]
    class InvalidDateFault
    {
        [DataMember]
        public string CustomError;
        public InvalidDateFault()
        {
        }
        public InvalidDateFault(string error)
        {
            CustomError = error;
        }

    }
}
