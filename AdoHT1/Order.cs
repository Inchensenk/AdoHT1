using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoHT1
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

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
