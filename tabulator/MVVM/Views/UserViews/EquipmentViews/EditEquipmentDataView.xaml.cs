using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using tabulator.Core;
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
        private List<EquipmentItem> equipmentItems;
        public static DataGrid dataGrid;

        public EditEquipmentDataView()
        {
            InitializeComponent();
            equipmentItems = context.Equipment.ToList();
            DataGridManager.GetInstance().ShowEquipmentDataGrid(EquipmentDataGrid, equipmentItems);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (EquipmentDataGrid.SelectedItem is null)
                return;
            EquipmentItem tempEq = context.Equipment.ToList().Where(eq => eq.Id == (((dynamic)EquipmentDataGrid.SelectedItem).ID)).FirstOrDefault();
            EditEquipmentPopup editDepartment = new EditEquipmentPopup(tempEq);
            editDepartment.ShowDialog();
            DataGridManager.GetInstance().ShowEquipmentDataGrid(EquipmentDataGrid, equipmentItems);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteEquipment = equipmentItems.ElementAt(EquipmentDataGrid.SelectedIndex);
            context.Equipment.Remove(deleteEquipment);
            equipmentItems.RemoveAt(EquipmentDataGrid.SelectedIndex);
            context.SaveChanges();
            DataGridManager.GetInstance().ShowEquipmentDataGrid(EquipmentDataGrid, equipmentItems);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<EquipmentItem> result = context.Equipment.ToList();
            if(!SearchTextBox.Text.Equals(string.Empty))
            {
                result = context.Equipment.Where(x => x.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) || x.Room.Number.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            }
            DataGridManager.GetInstance().ShowEquipmentDataGrid(EquipmentDataGrid, result);
        }
        
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
