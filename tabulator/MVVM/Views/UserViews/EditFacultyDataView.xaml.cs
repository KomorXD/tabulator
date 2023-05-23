using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Interaction logic for EditFacultyDataView.xaml
    /// </summary>
    public partial class EditFacultyDataView : UserControl
    {
        DBContext context = DBContext.GetInstance();
        public static DataGrid dataGrid;

        public EditFacultyDataView()
        {
            InitializeComponent();
            FacultiesDataGrid.ItemsSource = context.Faculties.ToList();
            dataGrid = FacultiesDataGrid;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            int ID = (FacultiesDataGrid.SelectedItem as Faculty).Id;
            EditFacultyPopup editFaculty = new EditFacultyPopup(ID);
            editFaculty.ShowDialog();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int ID = (FacultiesDataGrid.SelectedItem as Faculty).Id;
            var deleteFaculty = context.Faculties.Where(m => m.Id == ID).Single();
            context.Faculties.Remove(deleteFaculty);
            context.SaveChanges();
            FacultiesDataGrid.ItemsSource = context.Faculties.ToList();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = context.Faculties.Where(x => x.Name.Contains(SearchTextBox.Text) || x.Address.Contains(SearchTextBox.Text)).ToList();
            FacultiesDataGrid.ItemsSource = result;
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            helpPopup.IsOpen = !helpPopup.IsOpen;
        }
    }
}
