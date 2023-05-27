using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using tabulator.Core;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for EmployeeAssignmentView.xaml
    /// </summary>
    public partial class EmployeeAssignmentView : UserControl
    {
        DBContext context = DBContext.GetInstance();
        ToggleButton eqBtn;
        ToggleButton emplBtn;
        List<Employee> employees;
        List<EquipmentItem> equipmentItems;

        public EmployeeAssignmentView()
        {
            InitializeComponent();
            AddDataToDataGrids();
            employees = context.Employees.ToList();
            equipmentItems = context.Equipment.ToList();
        }

        private void EquipmentSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = context.Equipment.Where(x => x.Name.Contains(EquipmentSearchTextBox.Text) || x.Room.Number.ToString().Contains(EquipmentSearchTextBox.Text)).ToList();
            DataGridManager.GetInstance().ShowShortEquipmentDataGrid(EqDataGrid, result);
        }

        private void EmployeeSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = context.Employees.Where(x => x.Name.Contains(EmployeeSearchTextBox.Text) || x.Surname.Contains(EmployeeSearchTextBox.Text)).ToList();
            DataGridManager.GetInstance().ShowShortEmployeeDataGrid(EmplDataGrid, result);
        }

        private void BtnAddEQ_Click(object sender, RoutedEventArgs e)
        {
            if (eqBtn != null)
                eqBtn.IsChecked = false;
            if(eqBtn == (ToggleButton)sender)
            {
                eqBtn.IsChecked = false;
                return;
            }
            eqBtn = (ToggleButton)sender;
            eqBtn.IsChecked = true;
        }

        private void BtnAddEmpl_Click(object sender, RoutedEventArgs e)
        {
            if (emplBtn != null)
                emplBtn.IsChecked = false;
            if (emplBtn == (ToggleButton)sender)
            {
                emplBtn.IsChecked = false;
                return;
            }
            emplBtn = (ToggleButton)sender;
            emplBtn.IsChecked = true;
        }

        private void btnAssign_Click(object sender, RoutedEventArgs e)
        {
            if (eqBtn == null || emplBtn == null)
                return;

            var equipment = equipmentItems.ElementAt(EqDataGrid.SelectedIndex);
            var employee = employees.ElementAt(EqDataGrid.SelectedIndex);            
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddDataToDataGrids()
        {
            DataGridManager.GetInstance().ShowShortEquipmentDataGrid(EqDataGrid, context.Equipment.ToList());
            DataGridManager.GetInstance().ShowShortEmployeeDataGrid(EmplDataGrid, context.Employees.ToList());
        }
    }
}
