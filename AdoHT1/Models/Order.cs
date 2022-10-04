using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoHT1.Models
{
    /// <summary>
    /// Модель. Хранит данные о заказах
    /// </summary>
    public class Order : INotifyPropertyChanged
    {
        /// <summary>
        /// Поле: Идентификатор
        /// </summary>
        public int _id;

        /// <summary>
        /// Поле: Сумма заказа
        /// </summary>
        public double _summ;

        /// <summary>
        /// Поле: Дата заказа
        /// </summary>
        public string _date;

        /// <summary>
        /// Поле: Идентификатоор покупателя
        /// </summary>
        public int _customerId;


        /*****************************************************************************************************************************************
         *****************************************************************************************************************************************/

        /// <summary>
        /// Свойство: Идентификатор заказа
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        /// <summary>
        /// Свойство: Сумма заказа
        /// </summary>
        public double Summ
        {
            get => _summ;
            set
            {
                _summ=value;
                OnPropertyChanged(nameof(Summ));
            }
        }

        /// <summary>
        /// Свойство: Дата заказа
        /// </summary>
        public string Date
        {
            get => _date;
            set
            {
                _date=value;
                OnPropertyChanged(nameof(_date));
            }
        }

        /// <summary>
        /// Свойство: Идентификатор покупателя
        /// </summary>
        public int CustomerId
        {
            get => _customerId;
            set
            {
                _customerId=value;
                OnPropertyChanged(nameof(CustomerId));
            }
        }


        /*****************************************************************************************************************************************
         *****************************************************************************************************************************************/
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Order()
        {
            Id = int.MaxValue;
            Summ = double.MaxValue;
            Date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            CustomerId = int.MaxValue;
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="summ">Сумма заказа</param>
        /// <param name="dateString">Дата заказа</param>
        /// /// <param name="customerId">Идентификатор покупателя</param>
        public Order(int id, int customerId, double summ, string dateString)
        {
            Id = id;
            CustomerId = customerId;
            Summ = summ;
            Date = dateString;
        }

        /*****************************************************************************************************************************************
        *****************************************************************************************************************************************/

        /// <summary>
        /// Делегат-обработчик события PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Метод-обработчик делегата PropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
