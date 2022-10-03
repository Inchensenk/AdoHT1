using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoHT1
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {

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
