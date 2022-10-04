using AdoHT1.Generators;
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
        /// Имя сервера БД
        /// </summary>
        private string _serverName;

        /// <summary>
        /// Возвращает false, если подключение установленно
        /// </summary>
        private bool _isNotConnected;

        /// <summary>
        /// Возвращает true, если подключение установленно
        /// </summary>
        private bool _isConnected;

        /// <summary>
        /// Строка для вывода информации для пользователя
        /// </summary>
        private string _connectionStatus;


        /************************************************************************************************************************************************************
         ************************************************************************************************************************************************************/
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
