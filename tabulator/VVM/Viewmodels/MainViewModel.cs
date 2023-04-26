﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabulator.Core;

namespace tabulator.VVM.Viewmodels
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand AddUserViewCommand { get; set; }
        public RelayCommand EditUserDataViewCommand { get; set; }
        public RelayCommand EditUserPermissionsViewCommand { get; set; }
        public RelayCommand RemoveUserViewCommand { get; set; }


        public AddUserViewModel AddUserVM { get; set; }
        public EditUserDataViewModel EditUserDataVM { get; set; }
        public EditUserPermissionsViewModel EditUserPermissionsVM { get; set; }
        public RemoveUserViewModel RemoveUserVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get {  return _currentView;}
            set 
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            AddUserVM = new AddUserViewModel();
            EditUserDataVM = new EditUserDataViewModel();
            EditUserPermissionsVM = new EditUserPermissionsViewModel();
            RemoveUserVM = new RemoveUserViewModel();

            CurrentView = AddUserVM;

            AddUserViewCommand = new RelayCommand(o =>
            {
                CurrentView = AddUserVM;
            }); 
            
            EditUserDataViewCommand = new RelayCommand(o =>
            {
                CurrentView = EditUserDataVM;
            });

            EditUserPermissionsViewCommand = new RelayCommand(o =>
            {
                CurrentView = EditUserPermissionsVM;
            });

            RemoveUserViewCommand = new RelayCommand(o =>
            {
                CurrentView = RemoveUserVM;
            });
        }
    }
}
