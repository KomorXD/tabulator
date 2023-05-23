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
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for EditUserPopup.xaml
    /// </summary>
    public partial class EditUserPopup : Window
    {
        DBContext context = DBContext.GetInstance();
        int Id;

        public EditUserPopup(int UserID)
        {
            InitializeComponent();
            Id = UserID;

            var editUser = (from m in context.Users where m.Id == UserID select m).FirstOrDefault();
            UsernameInput.Text = editUser.Username;
            NameInput.Text = editUser.Name;
            SurnameInput.Text = editUser.Surname;
            PasswordInput.Text = editUser.Password;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            User user = (from m in context.Users where m.Id == Id select m).FirstOrDefault();
            user.Name = NameInput.Text;
            user.Password = PasswordInput.Text;
            user.Surname = SurnameInput.Text;
            user.Username = UsernameInput.Text;
            context.SaveChanges();
            EditUserDataView.dataGrid.ItemsSource = context.Users.ToList();
            this.Close();
        }
    }
}
