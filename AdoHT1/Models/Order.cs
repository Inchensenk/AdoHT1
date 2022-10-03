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
    internal class Order : INotifyPropertyChanged
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int _id;

        public double _summ;

        public string _date;

        public int _customerId;


        /*****************************************************************************************************************************************
         *****************************************************************************************************************************************/

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public double Summ
        {
            get => _summ;
            set
            {
                _summ=value;
                OnPropertyChanged(nameof(Summ));
            }
        }

        public string Date
        {
            get => _date;
            set
            {
                _date=value;
                OnPropertyChanged(nameof(_date));
            }
        }

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
            CustomerId = int.MaxValue;
            Date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            Summ = double.MaxValue;
        }
        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerId"></param>
        /// <param name="summ"></param>
        /// <param name="dateString"></param>
        public Order(int id, int customerId, string dateString, double summ )
        {
            Id = id;
            CustomerId = customerId;
            Date = dateString;
            Summ = summ;
            
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
