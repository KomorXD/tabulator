using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabulator.Core;
using tabulator.VVM.Viewmodels.UserVM;

namespace tabulator.VVM.Viewmodels
{
    class UserViewModel : ObservableObject
    {
        public RelayCommand EmployeeAssignmentCommand { get; set; }
        public RelayCommand AddEquipmentCommand { get; set; }
        public RelayCommand EditEquipmentDataCommand { get; set; }
        public RelayCommand AddRoomCommand { get; set; }
        public RelayCommand EditRoomDataCommand { get; set; }
        public RelayCommand AddFacultyCommand { get; set; }
        public RelayCommand EditFacultyDataCommand { get; set; }
        public RelayCommand AddDepartmentCommand { get; set; }
        public RelayCommand EditDepartmentDataCommand { get; set; }


        public EmployeeAssignmentViewModel EmployeeAssignmentVM { get; set; }
        public AddEquipmentViewModel       AddEquipmentVM { get; set; }
        public EditEquipmentDataViewModel  EditEquipmentDataVM { get; set; }
        public AddRoomViewModel            AddRoomVM { get; set; }
        public EditRoomDataViewModel       EditRoomDataVM { get; set; }
        public AddFacultyViewModel         AddFacultyVM { get; set; }
        public EditFacultyDataViewModel    EditFacultyDataVM { get; set; }
        public AddDepartmentViewModel      AddDepartmentVM { get; set; }
        public EditDepartmentDataViewModel EditDepartmentDataVM { get; set; }

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

        public UserViewModel()
        {
            EmployeeAssignmentVM = new EmployeeAssignmentViewModel();
            AddEquipmentVM =       new AddEquipmentViewModel();
            EditEquipmentDataVM =  new EditEquipmentDataViewModel();
            AddRoomVM =            new AddRoomViewModel();
            EditRoomDataVM =       new EditRoomDataViewModel();
            AddFacultyVM =         new AddFacultyViewModel();
            EditFacultyDataVM =    new EditFacultyDataViewModel();
            AddDepartmentVM =      new AddDepartmentViewModel();
            EditDepartmentDataVM = new EditDepartmentDataViewModel();

            CurrentView = EmployeeAssignmentVM;

            EmployeeAssignmentCommand = new RelayCommand(o =>
            {
                CurrentView = EmployeeAssignmentVM;
            });

            AddEquipmentCommand = new RelayCommand(o =>
            {
                CurrentView = AddEquipmentVM;
            });

            EditEquipmentDataCommand = new RelayCommand(o =>
            {
                CurrentView = EditEquipmentDataVM;
            });

            AddRoomCommand = new RelayCommand(o =>
            {
                CurrentView = AddRoomVM;
            });

            EditRoomDataCommand = new RelayCommand(o =>
            {
                CurrentView = EditRoomDataVM;
            });

            AddFacultyCommand = new RelayCommand(o =>
            {
                CurrentView = AddFacultyVM;
            });

            EditFacultyDataCommand = new RelayCommand(o =>
            {
                CurrentView = EditFacultyDataVM;
            });

            AddDepartmentCommand = new RelayCommand(o =>
            {
                CurrentView = AddDepartmentVM;
            });

            EditDepartmentDataCommand = new RelayCommand(o =>
            {
                CurrentView = EditDepartmentDataVM;
            });
        }
    }
}
