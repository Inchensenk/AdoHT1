using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoHT1
{
    /// <summary>
    /// Модель хранит данные Пользолвателя
    /// </summary>
    internal class Customer : INotifyPropertyChanged
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id
        {
            get;

            set;

        }

        /// <summary>
        /// Имя
        /// </summary>
        private string _name = null!;

        /// <summary>
        /// Номер телефона
        /// </summary>
        private string _phoneNumber = null!;
       

        /// <summary>
        /// Имя
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
        /// Номер телефона
        /// </summary>
        public string PhoneNumber
        {
            get => _phoneNumber;

            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(_phoneNumber));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
