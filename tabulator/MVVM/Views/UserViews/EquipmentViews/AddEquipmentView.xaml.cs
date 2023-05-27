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

namespace tabulator.MVVM.Views.UserViews
{
   
    /// <summary>
    /// Interaction logic for AddEquipmentView.xaml
    /// </summary>
    public partial class AddEquipmentView : UserControl
    {
        DBContext context = DBContext.GetInstance();

        public AddEquipmentView()
        {
            InitializeComponent();
            AvailableInput_SelectionChanged(AvailableInput, null);
            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid, context.Rooms.ToList(), context.FacultyRooms.ToList(), context.DepartmentRooms.ToList());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private bool StringToBool(string str)
        {
            if (str == "True") return true;
            return false;
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AvailableInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var available = ((ComboBoxItem)AvailableInput.SelectedItem)?.Content?.ToString();
            switch (available)
            {
                case "True":
                    NotInUseInput.Visibility = Visibility.Hidden;
                    DestroyedInput.Visibility = Visibility.Hidden;
                    notinuseText.Visibility = Visibility.Hidden;
                    destroyedText.Visibility = Visibility.Hidden;
                    break;

                case "False":
                    NotInUseInput.Visibility= Visibility.Visible;
                    destroyedText.Visibility= Visibility.Visible;
                    DestroyedInput.Visibility= Visibility.Visible;
                    notinuseText.Visibility= Visibility.Visible;
                    NotInUseInput.SelectedIndex= 0;
                    DestroyedInput.SelectedIndex= 1;
                    break;
            }
        }
    }
}
