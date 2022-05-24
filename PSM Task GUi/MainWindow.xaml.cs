using System;
using System.Collections.Generic;
using System.Windows;
using PSM_Libary;
using PSM_Libary.model;

namespace PSM_Task_GUi
{
    /// Author: Marc Enzmann
    public partial class MainWindow : Window
    {
        private DbAdapter _adapter;
        private List<Employee> _employees;
        private List<Order> _orders;
        private List<Activity> _activities;
        public MainWindow()
        {
            InitializeComponent();
            _adapter = new DbAdapter("psm", "127.0.0.1", "psm", "psm");

            if (!_adapter.TestDbConnection())
            {
                Console.Error.WriteLine("DB couldn't connect");
            }
            
            Refresh();
        }

        private void Refresh()
        {
            _employees = _adapter.GetEmployees();
            _orders = _adapter.GetOrders();
            _activities = _adapter.GetActivitys();
            
            CbxEmployees.Items.Clear();
            CbxOrders.Items.Clear();
            CbxActivity.Items.Clear();
            
            foreach (var employee in _employees)
            {
                CbxEmployees.Items.Add(employee);
            }

            foreach (var order in _orders)
            {
                CbxOrders.Items.Add(order);
            }

            foreach (var activity in _activities)
            {
                CbxActivity.Items.Add(activity);
            }
        }




        private void btn_save_OnClick(object sender, RoutedEventArgs e)
        {
            if (CbxEmployees.SelectedIndex == -1 || CbxActivity.SelectedIndex == -1 || CbxOrders.SelectedIndex == -1 ||
                txtb_hours.Text == "")
            {
                MessageBox.Show("Please enter a value for every field");
                return;
            }
            
            Report report = new Report();

            Employee selected = (Employee) CbxEmployees.SelectionBoxItem;
            Activity selectedActivity = (Activity) CbxActivity.SelectionBoxItem;
            Order selectedOrder = (Order) CbxOrders.SelectionBoxItem;
            
            report.EmployeeId = selected.Id;
            report.Date = (DateTime) DatePicker.SelectedDate.GetValueOrDefault();
            report.ActivityId = selectedActivity.Id;
            report.OrderId = selectedOrder.Id;
            report.Hours = int.Parse(txtb_hours.Text);

            _adapter.AddReport(report);

            MessageBox.Show("Saved successfully!");
        }
    }
}
