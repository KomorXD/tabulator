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
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for EditEquipmentPopup.xaml
    /// </summary>
    public partial class EditEquipmentPopup : Window
    {
        DBContext context = DBContext.GetInstance();
        int Id;

        public EditEquipmentPopup(int EquipmentID)
        {
            InitializeComponent();
            Id = EquipmentID;

            var editEquipment = (from m in context.Equipment where m.Id == EquipmentID select m).FirstOrDefault();
            NameInput.Text = editEquipment.Name;
            DescriptionInput.Text = editEquipment.Description;

            RoomDropdown.Items.Add(new ComboBoxItem() { Content = "Room Example 1" });
            RoomDropdown.Items.Add(new ComboBoxItem() { Content = "Room Example 2" });
            RoomDropdown.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EquipmentItem equipment = (from m in context.Equipment where m.Id == Id select m).FirstOrDefault();
            equipment.Name = NameInput.Text;
            equipment.Description = DescriptionInput.Text;

            // do ogarniecia przez tylny koniec xd

            context.SaveChanges();
            EditEquipmentDataView editEquipmentDataView = new EditEquipmentDataView();
            this.Close();
        }
    }
}
