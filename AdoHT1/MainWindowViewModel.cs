using AdoHT1.Generators;
using AdoHT1.Models;
using AdoHT1.TablesItems;
using Microsoft.IdentityModel.Tokens;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdoHT1
{
    public  class MainWindowViewModel : INotifyPropertyChanged
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

        /// <summary>
        /// Список покупателей для занесения в БД
        /// </summary>
        private ObservableCollection<CustomerTableItem> _PrimaryCustomerList;

        /// <summary>
        /// Текущий список заказов для занесения в БД
        /// </summary>
        private ObservableCollection<OrderTableItem> _PrimaryOrderList;

        /// <summary>
        /// Текущее подключение
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// Экзембляр генератора юзеров
        /// </summary>
        CustomerGenerate customerGenerate;

        /// <summary>
        /// Экземпляр генератора заказов;
        /// </summary>
        OrderGenerate orderGenerate;

        /// <summary>
        /// Строка для отправки запросов в БД
        /// </summary>
        private string queryString;

        /// <summary>
        /// Имя БД
        /// </summary>
        private readonly string reservedDbName = "SofoianAdoHT1";

        /// <summary>
        /// Свойство для удаления выбранного элемента из таблицы покупателей
        /// </summary>
        private CustomerTableItem _SelectedCustomer;

        /// <summary>
        /// поле для ввода имени пользователя
        /// </summary>
        private string _NameAddCustomerInputField;

        /// <summary>
        ///  Поле ввода для номера покупателя;
        /// </summary>
        private string _PhoneNumberAddCustomerInputField;

        /// <summary>
        /// Поле ввода для номера заказчика в соответствующей таблице
        /// </summary>
        private string _CustomerNumberAddOrderInputField;

        /// <summary>
        /// Поле ввода для суммы заказа в таблице;
        /// </summary>
        private string _SummAddOrderInputField;

        /// <summary>
        /// Поле ввода для даты в таблице;
        /// </summary>
        private string _DateAddOrderInputField;

        /// <summary>
        /// Значение для этикетки "Min"
        /// </summary>
        private string _MinOderPriceValue;

        /// <summary>
        /// Значение для этикетки "Max"
        /// </summary>
        private string _MaxOderPriceValue;

        /// <summary>
        /// Значение для этикетки "Overall"
        /// </summary>
        private string _OverallOderPriceValue;

        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        private string connectionString = "";

        /************************************************************************************************************************************************************
        ************************************************************************************************************************************************************/
      
        /// <summary>
       /// Свойство Имя сервера
       /// </summary>
        public string ServerName
        {
            get => _serverName;
            set => _serverName = value;
        }

        /// <summary>
        /// Свойство для удаления выбранного элемента из таблицы покупателей
        /// </summary>
        public CustomerTableItem SelectedCustomer
        {
            get => _SelectedCustomer;
            set
            {
                _SelectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }

        public string NameAddCustomerInputField
        {
            get=> _NameAddCustomerInputField;
            set
            {
                _NameAddCustomerInputField = value;
                OnPropertyChanged(nameof(NameAddCustomerInputField));
            }
        }

        public string PhoneNumberAddCustomerInputField
        {
            get => _PhoneNumberAddCustomerInputField;
            set
            {
                _PhoneNumberAddCustomerInputField = value;
                OnPropertyChanged(nameof(PhoneNumberAddCustomerInputField));
            }
        }

        public string CustomerNumberAddOrderInputField
        {
            get => _CustomerNumberAddOrderInputField;
            set
            {
                _CustomerNumberAddOrderInputField = value;
                OnPropertyChanged(nameof(CustomerNumberAddOrderInputField));
            }
        }

        public string SummAddOrderInputField
        {
            get => _SummAddOrderInputField;
            set
            {
                _SummAddOrderInputField= value; 
                OnPropertyChanged(nameof(SummAddOrderInputField));
            }
        }

        public string DateAddOrderInputField
        {
            get=> _DateAddOrderInputField; 
            set
            {
                _DateAddOrderInputField = value;
                OnPropertyChanged(nameof(_DateAddOrderInputField));
            }
        }

        public string MinOderPriceValue
        {
            get => _MinOderPriceValue;
            set
            {
                _MinOderPriceValue = value;
                OnPropertyChanged(nameof(MinOderPriceValue));
            }
        }

        public string MaxOderPriceValue
        {
            get => _MaxOderPriceValue;
            set
            {
                _MaxOderPriceValue = value;
                OnPropertyChanged(nameof(MaxOderPriceValue));
            }
        }

        public string OverallOderPriceValue
        {
            get => _OverallOderPriceValue; 
            set
            {
                _OverallOderPriceValue = value;
                OnPropertyChanged(nameof(OverallOderPriceValue));
            }
        }

        public ObservableCollection<CustomerTableItem> PrimaryCustomerList
        {
            get => _PrimaryCustomerList;
            set
            {
                _PrimaryCustomerList = value;
                OnPropertyChanged(nameof(PrimaryCustomerList));
            }
        }

        public ObservableCollection<OrderTableItem> PrimaryOrderList
        {
            get => _PrimaryOrderList;
            set
            {
                _PrimaryOrderList = value;
                OnPropertyChanged(nameof(PrimaryOrderList));
            }
        }
        /************************************************************************************************************************************************************
        ************************************************************************************************************************************************************/

        /// <summary>
        /// Делегат хендлера элемента "ConntectionButton"
        /// </summary>
        public DelegateCommand OnConnectButtonClickCommand { get; }

        /// <summary>
        /// Делегат хендлера элемента "CreationButton"
        /// </summary>
        public DelegateCommand OnFillButtonClickCommand { get; }

        /// <summary>
        /// Делегат хендлера элемента "ClearTablesButton"
        /// </summary>
        public DelegateCommand OnClearButtonClickCommand { get; }

        /// <summary>
        /// Команда удаления элемента из таблицы Customers
        /// </summary>
        public DelegateCommand OnCustomerDataGridDeleteKeyDownCommand { get; }


        /// <summary>
        /// Команда добавления пользователя при клике по кнопке "Add Customer";
        /// </summary>
        public DelegateCommand OnAddCustomerButtonClickCommand { get; }


        /// <summary>
        /// Команда добавления заказа при клике по кнопке "Add Order";
        /// </summary>
        public DelegateCommand OnAddOrderButtonClickCommand { get; }

        /************************************************************************************************************************************************************
        ************************************************************************************************************************************************************/

        public bool IsNotConnected
        {
            get => _isNotConnected;
            set
            {
                _isNotConnected = value;
                OnPropertyChanged(nameof(IsNotConnected));
            }
        }

        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                _isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }

        public string ConnectionStatus
        {
            get => _connectionStatus;
            set
            {
                _connectionStatus = value;
                OnPropertyChanged(nameof(ConnectionStatus));
            }
        }
        /*
         *******************************************************************************************************************************************************************************/
        /// <summary>
        /// Подключение к БД
        /// </summary>
        private async void OnConnectButtonClickAsync()
        {

            try
            {
                connectionString = $"Server={ServerName};Database = master;Trusted_Connection=true;Encrypt=false";

                connection = new SqlConnection(connectionString);

                ConnectionStatus = "Connecting .....";

                await connection.OpenAsync();

                ToggleConnectionState();


                MessageBox.Show($"Connection Established.\nId: {connection.ClientConnectionId}", "Success.", MessageBoxButton.OK, MessageBoxImage.Information);

                try
                {
                    ConnectionStatus = $"Connected to {ServerName}.";

                    ExecuteSQLCommand($"USE {reservedDbName};");

                    RefreshLists();
                }
                catch (Exception e)
                {
                    ExecuteSQLCommand($"USE master; CREATE DATABASE {reservedDbName};");
                }
                finally
                {
                    try
                    {
                        ExecuteSQLCommand($"USE {reservedDbName}; SELECT * FROM Customers;");

                        RefreshLists();
                    }
                    catch (Exception e)
                    {
                        ExecuteSQLCommand(

                        $"USE {reservedDbName};" +
                        $"CREATE TABLE {reservedDbName}..Customers" +
                        "(" +
                        "   [Id] INT PRIMARY KEY IDENTITY(0,1)," +
                        "   [Name] NVARCHAR(24) NOT NULL," +
                        "   [PhoneNumber] NVARCHAR(14)" +
                        ")" +

                        $"USE {reservedDbName};" +
                        $"CREATE TABLE {reservedDbName}..Orders" +
                        "(" +
                        "   [Id] INT PRIMARY KEY IDENTITY(0,1)," +
                        "   [CustomerId] INT FOREIGN KEY REFERENCES Customers(Id)," +
                        "   [Summ] FLOAT," +
                        "   [Date] NVARCHAR(24)" +
                        ")"
                    );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Если имя указано правильно, попробуй добавить \".\\\\\" в строку подключения (LOGIC - HANDLERS - View Controls)\nSomething went wrong. Please, try another name.\nIf you are sure of this name, please check your server settings.\n\nException: {ex.Message}.", "Error. Server not found.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Создание и заполнение базы данных
        /// </summary>
        private void OnFillButtonClick()
        {
            Customer customer;
            Order order;

            Random random = new Random();
            int nRandomCustomersQuantity = random.Next(10, 30);

            for (int i = 0, iSize = nRandomCustomersQuantity; i < iSize; ++i)
            {
                //CustomerList.Add(CustomerGenerate.GetCustomer());
                customer = customerGenerate.GetRandomCustomer();
                queryString =
                    $"USE {reservedDbName}; INSERT INTO Customers (Name, PhoneNumber) VALUES(N'{customer.Name}','{customer.PhoneNumber}');";
                TryExecuteSQLCommand(queryString);
            }

            /* Таблица заказов связана вторичным ключом с таблоицей пользователей, чтобы не получить Exception,
            нам необходимо узнать id пользователей;*/
            int[] CustomersIdSchemeForRandomOrders = GetCurrentCustomersIdInfo();

            
            for (int i = 0, iSize = nRandomCustomersQuantity; i < iSize; ++i)
            {
                order = orderGenerate.GetRandomOrder(CustomersIdSchemeForRandomOrders);
                queryString =
                    $"USE {reservedDbName}; INSERT INTO Orders (CustomerId, Summ, Date) VALUES('{order.CustomerId}','{Math.Round(order.Summ, 1).ToString(CultureInfo.InvariantCulture)}', '{order.Date.ToString()}');";
                TryExecuteSQLCommand(queryString);
            }

            ShowSuccessChangesMessageBox();

            RefreshLists();

        }

        /// <summary>
        /// Launch table clear procedure;
        /// <br />
        /// Запустить процесс очистки таблиц;
        /// </summary>
        private async void OnClearButtonClickAsync()
        {
            var a = PrimaryCustomerList;
            try
            {
                queryString = $"USE {reservedDbName} DELETE FROM Orders; DELETE FROM Customers";

                await ExecuteSQLCommandAsync(queryString);
            }
            catch (Exception e)
            {

            }

            ShowSuccessChangesMessageBox();

            RefreshLists();
        }

        /// <summary>
        /// Хендлер комманды удаления элемента из таблицы 'Customers';
        /// </summary>
        private void OnCustomerDataGridDeleteKeyDown()
        {
            ObservableCollection<CustomerTableItem> tempCustomerList = new ObservableCollection<CustomerTableItem>();

            SqlCommand command;

            foreach (var customer in PrimaryCustomerList)
            {
                if (!customer.Equals(SelectedCustomer))
                {
                    tempCustomerList.Add(customer);
                }
                else
                {
                    foreach (Order order in PrimaryOrderList)
                    {
                        if (order.CustomerId == customer.Id)
                        {
                            queryString = $"USE {reservedDbName}; DELETE FROM Orders WHERE CustomerId = {customer.Id};";

                            ExecuteSQLCommand(queryString);
                        }
                    }

                    queryString = $"USE {reservedDbName}; DELETE FROM Customers WHERE Id = {customer.Id};";

                    ExecuteSQLCommand(queryString);
                }
            }

            PrimaryCustomerList = tempCustomerList;

            RefreshLists();
        }


        /// <summary>
        /// Хендлер команды нажатия на кпопку "Add";
        /// </summary>
        private void OnAddCustomerButtonClick()
        {
            if (NameAddCustomerInputField.Length <= 24)
            {
                SqlParameter phoneNumberParam = new SqlParameter("@phone", PhoneNumberAddCustomerInputField);

                SqlParameter nameParam = new SqlParameter("@name", NameAddCustomerInputField);

                if (PhoneNumberAddCustomerInputField.StartsWith("+7") && PhoneNumberAddCustomerInputField.Length == 12)
                {
                    queryString = $"USE {reservedDbName}; INSERT INTO Customers (Name, PhoneNumber) VALUES (@name, @phone);";

                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.Add(nameParam);

                    command.Parameters.Add(phoneNumberParam);

                    command.ExecuteNonQuery();

                    ShowSuccessChangesMessageBox();

                    RefreshLists();
                }
                else
                {
                    MessageBox.Show("Wrong phone number format. It should start with '+7' and include 13 symbols at max.", "Wrong Phone Number format.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Хендлер команды нажатия на кпопку "Add Order";
        /// </summary>
        private void OnAddOrderButtonClick()
        {
            // Check if we have that customerNumber;
            List<int> NumbersList = new List<int>();
            PrimaryCustomerList.ToList().ForEach(customer => NumbersList.Add(customer.TableNumber));
            int customerNumberTryParse = 0;
            int CustomerIdByTableNumber = 0;

            try
            {
                customerNumberTryParse = int.Parse(CustomerNumberAddOrderInputField);

                CustomerIdByTableNumber = PrimaryCustomerList.ToList().Find(customer => customer.TableNumber == int.Parse(CustomerNumberAddOrderInputField)).Id;
            }
            catch (Exception e)
            {
                MessageBox.Show("Please, enter existing digital number.", "Wrong Customer Number.", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            if (NumbersList.Contains(customerNumberTryParse))
            {
                string[] datetimeString = DateAddOrderInputField.Split("-");
                int[] datetimeInt = new int[datetimeString.Length];

                try
                {
                    SqlParameter idParam = new SqlParameter("@customerId", CustomerIdByTableNumber);
                    SqlParameter summParam = new SqlParameter("@summ", SummAddOrderInputField);
                    SqlParameter dateParam = new SqlParameter("@date", DateTime.ParseExact(DateAddOrderInputField, "dd-mm-yyyy", CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture));


                    queryString = $"USE {reservedDbName}; INSERT INTO Orders (CustomerId, Summ, Date) VALUES " +

                        $"(@customerId, @summ, @date);";

                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(summParam);
                    command.Parameters.Add(dateParam);

                    command.ExecuteNonQuery();

                    ShowSuccessChangesMessageBox();

                    RefreshLists();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please, enter proper date (dd-mm-yyyy)", "Wrong Customer Number.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please, use existing customer number", "Wrong Customer Number.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /************************************************************************************************************************************************************
         ************************************************************************************************************************************************************/
        /// <summary>
        /// Когда изменяется сам список пользователей;
        /// </summary>
        private void OnCustomerListCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= OnCustomerListItemPropertyChanged;
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += OnCustomerListItemPropertyChanged;
            }
        }

        /// <summary>
        /// Когда изменяется сам список заказов;
        /// </summary>
        private void OnOrderListCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= OnOrderListItemPropertyChanged;
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += OnOrderListItemPropertyChanged;
            }
        }

        /// <summary>
        /// Когда изменяется объект в списке пользователей;
        /// </summary>
        private void OnCustomerListItemPropertyChanged(object? sender, EventArgs e)
        {
            ObservableCollection<CustomerTableItem> tempCustomersForComparison = new ObservableCollection<CustomerTableItem>();

            SqlCommand command = new SqlCommand($"SELECT * FROM Customers", connection);

            CustomerTableItem customerRef = new CustomerTableItem();

            // create copy of db data in a list;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    customerRef = new CustomerTableItem(Id: reader.GetInt32(0), Name: reader.GetString(1), PhoneNumber: reader.GetString(2));
                    tempCustomersForComparison.Add(customerRef);
                }
            }

            // for each view collection item;
            foreach (CustomerTableItem newCustomer in PrimaryCustomerList)
            {
                // for each db item;
                foreach (CustomerTableItem oldCustomer in tempCustomersForComparison)
                {
                    // if they differ;
                    if (oldCustomer.Id == newCustomer.Id && !oldCustomer.Equals(newCustomer))
                    {
                        // push changes;
                        ExecuteSQLCommand($"USE {reservedDbName}; UPDATE Customers SET [Name] = '{newCustomer.Name}', [PhoneNumber] = '{newCustomer.PhoneNumber}' WHERE Id = {oldCustomer.Id};");

                    }
                }
            }
        }

        /// <summary>
        /// Когда изменяется объект в списке заказов;
        /// </summary>
        public void OnOrderListItemPropertyChanged(object? sender, EventArgs e)
        {
            ObservableCollection<OrderTableItem> tempOrdersForComparison = new ObservableCollection<OrderTableItem>();

            SqlCommand command = new SqlCommand($"SELECT * FROM Orders", connection);

            OrderTableItem orderRef = new OrderTableItem();

            // create copy of db data in a list;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    orderRef = new OrderTableItem(id: reader.GetInt32(0), customerId: reader.GetInt32(1), summ: reader.GetSqlDouble(2).Value, dateString: reader.GetString(3));
                    tempOrdersForComparison.Add(orderRef);
                }
            }

            // for each view collection item;
            foreach (OrderTableItem newOrder in PrimaryOrderList)
            {
                // for each db item;
                foreach (OrderTableItem oldOrder in tempOrdersForComparison)
                {
                    // if they differ;
                    if (oldOrder.Id == newOrder.Id && !oldOrder.Equals(newOrder))
                    {
                        // push changes;
                        ExecuteSQLCommand($"USE {reservedDbName}; UPDATE Orders SET [CustomerId] = '{newOrder.CustomerId}', [Summ] = '{Math.Round(newOrder.Summ, 1).ToString(CultureInfo.InvariantCulture)}', [Date] = '{newOrder.Date.ToString(CultureInfo.InvariantCulture)}' WHERE Id = {oldOrder.Id};");
                    }
                }
            }
        }

        /************************************************************************************************************************************************************
         ************************************************************************************************************************************************************/

        /// <summary>
        /// Изменить статус подключения;
        /// </summary>
        private void ToggleConnectionState()
        {
            bool bTemp = IsConnected;
            IsConnected = IsNotConnected;
            IsNotConnected = bTemp;
        }

        /// <summary>
        /// Launch query execution;
        /// <br />
        /// Запустить выполнение запроса;
        /// </summary>
        /// <param name="QueryString">
        /// A string that represents the query;
        /// <br />
        /// Строка, представляющая собой запрос;
        /// </param>
        private async Task ExecuteSQLCommandAsync(string QueryString)
        {
            if (IsConnected)
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Failed to execute your querry.\nException: {e.Message}", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Executes SQL command in a non-async way;
        /// <br />
        /// Запустить комманду SQL в однопоточном режиме;
        /// </summary>
        /// <param name="QueryString">
        /// A string that represents the query;
        /// <br />
        /// Строка, представляющая собой запрос;
        /// </param>
        private void TryExecuteSQLCommand(string QueryString)
        {
            if (IsConnected)
            {
                try
                {
                    SqlCommand command = new SqlCommand(QueryString, connection);

                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Failed to execute your querry.\nException: {e.Message}", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //dfghdfhdfh

        /// <summary>
        /// Execute SQL command in non-async mode;
        /// <br />
        /// Выполнить SQL запрос в однопоточном режиме;
        /// </summary>
        /// <param name="QueryString">
        /// The string which represents SQL quety;
        /// <br />
        /// Строка SQL-запроса;
        /// </param>
        private void ExecuteSQLCommand(string QueryString)
        {
            if (IsConnected)
            {
                SqlCommand command = new SqlCommand(QueryString, connection);

                command.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Show a message box whenever query ends with successful result;
        /// <br />
        /// Отобразить "MessageBox" каждый раз, когда запрос заканчивается успешно;
        /// </summary>
        private void ShowSuccessChangesMessageBox()
        {
            MessageBox.Show($"Data changed successfully. Please, check your server.", "Success.", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        /// <summary>
        /// Connect to the 'Customers' table and get list of current ids;
        /// <br />
        /// Подключиться к таблице "Customers" и получить список актуальных id;
        /// </summary>
        /// <returns>
        /// An array of int 'Id' values;
        /// <br />
        /// Массив целых значений "Id";
        /// </returns>
        private int[] GetCurrentCustomersIdInfo()
        {
            List<int> idsList = new List<int>();

            SqlCommand command = new SqlCommand($"SELECT * FROM Customers", connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    idsList.Add(reader.GetInt32(0));
                }
            }

            return idsList.ToArray();
        }





        ///
        /// Refresh tables;
        ///

        /// <summary>
        /// Pull data from db to collection;
        /// <br />
        /// Достать данные из бд в коллекцию;
        /// </summary>
        private void RefreshCustomerList()
        {
            PrimaryCustomerList.Clear();

            SqlCommand command = new SqlCommand($"USE {reservedDbName}; SELECT * FROM Customers", connection);

            CustomerTableItem Customer = new CustomerTableItem();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Customer = new CustomerTableItem(Id: reader.GetInt32(0), Name: reader.GetString(1), PhoneNumber: reader.GetString(2));
                    PrimaryCustomerList.Add(Customer);
                }
            }

            RefreshTableItemsNumbers();
        }


        /// <summary>
        /// Pull data from db to collection;
        /// <br />
        /// Достать данные из бд в коллекцию;
        /// </summary>
        private void RefreshOrderList()
        {
            PrimaryOrderList.Clear();

            SqlCommand command = new SqlCommand($"USE {reservedDbName}; SELECT * FROM Orders", connection);

            OrderTableItem order = new OrderTableItem();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    order = new OrderTableItem(id: reader.GetInt32(0), customerId: reader.GetInt32(1), summ: reader.GetSqlDouble(2).Value, dateString: reader.GetString(3));
                    PrimaryOrderList.Add(order);
                }
            }

            RefreshTableItemsNumbers();
        }


        /// <summary>
        /// 'Refresh' all lists;
        /// <br />
        /// Обновить оба списка;
        /// </summary>
        private void RefreshLists()
        {
            RefreshCustomerList();
            RefreshOrderList();
            RefreshLabelValues();
        }


        /// <summary>
        /// Refresh view-table numbers to avoid id exposing for the Customer;
        /// <br />
        /// Обновить номера значений в таблице представления, чтобы избежать раскрытия id для пользователя;
        /// </summary>
        private void RefreshTableItemsNumbers()
        {
            int i = 1;
            foreach (CustomerTableItem customerWrapper in PrimaryCustomerList)
            {
                customerWrapper.TableNumber = i++;
                foreach (OrderTableItem orderWrapper in PrimaryOrderList)
                {
                    if (orderWrapper.CustomerId.Equals(customerWrapper.Id))
                    {
                        orderWrapper.CustomerTableNumber = customerWrapper.TableNumber;
                    }
                }
            }
        }


        /// <summary>
        /// Refresh those label values that show min, max and overall values;
        /// <br />
        /// Обновить значения для лейблов, которые показывают минимальное, максимальное и среднее значения;
        /// </summary>
        private void RefreshLabelValues()
        {
            if (!PrimaryOrderList.IsNullOrEmpty())
            {
                var tempColl = PrimaryOrderList.ToList().Select(order => order.Summ).ToList();
                MinOderPriceValue = tempColl.Min().ToString();
                MaxOderPriceValue = tempColl.Max().ToString();
                OverallOderPriceValue = tempColl.Sum().ToString("n2");
            }
            else
            {
                MinOderPriceValue = "0";
                MaxOderPriceValue = "0";
                OverallOderPriceValue = "0";
            }
        }

        /************************************************************************************************************************************************************
        ************************************************************************************************************************************************************/

        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MainWindowViewModel()
        {
            // Debug;
            //ServerName = "DoronovIV";


            // Button commands;
            OnConnectButtonClickCommand = new DelegateCommand(OnConnectButtonClickAsync);
            OnFillButtonClickCommand = new DelegateCommand(OnFillButtonClick);
            OnClearButtonClickCommand = new DelegateCommand(OnClearButtonClickAsync);
            OnCustomerDataGridDeleteKeyDownCommand = new DelegateCommand(OnCustomerDataGridDeleteKeyDown);
            OnAddCustomerButtonClickCommand = new DelegateCommand(OnAddCustomerButtonClick);
            OnAddOrderButtonClickCommand = new DelegateCommand(OnAddOrderButtonClick);


            // Connection status;
            IsNotConnected = true;
            IsConnected = false;
            ConnectionStatus = "Waiting for connection.";

            // Initializing lists;
            PrimaryCustomerList = new ObservableCollection<CustomerTableItem>();
            PrimaryOrderList = new ObservableCollection<OrderTableItem>();

            // Adding events for Customer input handling;
            PrimaryCustomerList.CollectionChanged += OnCustomerListCollectionChanged;
            PrimaryOrderList.CollectionChanged += OnOrderListCollectionChanged;

            // Initializing gemerator instances for object generating;
            customerGenerate = new CustomerGenerate();
            orderGenerate = new OrderGenerate();


            // Initializing label strings;
            MinOderPriceValue = 0.ToString();
            MaxOderPriceValue = 0.ToString();
            OverallOderPriceValue = 0.ToString();
        }

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
