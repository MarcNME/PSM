using System;
using System.Collections.Generic;
using System.Data.Common;
using PSM_Libary.Connectors;
using PSM_Libary.model;

namespace PSM_Libary
{
    public class DBAdapter
    {
        IDBConnector _connector;
        
        public DBAdapter()
        {
            _connector = new MySQLConnector("PSM", "localhost", "pma", "pma");
        }

        public int AddEmployee(Employee employee)
        {
            var dml = string.Format(
                "INSERT INTO tblemployees (firstName, lastName, gender, birthday, cityId, address, phoneNumber, entryDate, baseSalary, departmentID) ",
                " VALUES ({0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10});",
                employee.FirstName, employee.LastName, employee.Gender, employee.Birthday, employee.City.Id,
                employee.Address, employee.Phonenumber, employee.EntryDate, employee.BaseSalary, employee.Department.Id
            );
            return _connector.ExecuteNonQuery(dml);
        }

        public List<FamilyMember> GetFamilyMembers(int employeeId)
        {
            var sql = string.Format("SELECT memberID, relation, description, contribution, firstName " +
                                    "FROM tblfamilymembers JOIN tblreations ON tblfamilymembers.memberID = tblreations.fk_familymeberId " +
                                    "WHERE tblreations.fk_employeeId = {0}", employeeId);

            return null;
        }

        public List<Employee> GetEmployees()
        {
            var sql = string.Format("SELECT * FROM tblemployees");
            var dbReader = _connector.ExecuteQuery(sql);

            var employees = new List<Employee>();
            while (dbReader.Read())
            {
                var employee = new Employee
                {
                    FirstName = (string) dbReader["firstName"],
                    LastName = (string) dbReader["lastName"],
                    Gender = (Gender) dbReader["gender"],
                    Birthday = DateTime.Parse((string) dbReader["birthday"]),
                    City = null,
                    Address = (string) dbReader["phoneNumber"],
                    EntryDate = DateTime.Parse((string) dbReader["entryDate"]),
                    BaseSalary = (int) dbReader["baseSalary"],
                    Department = new Department()
                };

                employees.Add(employee);
            }

            return employees;
        }

    }
}