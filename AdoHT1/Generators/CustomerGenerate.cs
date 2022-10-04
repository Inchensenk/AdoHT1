using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdoHT1.Models;

namespace AdoHT1.Generators
{
    /// <summary>
    /// Генерация пользователей 
    /// </summary>
    public class CustomerGenerate
    {
        /// <summary>
        /// Список имён покупателей
        /// </summary>
        private List<string> _customerNameList = null!;

        private Random _random = null!;

        /// <summary>
        /// Получение рандомного покупателя
        /// </summary>
        /// <returns></returns>
        public Customer GetRandomCustomer()
        {
            return new Customer(int.MaxValue, _customerNameList[_random.Next(0, _customerNameList.Count)], GetRandomPhoneNumber());
        }

        /// <summary>
        /// Получение пути к файлу с именами покупателей
        /// </summary>
        /// <param name="filePath">Путь к файлу с именами покупателей</param>
        /// <returns></returns>
        private static List<string> GetCustomerNameList(string filePath)
        {
            List<string> customerNameList = new();
            customerNameList = File.ReadAllLines("../../../.data/Customer/CustomersNames.txt").ToList();
            return customerNameList;
        }

        /// <summary>
        /// Рандомайзер для генерации номера телефона
        /// </summary>
        /// <returns>сгенерированный номер телефона</returns>
        public string GetRandomPhoneNumber()
        {
            string result = "";

            string prefix = "+7";

            //string operCod = "(919)";

            long minValue = 1000000000;

            long maxValue = 9999999999;

            long INumber = _random.NextInt64(minValue, maxValue);

            result = prefix + INumber.ToString();

            return result;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public CustomerGenerate()
        {
            _random = new();
            _customerNameList = new();
            _customerNameList = GetCustomerNameList(".data\\Customer\\CustomersNames.txt");
        }
    }
}
