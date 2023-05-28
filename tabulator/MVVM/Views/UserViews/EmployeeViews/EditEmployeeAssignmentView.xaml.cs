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
using tabulator.Core;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for EditEmployeeAssignmentView.xaml
    /// </summary>
    public partial class EditEmployeeAssignmentView : UserControl
    {
        DBContext context = DBContext.GetInstance();
        public EditEmployeeAssignmentView()
        {
            InitializeComponent();
            DataGridManager.GetInstance().ShowAssaignmentDataGrdi(AssaignmentDataGrid, context.EquipmentCaretakers.ToList());
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<EquipmentCaretakers> result = context.EquipmentCaretakers.ToList();
            if (!SearchTextBox.Text.Equals(string.Empty))
            {
                result = context.EquipmentCaretakers.Where(x => x.Item.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) || x.Item.Room.Number.ToLower().Contains(SearchTextBox.Text.ToLower()) || x.Employee.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) || x.Employee.Surname.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            }
            DataGridManager.GetInstance().ShowAssaignmentDataGrdi(AssaignmentDataGrid, result);
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
