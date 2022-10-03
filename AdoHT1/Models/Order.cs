using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public int Id
        {
            get;

            set;

        }

        public decimal Summ;

        public DateTime date;

        public int CustomerId;


        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------
        --------------------------------------------------------------------------------------------------------------------------------------------------------------*/
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
