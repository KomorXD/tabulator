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
using tabulator.Core;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for AddEmployeeView.xaml
    /// </summary>

    public partial class AddEmployeeView : UserControl
    {
        DBContext context = DBContext.GetInstance();

        public AddEmployeeView()
        {
            InitializeComponent();

            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid, context.Rooms.ToList(), context.FacultyRooms.ToList(), context.DepartmentRooms.ToList());
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(!AllInputsFilled())
            {
                //Warning
                return;
            }

            Employee temp = new Employee();
            PopulateTempEquipment(temp);

            context.Employees.Add(temp);
            context.SaveChanges();
            ClearAllFields();
        }

        private void ClearAllFields()
        {
            throw new NotImplementedException();
        }

        private void PopulateTempEquipment(Employee temp)
        {
            temp.Name = NameInput.Text;
            temp.Surname = SurnameInput.Text;
            temp.PESEL = PeselInput.Text;
            temp.PhoneNumber = PhoneNumberInput.Text;
            temp.Room = context.Rooms.ToList().Where(room => room.Id == (((dynamic)RoomDataGrid.SelectedItem).ID)).FirstOrDefault();
        }

        bool AllInputsFilled()
        {
            if(NameInput.Text.Equals(string.Empty)) return false;
            if(SurnameInput.Text.Equals(string.Empty)) return false;
            if(PeselInput.Text.Equals(string.Empty)) return false;
            //if(PhoneNumberInput.Text.Equals(string.Empty)) return false;
            //if(RoleDropdown.SelectedIndex == -1) return false;
            //if (RoomDataGrid.SelectedIndex == -1) return false;

            return true;
        }
    }
}
