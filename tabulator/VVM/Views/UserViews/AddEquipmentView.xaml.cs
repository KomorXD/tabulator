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



namespace tabulator.VVM.Views.UserViews
{
   
    /// <summary>
    /// Interaction logic for AddEquipmentView.xaml
    /// </summary>
    public partial class AddEquipmentView : UserControl
    {
      
        public AddEquipmentView()
        {
            InitializeComponent();
            RoomDropdownSelection(null, null);
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name =        NameInput.Text;
            string description = DescriptionInput.Text;
            bool isAvailable =   StringToBool(AvailableInput.Text);
            bool isInUse =       StringToBool(NotInUseInput.Text);
            bool isDestroyed =   StringToBool(DestroyedInput.Text);
            string selectedRoom = ((ComboBoxItem)RoomDropdown.SelectedItem)?.Content?.ToString();
        }

        private void RoomDropdownSelection(object sender, SelectionChangedEventArgs e)
        {
            RoomDropdown.Items.Add(new ComboBoxItem() { Content = "Room Example 1" });
            RoomDropdown.Items.Add(new ComboBoxItem() { Content = "Room Example 2" });
            RoomDropdown.SelectedIndex = 0;
        }
        private bool StringToBool(string str)
        {
            if (str == "True") return true;
            return false;
        }
    }
}
