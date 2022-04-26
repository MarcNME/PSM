using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using PSM_Libary;
using PSM_Libary.model;

namespace PSM_Task_GUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DbAdapter _adapter;
        private List<Employee> _employees;
        public MainWindow()
        {
            InitializeComponent();
            _adapter = new DbAdapter("psm", "127.0.0.1", "psm", "psm");

            if (!_adapter.TestDbConnection())
            {
                Console.Error.WriteLine("DB couldn't connect");
            }
            
            _employees = _adapter.GetEmployees();
            
            RefreshEmployees();

            CbxEmployees.SelectedIndex = 0;
        }

        private void RefreshEmployees()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            foreach (var employee in _employees)
            {
                dict.Add(employee.Id, employee.FirstName + " " + employee.LastName);
            }

            //CbxEmployees.SelectedValue = new Binding(dict);
        }


        private void btn_save_OnClick(object sender, RoutedEventArgs e)
        {
            Report report = new Report
            {
            };
        }
    }
}
