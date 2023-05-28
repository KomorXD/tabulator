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
    /// Interaction logic for AddEquipmentView.xaml
    /// </summary>
    public partial class AddEquipmentView : UserControl
    {
        DBContext context = DBContext.GetInstance();
        string available = "";
        public AddEquipmentView()
        {
            InitializeComponent();
            AvailableInput_SelectionChanged(AvailableInput, null);
            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid, context.Rooms.ToList(), context.FacultyRooms.ToList(), context.DepartmentRooms.ToList());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!AllFieldsAreFilled())
            {
                errorText.Text = "Fill out all fields!";
                return;
            }

            EquipmentItem temp = new EquipmentItem();
            PopulateTempEquipment(temp);

            context.Equipment.Add(temp);
            context.SaveChanges();
            ClearAllFields();
        }

        private void PopulateTempEquipment(EquipmentItem temp)
        {
            temp.Name = NameInput.Text;
            temp.Description = DescriptionInput.Text;
            temp.Available = GetCheckboxValue(AvailableInput);
            temp.NotInUse = GetCheckboxValue(NotInUseInput);
            temp.Destroyed = GetCheckboxValue(DestroyedInput);
            temp.Room = context.Rooms.ToList().Where(room => room.Id == (((dynamic)RoomDataGrid.SelectedItem).ID)).FirstOrDefault();
        }

        private bool GetCheckboxValue(ComboBox combobox)
        {
            string comboboxText = ((ComboBoxItem)combobox.SelectedItem)?.Content?.ToString();
            switch (comboboxText)
            {
                case "True":
                    return true;

                case "False":
                    return false;

                default:
                    return false;
            }
        }

        bool AllFieldsAreFilled()
        {
            if (NameInput.Text.Equals(string.Empty)) return false; 
            if (DescriptionInput.Text.Equals(string.Empty)) return false; 
            if (AvailableInput.SelectedIndex.Equals(-1)) return false; 
            if (RoomDataGrid.SelectedIndex.Equals(-1)) return false; 

            if(AvailableInput.SelectedIndex.Equals(1))
            {
                if (NotInUseInput.SelectedIndex.Equals(-1)) return false; 
                if (DestroyedInput.SelectedIndex.Equals(-1)) return false; 
            }
            
            return true;
        }

        void ClearAllFields()
        {
            NameInput.Text = string.Empty;
            DescriptionInput.Text = string.Empty;
            AvailableInput.SelectedIndex = 0;
            NotInUseInput.SelectedIndex = 0;
            DestroyedInput.SelectedIndex = 0;
            RoomDataGrid.SelectedIndex = 0;
            errorText.Text = string.Empty; 
        }

        private void AvailableInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeCheckboxVisibility();
        }

        private void ChangeCheckboxVisibility()
        {
            available = ((ComboBoxItem)AvailableInput.SelectedItem)?.Content?.ToString();

            switch (available)
            {
                case "True":
                    NotInUseInput.Visibility = Visibility.Hidden;
                    DestroyedInput.Visibility = Visibility.Hidden;
                    notinuseText.Visibility = Visibility.Hidden;
                    destroyedText.Visibility = Visibility.Hidden;
                    break;

                case "False":
                    NotInUseInput.Visibility = Visibility.Visible;
                    destroyedText.Visibility = Visibility.Visible;
                    DestroyedInput.Visibility = Visibility.Visible;
                    notinuseText.Visibility = Visibility.Visible;
                    NotInUseInput.SelectedIndex = 0;
                    DestroyedInput.SelectedIndex = 1;
                    break;
            }
        }
    }
}
