using System;
using System.Collections.Generic;
using PSM_Libary.Connectors;
using PSM_Libary.model;

namespace PSM_Libary
{
    public class DbAdapter
    {
        private readonly IDBConnector _connector;

        public DbAdapter()
        {
            _connector = new MySQLConnector("PSM", "localhost", "pma", "pma");
        }

        public List<FamilyMember> GetFamilyMembers(int employeeId)
        {
            var sql = "SELECT memberID, relation, description, contribution, firstName " +
                      "FROM tblfamilymembers JOIN tblreations ON tblfamilymembers.memberID = tblreations.fk_familymeberId " +
                      $"WHERE tblreations.fk_employeeId = {employeeId}";

            var reader = _connector.ExecuteQuery(sql);

            var familyMembers = new List<FamilyMember>();

            while (reader.Read())
            {
                var familyMember = new FamilyMember
                {
                    Id = (int) reader["memberID"],
                    Relation = (Relation) reader["relation"],
                    Description = (string) reader["description"],
                    Contribution = (int) reader["contribution"],
                    FirstName = (string) reader["firstName"]
                };
                familyMembers.Add(familyMember);
            }

            _connector.CloseConnection();
            return familyMembers;
        }

        public Department GetDepartment(int departmentId)
        {
            var sql = $"Select * From tbldepartmens WHERE departmentID = {departmentId}";
            var reader = _connector.ExecuteQuery(sql);

            var department = new Department()
            {
                Id = (int) reader["departmentID"],
                Name = (string) reader["name"],
                DepartmentHeadId = (int) reader["departmentHeadID"],
            };
            _connector.CloseConnection();
            return department;
        }

        public City GetCity(int cityId)
        {
            var sql = $"SELECT * FROM tblcity WHERE id = {cityId}";
            var reader = _connector.ExecuteQuery(sql);

            reader.Read();

            var city = new City
            {
                Id = (int) reader["id"],
                Plz = (string) reader["plz"],
                Name = (string) reader["name"],
                Addition = (string) reader["addition"],
                Prefix = (string) reader["prefix"],
            };
            _connector.CloseConnection();
            return city;
        }

        public List<Employee> GetEmployees()
        {
            var sql = "SELECT * FROM tblemployees";
            var reader = _connector.ExecuteQuery(sql);

            var employees = new List<Employee>();
            while (reader.Read())
            {
                var employee = new Employee
                {
                    Id = (int) reader["employeeID"],
                    FirstName = (string) reader["firstName"],
                    LastName = (string) reader["lastName"],
                    Gender = (Gender) reader["gender"],
                    Birthday = DateTime.Parse((string) reader["birthday"]),
                    Address = (string) reader["phoneNumber"],
                    EntryDate = DateTime.Parse((string) reader["entryDate"]),
                    BaseSalary = (int) reader["baseSalary"],
                    Department = GetDepartment((int) reader["departmentID"]),
                };

                employee.FamilyMembers = GetFamilyMembers(employee.Id);
                employee.City = GetCity((int) reader["cityId"]);

                employees.Add(employee);
            }

            _connector.CloseConnection();
            return employees;
        }

        public Activity GetActivity(int activityId)
        {
            var sql = $"SELECT * FROM tblactivity WHERE activityID = {activityId}";
            var reader = _connector.ExecuteQuery(sql);

            reader.Read();
            var activity = new Activity
            {
                Id = (int) reader["activityID"],
                Description = (string) reader["Description"],
                Price = (double) reader["price"],
            };
            
            _connector.CloseConnection();
            return activity;
        }

        public Order GetOrder(int orderId)
        {
            var sql = $"SELECT * FROM tblorders WHERE orderID = {orderId}";
            var reader = _connector.ExecuteQuery(sql);

            reader.Read();
            var order = new Order
            {
                Id = (int) reader["orderID"],
                Value = (int) reader["value"],
                PayDate = (DateTime) reader["payDate"],
                Customer = (string) reader["customer"],
            };
            
            _connector.CloseConnection();
            return order;
        }

        public Report GetReport(int reportId)
        {
            var sql = $"SELECT * FROM tblreations WHERE id = {reportId}";
            var reader = _connector.ExecuteQuery(sql);

            reader.Read();
            var report = new Report
            {
                Id = (int) reader["orderID"],
                Date = (DateTime) reader["date"],
                Hours = (double) reader["hours"],
                OrderId = (int) reader["orderID"],
                ActivityId = (int) reader["activityID"],
                EmployeeId = (int) reader["employeeID"]
            };
            
            _connector.CloseConnection();
            return report;
        }
        
        public int AddEmployee(Employee employee)
        {
            var dml = "INSERT INTO tblemployees (firstName, lastName, gender, birthday, cityId, address, phoneNumber, entryDate, baseSalary, departmentID) " +
                      $" VALUES ({employee.FirstName},{employee.LastName},{employee.Gender},{employee.Birthday},{employee.City.Id}," +
                      $"{employee.Address},{employee.Phonenumber},{employee.EntryDate},{employee.BaseSalary},{employee.Department.Id});";
            return _connector.ExecuteNonQuery(dml);
        }

        public int AddActivity(Activity act)
        {
            var dml = $"INSERT INTO tblactivity(description, price) VALUES ({act.Description}, {act.Price})";
            return _connector.ExecuteNonQuery(dml);
        }

        public int AddCity(City city)
        {
            var dml =
                $"INSERT INTO tblcity(plz, name, addition, prefix) VALUES ({city.Plz}, {city.Name}, {city.Addition}, {city.Prefix})";
            return _connector.ExecuteNonQuery(dml);
        }

        public int AddDepartment(Department dep)
        {
            var dml =
                $"INSERT INTO tbldepartmens(departmentName, departmentHeadID) VALUES ({dep.Name}, {dep.DepartmentHeadId})";
            return _connector.ExecuteNonQuery(dml);
        }

        public int AddFamilyMember(FamilyMember fMbr)
        {
            var dml = $"INSERT INTO tblfamilymembers(relation, description, contribution, firstName) " +
                      $"VALUES ({fMbr.Relation}, {fMbr.Description}, {fMbr.Contribution}, {fMbr.FirstName})";
            return _connector.ExecuteNonQuery(dml);
        }

        public int AddOrder(Order order)
        {
            var dml = $"INSERT INTO tblorders(value, payDate, customer) VALUES ({order.Value}, {order.PayDate}, {order.Customer})";
            return _connector.ExecuteNonQuery(dml);
        }

        public int AddReport(Report report)
        {
            var dml = $"INSERT INTO tblreports (Date, orderID, activityID, hours, employeeID) " +
                      $"VALUES ({report.Date}, {report.OrderId}, {report.ActivityId}, {report.Hours}, {report.EmployeeId})";
            return _connector.ExecuteNonQuery(dml);
        }

        public int AddRelation(int employeeId, int familyMemberId)
        {
            var dml = $"INSERT INTO tblreations(employeeId, familymeberId) VALUES ({employeeId}, {familyMemberId})";
            return _connector.ExecuteNonQuery(dml);
        }
    }
}