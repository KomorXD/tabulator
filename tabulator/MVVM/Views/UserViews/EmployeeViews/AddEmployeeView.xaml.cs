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
        public static DataGrid dataGrid;

        public AddEmployeeView()
        {
            InitializeComponent();

            dataGrid = RoomDataGrid;
            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid, context.Rooms.ToList(), context.FacultyRooms.ToList(), context.DepartmentRooms.ToList());
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
