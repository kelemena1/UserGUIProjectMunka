using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;


namespace UserGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        public static List<Users> Datas = new List<Users>();
        public static string connectionString = "SERVER=192.168.56.1;DATABASE=users;USER=root;PASSWORD=password;SSL mode=NONE";


        public static void DataInput()
        {
            string connectionString = "SERVER=192.168.56.1;DATABASE=users;USER=root;PASSWORD=password;SSL mode=NONE";
            string sqlCommand = @"SELECT * FROM user";
            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
            using (MySqlCommand mySqlCommand = new MySqlCommand(sqlCommand, mySqlConnection))
            {
                try
                {
                    mySqlConnection.Open();
                    using (MySqlDataReader myReader = mySqlCommand.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            Users OneUser = new Users(int.Parse(myReader.GetString(0)), myReader.GetString(1), DateOnly.Parse(myReader.GetDateTime(2).ToShortDateString()), myReader.GetString(3));
                            Datas.Add(OneUser);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

     

        public MainWindow()
        {
            
            InitializeComponent();
            DataInput();
            dataGrid.ItemsSource = Datas;
            cmb_Id.ItemsSource = Datas.Select(x => x.Id);
            cmb_Id.SelectedIndex = 0;

        }
        private void RefreshDataGrid()
        {
            dataGrid.ItemsSource = null; 
            dataGrid.ItemsSource = Datas; 
        }
        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            int index = dataGrid.SelectedIndex;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

            try
            {
                mySqlConnection.Open();
                string sqlCommand = $"DELETE FROM users.user WHERE Id = {Datas[index].Id}";

                MySqlCommand mySqlCommand = new MySqlCommand(sqlCommand, mySqlConnection);
                mySqlCommand.ExecuteNonQuery();
                Datas.Remove(Datas[index]);
                dataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            HozzaAd addUserWindow = new HozzaAd();
            addUserWindow.ShowDialog();
            RefreshDataGrid();
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

            try
            {
                
                mySqlConnection.Open();
                string sqlCommand = $"INSERT INTO user (Id, Name, Birth, Address) VALUES ('{Datas[Datas.Count-1].Id}','{Datas[Datas.Count - 1].Name}','{(Datas[Datas.Count - 1].Birth).ToString("yyyy-MM-dd")}','{Datas[Datas.Count-1].Address}');";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlCommand, mySqlConnection);
                mySqlCommand.ExecuteNonQuery();
                dataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Users selectedRecord = Datas.FirstOrDefault(record => record.Id == (int)cmb_Id.SelectedItem);
            UpdateUser updateUserWindow= new UpdateUser(selectedRecord);
            updateUserWindow.ShowDialog();
            Datas.Clear();
            DataInput();
            RefreshDataGrid();
            
            
        }
    }
}

