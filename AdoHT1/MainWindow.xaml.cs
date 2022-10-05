using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
using SqlConnection = System.Data.SqlClient.SqlConnection;

namespace AdoHT1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection sqlConnection;
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            //Connection();
        }

        //private void Connection()
        //{
        //    sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdoDB"].ConnectionString);
        //    sqlConnection.Open();

        //    if(sqlConnection.State == ConnectionState.Open)
        //    {
        //        MessageBox.Show("Подключение открыто!");
        //    }
        //}
        
       
    }
}
