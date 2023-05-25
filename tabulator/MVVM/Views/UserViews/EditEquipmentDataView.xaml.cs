using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
            EquipmentDataGrid.ItemsSource = context.Equipment.Select(eq => new
            {
                eq.Id,
                eq.Name,
                eq.Description,
                RoomNumber = eq.Room.Number,
                eq.Available,
                eq.NotInUse,
                eq.Destroyed
            }).ToList();
            dataGrid = EquipmentDataGrid;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (EquipmentDataGrid.SelectedItem is null)
                return;
            var selectedItem = (dynamic)EquipmentDataGrid.SelectedItem;
            int id = selectedItem.Id;
            EditEquipmentPopup editDepartment = new EditEquipmentPopup(id);
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
