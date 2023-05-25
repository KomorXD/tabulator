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

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for ReportGeneratorView.xaml
    /// </summary>
    public partial class ReportGeneratorView : UserControl
    {
        int recordsFound = 0;

        public ReportGeneratorView()
        {
            InitializeComponent();
            UpdateText();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateText()
        {
            RecordsText.Text = "Records found: " + recordsFound.ToString();
        }
    }
}
