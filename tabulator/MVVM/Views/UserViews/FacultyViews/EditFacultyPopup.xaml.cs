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
using tabulator.MVVM.Views.AdminViews;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for EditFacultyPopup.xaml
    /// </summary>
    public partial class EditFacultyPopup : Window
    {
        DBContext context = DBContext.GetInstance();
        int Id;

        public EditFacultyPopup(int facultyID)
        {
            InitializeComponent();
            Id = facultyID;

            var editFaculty = (from m in context.Faculties where m.Id == facultyID select m).FirstOrDefault();
            NameInput.Text = editFaculty.Name;
            AddressInput.Text = editFaculty.Address;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(NameInput.Text == string.Empty || AddressInput.Text == string.Empty)
            {
                errorText.Text = "Fill all fields!";
                return;
            }

            Faculty faculty = (from m in context.Faculties where m.Id == Id select m).FirstOrDefault();
            faculty.Name = NameInput.Text;
            faculty.Address = AddressInput.Text;
            context.SaveChanges();
            this.Close();
        }

        private void Border_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btnEdit_Click(sender, e);
            }
        }
    }
}
