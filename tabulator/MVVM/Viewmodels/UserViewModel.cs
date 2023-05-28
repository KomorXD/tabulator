using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabulator.Core;
using tabulator.MVVM.Viewmodels.UserVM;
using tabulator.MVVM.Viewmodels.UserVM.EmployeeVM;

namespace tabulator.MVVM.Viewmodels
{
    class UserViewModel : ObservableObject
    {
        public RelayCommand EmployeeAssignmentCommand { get; set; }
        public RelayCommand EditEmployeeAssignmentCommand { get; set; }
        public RelayCommand AddEquipmentCommand { get; set; }
        public RelayCommand EditEquipmentDataCommand { get; set; }
        public RelayCommand AddRoomCommand { get; set; }
        public RelayCommand EditRoomDataCommand { get; set; }
        public RelayCommand AddFacultyCommand { get; set; }
        public RelayCommand EditFacultyDataCommand { get; set; }
        public RelayCommand AddDepartmentCommand { get; set; }
        public RelayCommand EditDepartmentDataCommand { get; set; }
        public RelayCommand GenerateReportDataCommand { get; set; }
        public RelayCommand EditEmployeeDataCommand { get; set; }
        public RelayCommand AddEmployeeDataCommand { get; set; }


        public EmployeeAssignmentViewModel      EmployeeAssignmentVM { get; set; }
        public EditEmployeeAssignmentViewModel  EditEmployeeAssignmentVM { get; set; }
        public AddEquipmentViewModel            AddEquipmentVM { get; set; }
        public EditEquipmentDataViewModel       EditEquipmentDataVM { get; set; }
        public AddRoomViewModel                 AddRoomVM { get; set; }
        public EditRoomDataViewModel            EditRoomDataVM { get; set; }
        public AddFacultyViewModel              AddFacultyVM { get; set; }
        public EditFacultyDataViewModel         EditFacultyDataVM { get; set; }
        public AddDepartmentViewModel           AddDepartmentVM { get; set; }
        public EditDepartmentDataViewModel      EditDepartmentDataVM { get; set; }
        public GenerateReportViewModel          GenerateReportVM { get; set; }
        public EditEmployeeDataViewModel        EditEmployeeVM { get; set; }
        public AddEmployeeViewModel             AddEmployeeVM { get; set; }

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
            EmployeeAssignmentVM =      new EmployeeAssignmentViewModel();
            EditEmployeeAssignmentVM =  new EditEmployeeAssignmentViewModel();
            AddEquipmentVM =            new AddEquipmentViewModel();
            EditEquipmentDataVM =       new EditEquipmentDataViewModel();
            AddRoomVM =                 new AddRoomViewModel();
            EditRoomDataVM =            new EditRoomDataViewModel();
            AddFacultyVM =              new AddFacultyViewModel();
            EditFacultyDataVM =         new EditFacultyDataViewModel();
            AddDepartmentVM =           new AddDepartmentViewModel();
            EditDepartmentDataVM =      new EditDepartmentDataViewModel();
            GenerateReportVM =          new GenerateReportViewModel();
            EditEmployeeVM =            new EditEmployeeDataViewModel();
            AddEmployeeVM =             new AddEmployeeViewModel();

            CurrentView = EmployeeAssignmentVM;

            EmployeeAssignmentCommand = new RelayCommand(o =>
            {
                CurrentView = EmployeeAssignmentVM;
            });

            EditEmployeeAssignmentCommand = new RelayCommand(o =>
            {
                CurrentView = EditEmployeeAssignmentVM;
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

            GenerateReportDataCommand = new RelayCommand(o =>
            {
                CurrentView = GenerateReportVM;
            });

            EditEmployeeDataCommand = new RelayCommand(o =>
            {
                CurrentView = EditEmployeeVM;
            });

            AddEmployeeDataCommand = new RelayCommand(o =>
            {
                CurrentView = AddEmployeeVM;
            });
        }
    }
}
