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
    internal class Customer : INotifyPropertyChanged
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        private int _id;
        

        /// <summary>
        /// Имя
        /// </summary>
        private string _name;

        /// <summary>
        /// Номер телефона
        /// </summary>
        private string _phoneNumber;

        /*****************************************************************************************************************************************
         *****************************************************************************************************************************************/

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

        /*****************************************************************************************************************************************
        *****************************************************************************************************************************************/

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
