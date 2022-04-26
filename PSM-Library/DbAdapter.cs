using System;
using System.Collections.Generic;
using System.Text;
using PSM_Libary.Connectors;
using PSM_Libary.model;

namespace PSM_Libary
{
    public class DbAdapter
    {
        private readonly IDBConnector _connector;

        public DbAdapter()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _connector = new MySQLConnector("psm", "localhost", "psm", "psm");
        }

        public DbAdapter(string dbName, string dbHost, string dbUser, string dbPassword)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _connector = new MySQLConnector(dbName, dbHost, dbUser, dbPassword);
        }

        public bool TestDbConnection()
        {
            return _connector.TestConnection();
        }

        public List<FamilyMember> GetFamilyMembers(int employeeId)
        {
            var sql = "SELECT memberID, relation, description, contribution, firstName " +
                      "FROM tblfamilymembers " +
                      $"WHERE employeeId = {employeeId}";

            var reader = _connector.ExecuteQuery(sql);

            var familyMembers = new List<FamilyMember>();

            while (reader.Read())
            {
                var familyMember = new FamilyMember
                {
                    Id = (int) reader["memberID"],
                    Description = (string) reader["description"],
                    Contribution = (int) reader["contribution"],
                    FirstName = (string) reader["firstName"]
                };

                familyMember.Relation = (Relation) reader["relation"];
                
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
                DepartmentHeadName = (string) reader["departmentHeadName"],
                DepartmentHeadLastName = (string) reader["departmentHeadLastName"],
            };
            _connector.CloseConnection();
            return department;
        }

        public City GetCity(int cityPlz)
        {
            var sql = $"SELECT * FROM tblcity WHERE plz = {cityPlz}";
            var reader = _connector.ExecuteQuery(sql);

            reader.Read();

            var city = new City
            {
                Plz = (string) reader["plz"],
                Name = (string) reader["name"],
                Vorwahl = (string) reader["vorwahl"],
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
                var employee = new Employee();
                
                employee.Id = (int) reader["employeeID"];
                employee.FirstName = (string) reader["firstName"];
                employee.LastName = (string) reader["lastName"];
                employee.Address = (string) reader["phoneNumber"];
                employee.BaseSalary = (int) reader["baseSalary"];
                employee.Birthday = DateTime.Parse((string) reader["birthday"]);
                employee.EntryDate = DateTime.Parse((string) reader["entryDate"]);
                
                Enum.TryParse((string) reader["gender"], out Gender gender);
                employee.Gender = gender;

                if (reader["departmentID"] != System.DBNull.Value)
                {
                    employee.Department = GetDepartment((int) reader["departmentID"]);
                }

                if (reader["plz"] != System.DBNull.Value)
                {
                    employee.City = GetCity((int) reader["plz"]);
                }
                
                employee.FamilyMembers = GetFamilyMembers(employee.Id);
                
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
            var sql = $"SELECT * FROM tblreports WHERE id = {reportId}";
            var reader = _connector.ExecuteQuery(sql);

            reader.Read();
            var report = new Report
            {
                Id = (int) reader["reportID"],
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
            var dml = "INSERT INTO tblemployees (firstName, lastName, gender, birthday, plz, address, phoneNumber, entryDate, baseSalary, departmentID) " +
                      $" VALUES ({employee.FirstName},{employee.LastName},{employee.Gender},{employee.Birthday},{employee.City.Plz}," +
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
                $"INSERT INTO tblcity(plz, name, vorwahl, prefix) VALUES ({city.Plz}, {city.Name}, {city.Vorwahl}, {city.Prefix})";
            return _connector.ExecuteNonQuery(dml);
        }

        public int AddDepartment(Department dep)
        {
            var dml =
                $"INSERT INTO tbldepartmens(departmentName, departmentHeadName, departmentHeadLastName) VALUES ({dep.Name}, {dep.DepartmentHeadName}, {dep.DepartmentHeadLastName})";
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