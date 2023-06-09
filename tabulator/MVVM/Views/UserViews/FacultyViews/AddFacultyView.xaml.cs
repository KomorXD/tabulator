﻿using System.Windows;
using System.Windows.Controls;
using tabulator.MVVM.Models;
using tabulator.MVVM.Viewmodels.UserVM;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for AddFacultyView.xaml
    /// </summary>
    public partial class AddFacultyView : UserControl
    {
        public AddFacultyViewModel ViewModel { get; set; }
        public AddFacultyView()
        {
            InitializeComponent();
            ViewModel = new AddFacultyViewModel();
        }

        private void btnAddFaculty_Click(object sender, RoutedEventArgs e)
        {
            if(NameInput.Text == string.Empty || AddressInput.Text == string.Empty)
            {
                errorText.Text = "Fill all fields!";
                return;
            }

            Faculty faculty = new Faculty();
            faculty.Name = NameInput.Text;
            faculty.Address = AddressInput.Text;
            ViewModel.AddFaculty(faculty);
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            NameInput.Text = string.Empty;
            AddressInput.Text = string.Empty;
            errorText.Text = string.Empty;
        }

        private void Border_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                btnAddFaculty_Click(sender, e);
            }
        }
    }
}
