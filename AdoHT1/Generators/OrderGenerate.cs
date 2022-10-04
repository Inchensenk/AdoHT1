using AdoHT1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoHT1.Generators
{
    /// <summary>
    /// Генерация заказов
    /// </summary>
    public class OrderGenerate
    {
        private Random random;


        /// <summary>
        /// Получение нового рандомного заказа
        /// </summary>
        /// <param name="ptrUsersIds"></param>
        /// <returns></returns>
        public Order GetRandomOrder(int[] ptrUsersIds)
        {
            return new Order(id: int.MaxValue, customerId: random.Next(ptrUsersIds[0], ptrUsersIds[random.Next(0, ptrUsersIds.Length)]), summ: random.NextDouble(), dateString: DateTime.Now.ToString(CultureInfo.InvariantCulture)); ;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public OrderGenerate()
        {
            random = new Random();
        }

    }
}
