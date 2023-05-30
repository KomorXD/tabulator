using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Viewmodels.UserVM
{
    public class AddFacultyViewModel
    {
        private ObservableCollection<Faculty> Faculties;

        public AddFacultyViewModel()
        {
            DBContext context = DBContext.GetInstance();
            Faculties = new ObservableCollection<Faculty>(context.Faculties);
        }
        public void AddFaculty(Faculty faculty)
        {
            DBContext context = DBContext.GetInstance();
            Faculties.Add(faculty);
            context.Faculties.Add(faculty);
            context.SaveChanges();
        }
    }
}
