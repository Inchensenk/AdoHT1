using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoHT1.Models
{
    /// <summary>
    /// Модель хранит данные Пользолвателя
    /// </summary>
    public class Customer : INotifyPropertyChanged
    {
        /// <summary>
        /// Поле: Идентификатор
        /// </summary>
        private int _id;
        
        /// <summary>
        /// Поле: Имя
        /// </summary>
        private string _name = null!;

        /// <summary>
        /// Поле: Номер телефона
        /// </summary>
        private string _phoneNumber  = null!;
        private int maxValue;
        private Customer customer;
        private string v;

        /*****************************************************************************************************************************************
         *****************************************************************************************************************************************/
        /// <summary>
        /// Свойство: Идентификатор покупателя
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
        /// Свойство: Имя
        /// </summary>
        public string Name
        {
            get => _name;

            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Свойство: Номер телефона
        /// </summary>
        public string PhoneNumber
        {
            get => _phoneNumber;

            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        /*****************************************************************************************************************************************
        *****************************************************************************************************************************************/

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Customer()
        {
            this.Id = int.MaxValue;
            this.Name = "";
            this.PhoneNumber = "";
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="Id">Идентификатор</param>
        /// <param name="Name">Имя покупателя</param>
        /// <param name="PhoneNumber">Номер телефона покупателя</param>
        public Customer(int Id, string Name, string PhoneNumber) : this()
        {
            this.Id = Id;
            this.Name = Name;
            this.PhoneNumber = PhoneNumber;
        }

        public Customer(int maxValue, Customer customer, string v)
        {
            this.maxValue = maxValue;
            this.customer = customer;
            this.v = v;
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
        /// <param name="propertyName">Имя свойства</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
