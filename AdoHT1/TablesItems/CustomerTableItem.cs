using AdoHT1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdoHT1.TablesItems
{
    /// <summary>
    /// Обёртка над классом Customer, для его нумерации в коллекции;
    /// </summary>
    public class CustomerTableItem : Customer, INotifyPropertyChanged
    {
        /// <summary>
        /// Порядковый номер во view-таблице "Customers"
        /// </summary>
        private int _TableNumber = 0;

        /***********************************************************************************************************************************
        ***********************************************************************************************************************************/
        /// <summary>
        /// Свойство для отображения порядкового номера покупателя во view-таблице 
        /// </summary>
        public int TableNumber
        {
            get => _TableNumber;
            set
            {
                _TableNumber = value;
                OnPropertyChanged(nameof(TableNumber));
            }
        }

        public int Id
        {
            get => _id;
            set
            {
                _id=value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name=value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
        /***********************************************************************************************************************************
        ***********************************************************************************************************************************/

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public CustomerTableItem() : base()
        {

        }

        /// <summary>
        /// конструктор с параметрами
        /// </summary>
        /// <param name="Id">Идентификатор покупателя</param>
        /// <param name="Name">Имя</param>
        /// <param name="PhoneNumber">Номер телефона</param>
        public CustomerTableItem(int Id, string Name, string PhoneNumber) : base(Id, Name, PhoneNumber)
        {

        }

        /***********************************************************************************************************************************
         ***********************************************************************************************************************************/
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
