using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for EditUserDataView.xaml
    /// </summary>
    public partial class EditUserDataView : UserControl
    {
        public EditUserDataView()
        {
            InitializeComponent();

            DBContext context = DBContext.GetInstance();

            //Employee employee = context.Employees.Find(18);
            //Employee employee2 = new Employee
            //{
            //    Name = "Ziutek",
            //    Surname = "Eluwa",
            //    PESEL = "123456",
            //    PhoneNumber = "111111"
            //};
            //context.Employees.Attach(employee);
            //context.Employees.Remove(employee);
            //context.Employees.Add(employee2);
            //context.SaveChanges();
        }
    }
}
