using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestovoeNabiullinVladislav.Events
{
    public static class UserEvents
    {
        public delegate void ClientAdded(Client client);
        public static event ClientAdded clientAddHandler;

        /// <summary>
        /// Добавление клиента
        /// </summary>
        /// <param name="client">Новый Клиент</param>
        public static void OnClientAdded(Client client)
        {
            clientAddHandler?.Invoke(client);
        }

        public delegate void ClientPay(double sum, string currency);
        public static event ClientPay ClientPayHandler;

        /// <summary>
        /// Оплата
        /// </summary>
        /// <param name="sum">Сумма перевода</param>
        /// <param name="currency">Валюта</param>
        public static void OnClientPay(double sum, string currency)
        {
            ClientPayHandler?.Invoke(sum, currency);
        }
    }
}
