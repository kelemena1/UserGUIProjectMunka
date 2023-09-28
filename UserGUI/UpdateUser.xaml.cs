using MySql.Data.MySqlClient;
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

namespace UserGUI
{
    /// <summary>
    /// Interaction logic for UpdateUser.xaml
    /// </summary>
    public partial class UpdateUser : Window
    {

        private int userId;

        public static string connectionString = "SERVER=192.168.56.1;DATABASE=users;USER=root;PASSWORD=password;SSL mode=NONE";
        public UpdateUser(Users user)
        {
            InitializeComponent();
            Id.Text = Convert.ToString(user.Id);
            Name.Text = user.Name;
            DateOnly dateOnlyValue = user.Birth;  
            DateTime dateTimeValue = new DateTime(dateOnlyValue.Year, dateOnlyValue.Month, dateOnlyValue.Day);
            Birth.SelectedDate = dateTimeValue;
            Address.Text = user.Address;




        }

        private void btn_Update(object sender, RoutedEventArgs e)
        {
            string updateSql = "UPDATE user SET Name = @Name, Birth = @Birth, Address = @Address WHERE Id = @Id";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(updateSql, connection))
            {
                cmd.Parameters.AddWithValue("@Name", Name.Text);
                cmd.Parameters.AddWithValue("@Birth", Birth.SelectedDate.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Address", Address.Text);
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt64(Id.Text));

                connection.Open();
                cmd.ExecuteNonQuery();
            }
            Close();
            

        }

        private void btn_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

     
    }
}
