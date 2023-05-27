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

        public EmployeeAssignmentView()
        {
            InitializeComponent();
            AddDataToDataGrids();
        }

        private void EquipmentSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = context.Equipment.Where(x => x.Name.Contains(EquipmentSearchTextBox.Text) || x.Room.Number.ToString().Contains(EquipmentSearchTextBox.Text)).ToList();
            AddEquipment(result);
        }

        private void EmployeeSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = context.Employees.Where(x => x.Name.Contains(EmployeeSearchTextBox.Text) || x.Surname.Contains(EmployeeSearchTextBox.Text)).ToList();
            AddEmployees(result);
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


        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddDataToDataGrids()
        {
            AddEquipment(context.Equipment.ToList());
            AddEmployees(context.Employees.ToList());
        }
        private void AddEquipment(List<EquipmentItem> eqList)
        {
            EqDataGrid.ItemsSource = eqList.Select(eq => new
            {
                eq.Id,
                eq.Name,
                RoomNumber = eq.Room.Number,    
                eq.Available
            }).Where(eq => eq.Available.Equals(true)).ToList();
        }
        private void AddEmployees(List<Employee> emplList)
        {
            EmplDataGrid.ItemsSource = emplList.Select(empl => new
            {
                empl.Name,
                empl.Surname
            }).ToList();
        }
    }
}
