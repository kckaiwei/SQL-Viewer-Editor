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
using System.Data;

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
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SHOW TABLES;";
                MySqlDataReader Reader;
                Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    string row = "";
                    for (int i = 0; i < Reader.FieldCount; i++)
                        row += Reader.GetValue(i).ToString();
                    table_comboBox.Items.Add(row);
                }

                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void table_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (table_comboBox.SelectedIndex > -1)
            {
                DataTable dataTable = new DataTable();

                try
                {
                    string sql_command = "SELECT * FROM ";
                    sql_command = sql_command + table_comboBox.SelectedItem.ToString();
                    conn = new MySql.Data.MySqlClient.MySqlConnection();
                    conn.ConnectionString = myConnectionString;
                    conn.Open();
                    MySqlCommand command = conn.CreateCommand();
                    command.CommandText = sql_command;

                    //Load data into dataTable object, which can then be imported to dataGrid
                    using (MySqlDataAdapter da = new MySqlDataAdapter(command))
                    {
                        da.Fill(dataTable);
                    }

                    dataGrid.DataContext = dataTable;
                    dataGrid.Name = dataTable.TableName;
                    conn.Close();

                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //Debugging
                //MessageBox.Show(table_comboBox.SelectedItem.ToString());
            }
        }
    }

}
