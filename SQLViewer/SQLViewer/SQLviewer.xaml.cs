using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;

namespace SQLViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string serverURI = uri_form.Text;
            string user_cred = user_form.Text;
            string pass_cred = pass_form.Password;
            string db = db_form.Text;

            sql_object sqlobj = new sql_object();

            sqlobj.login = user_cred;
            sqlobj.password = pass_cred;
            sqlobj.uri = serverURI;
            sqlobj.database = db;

            myConnectionString = $"server={serverURI};uid={user_cred};" +
                $"pwd={pass_cred};database = {db};";

            try
            {
                //testing connection for login before proceeding
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                editor_window win2 = new editor_window(sqlobj);
                win2.Show();
                this.Close();
            } catch (MySql.Data.MySqlClient.MySqlException ex){
                MessageBox.Show(ex.Message);
            }

        }
    }
}
