using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdoHT1.Models;

namespace AdoHT1.Generators
{
    public class CustomerGenerate
    {
        /// <summary>
        /// Список имён покупателей
        /// </summary>
        private List<Customer> _customerNameList;

        private Random _random;

        public Customer GetRandomCustomer()
        {
            return new Customer(int.MaxValue, _customerNameList[_random.Next(0, _customerNameList.Count)], GetRandomPhoneNumber());
        }

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
    }
}
