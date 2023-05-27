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
        EquipmentItem item;
        Employee employee;

        public EmployeeAssignmentView()
        {
            InitializeComponent();
            AddDataToDataGrids();
            employees = context.Employees.ToList();
            equipmentItems = context.Equipment.ToList();
        }

        private void EquipmentSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<EquipmentItem> result = context.Equipment.ToList();
            if(!EquipmentSearchTextBox.Text.Equals(string.Empty))
                result = context.Equipment.Where(x => x.Name.ToLower().Contains(EquipmentSearchTextBox.Text.ToLower()) || x.Room.Number.ToLower().Contains(EquipmentSearchTextBox.Text.ToLower())).ToList();
            DataGridManager.GetInstance().ShowShortEquipmentDataGrid(EqDataGrid, result);
        }

        private void EmployeeSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Employee> result = context.Employees.ToList();
            if(!EmployeeSearchTextBox.Text.Equals(string.Empty))
                result = context.Employees.Where(x => x.Name.ToLower().Contains(EmployeeSearchTextBox.Text.ToLower()) || x.Surname.ToLower().Contains(EmployeeSearchTextBox.Text.ToLower())).ToList();
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
            item = context.Equipment.ToList().Where(eq => eq.Id == (((dynamic)EqDataGrid.SelectedItem).ID)).FirstOrDefault();
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
            employee = context.Employees.ToList().Where(emp => emp.Id == (((dynamic)EmplDataGrid.SelectedItem).ID)).FirstOrDefault();
        }

        private void btnAssign_Click(object sender, RoutedEventArgs e)
        {
            if (eqBtn == null || emplBtn == null)
                return;

            item.Available = false;

            EquipmentCaretakers equipmentCaretakers = new EquipmentCaretakers();
            equipmentCaretakers.Item = item;
            equipmentCaretakers.Employee = employee;
            context.EquipmentCaretakers.Add(equipmentCaretakers);
            context.SaveChanges();
            AddDataToDataGrids();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddDataToDataGrids()
        {
            employees = context.Employees.ToList();
            equipmentItems = context.Equipment.ToList();
            DataGridManager.GetInstance().ShowShortEquipmentDataGrid(EqDataGrid, context.Equipment.ToList());
            DataGridManager.GetInstance().ShowShortEmployeeDataGrid(EmplDataGrid, context.Employees.ToList());
        }
    }
}
