using AdoHT1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoHT1
{
    internal class OrderTableItem : Order, INotifyPropertyChanged
    {
        /// <summary>
        ///  Поле: Номер клиента в таблице-представлении
        /// </summary>
        private int _CustomerTableNumber;

        /********************************************************************************************************************************************************************************
        ********************************************************************************************************************************************************************************/

        /// <summary>
        /// Свойство: Номер клиента в таблице-представлении
        /// </summary>
        public int CustomerTableNumber
        {
            get => _CustomerTableNumber;
            set
            {
                _CustomerTableNumber = value;
                OnPropertyChanged(nameof(CustomerTableNumber));
            }
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int CustomerId
        {
            get => _customerId;
            set
            {
                _customerId = value;
                OnPropertyChanged(nameof(CustomerId));
            }
        }

        public double Summ
        {
            get => _summ;
            set
            {
                _summ = value;
                OnPropertyChanged(nameof(Summ));
            }
        }

        public string Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        /********************************************************************************************************************************************************************************
         ********************************************************************************************************************************************************************************/
        /// <summary>
        /// Конструктор по умолчанию базового класса Order
        /// </summary>
        public OrderTableItem() : base()
        {

        }

        /// <summary>
        /// Конструктор с параметрами базового класса Order
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="customerId">Идентификатор покупателя</param>
        /// <param name="summ">Сумма заказа</param>
        /// <param name="dateString">Дата заказа</param>
        public OrderTableItem(int id, int customerId, double summ, string dateString) : base(id, customerId, summ, dateString)
        {

        }

        /********************************************************************************************************************************************************************************
         ********************************************************************************************************************************************************************************/
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
