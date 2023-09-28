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
    /// Interaction logic for HozzaAd.xaml
    /// </summary>
    public partial class HozzaAd : Window
    {
        public HozzaAd()
        {
            InitializeComponent();
        }
      
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            int id;
            if (int.TryParse(txtId.Text, out id))
            {
                DateOnly now = new DateOnly();
                now = DateOnly.FromDateTime(DateTime.Now);
                string name = txtName.Text;
                DateOnly birth;
                if (dpBirth.SelectedDate.HasValue)
                {
                    birth = DateOnly.FromDateTime(dpBirth.SelectedDate.Value);
                }
                else
                {
                    birth = DateOnly.FromDateTime(DateTime.Now);
                }

                string address = txtAddress.Text;

                Users newUser = new Users(id, name, birth, address);
                MainWindow.Datas.Add(newUser); 

                
               
                Close(); 
            }
            else
            {
                MessageBox.Show("Hibás Id formátum.");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close(); 
        }

    }
}
