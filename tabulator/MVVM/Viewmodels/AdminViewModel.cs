using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabulator.Core;
using tabulator.MVVM.Viewmodels.AdminVM;

namespace tabulator.MVVM.Viewmodels
{
    class AdminViewModel : ObservableObject
    {
        public RelayCommand AddUserViewCommand { get; set; }
        public RelayCommand EditUserDataViewCommand { get; set; }

        public AddUserViewModel AddUserVM { get; set; }
        public EditUserDataViewModel EditUserDataVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public AdminViewModel()
        {
            AddUserVM             = new AddUserViewModel();
            EditUserDataVM        = new EditUserDataViewModel();

            CurrentView = AddUserVM;

            AddUserViewCommand = new RelayCommand(o =>
            {
                CurrentView = AddUserVM;
            });

            EditUserDataViewCommand = new RelayCommand(o =>
            {
                CurrentView = EditUserDataVM;
            });
        }
    }
}
