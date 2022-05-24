using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PSM_Libary;
using PSM_Libary.model;

namespace PSM_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DbAdapter _adapter;
        private List<Employee> _employees;
        private List<Activity> _activities;
        
        public MainWindow()
        {            
            InitializeComponent();
            
            _adapter = new DbAdapter("psm", "127.0.01", "psm", "psm");
            Refresh();
        }
        
        private void Refresh()
        {
            _employees = _adapter.GetEmployees();
            _activities = _adapter.GetActivitys();
            
            CbxEmployees.Items.Clear();

            foreach (var employee in _employees)
            {
                CbxEmployees.Items.Add(employee);
            }
        }

        private void btn_Kalkulation(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_Nachkalkulation(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_Gehaltsermittlung(object sender, RoutedEventArgs e)
        {

            if (CbxEmployees.SelectedIndex == -1)
            {
                MessageBox.Show("Bitte wählen Sie einen Mittarbeiter aus");
                return;
            }

            Employee employee = (Employee) CbxEmployees.SelectionBoxItem;

            double salary = employee.BaseSalary;

            foreach (var familymember in employee.FamilyMembers)
            {
                salary = familymember.Contribution;
            }

            var reports = _adapter.GetAllReportsForEmployee(employee.Id);

            foreach (var report in reports)
            {
                salary += report.Hours * _activities.Find(activity => activity.Id == report.ActivityId).Price;
            }

            lbl_Gehaltsermittlung.Content = $"{salary}€";

        }

        private void txtb_calculation(object sender, TextChangedEventArgs e)
        {
        }
    }
}
