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
using tabulator.Models;

namespace tabulator.VVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for AddRoomView.xaml
    /// </summary>
    public partial class AddRoomView : UserControl
    {
        public AddRoomView()
        {
            InitializeComponent();
        }

        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            string roomNumber = RoomNumberInput.Text;
        }
    }
}
