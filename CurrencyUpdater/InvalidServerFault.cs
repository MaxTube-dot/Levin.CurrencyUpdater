using System.Runtime.Serialization;

namespace CurrencyUpdater
{
    /// <summary>
    /// Указывает на ошибку в доступе к серверу.
    /// </summary>
    [DataContract]
    class InvalidServerFault
    {
        [DataMember]
        public string CustomError;
        public InvalidServerFault()
        {
        }
        public InvalidServerFault(string error)
        {
            CustomError = error;
        }

    }
}
