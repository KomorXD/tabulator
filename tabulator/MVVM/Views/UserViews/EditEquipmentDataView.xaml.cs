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

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for EditEquipmentDataView.xaml
    /// </summary>
    public partial class EditEquipmentDataView : UserControl
    {
        DBContext context = DBContext.GetInstance();
        public static DataGrid dataGrid;

        public EditEquipmentDataView()
        {
            InitializeComponent();
            EquipmentDataGrid.ItemsSource = context.Equipment.ToList();
            dataGrid = EquipmentDataGrid;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            int ID = (EquipmentDataGrid.SelectedItem as EquipmentItem).Id;
            EditEquipmentPopup editDepartment = new EditEquipmentPopup(ID);
            editDepartment.ShowDialog();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int ID = (EquipmentDataGrid.SelectedItem as EquipmentItem).Id;
            var deleteEquipment = context.Equipment.Where(m => m.Id == ID).Single();
            context.Equipment.Remove(deleteEquipment);
            context.SaveChanges();
            EquipmentDataGrid.ItemsSource = context.Equipment.ToList();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = context.Equipment.Where(x => x.Name.Contains(SearchTextBox.Text) || x.RoomId.ToString().Contains(SearchTextBox.Text)).ToList();
            EquipmentDataGrid.ItemsSource = result;
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
