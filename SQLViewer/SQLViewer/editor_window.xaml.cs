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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace SQLViewer
{
    /// <summary>
    /// Interaction logic for editor_window.xaml
    /// </summary>
    public partial class editor_window : Window
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        public editor_window(sql_object sqlobj)
        {
            InitializeComponent();
            myConnectionString = $"server={sqlobj.uri};uid={sqlobj.login};" +
    $"pwd={sqlobj.password};database = {sqlobj.database};";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void table_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
