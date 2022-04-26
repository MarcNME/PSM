using System;
using System.Collections.Generic;
using System.IO;
using PSM_Libary.model;
using LumenWorks.Framework.IO.Csv;
using System.Data;

namespace PSM
{
    class ExcelAdapter
    {
        List<FamilyMember> familyMembers = new List<FamilyMember>();
        PSM_Libary.DbAdapter dbAdapter = new PSM_Libary.DbAdapter();


        public void ReadfamilyMemberExcel(string filename)
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(filename)), true))
            {
                csvTable.Load(csvReader);
            }
            FamilyMember fMbr;
            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                fMbr = new FamilyMember();
                fMbr.Id = Int32.Parse(csvTable.Rows[i][0].ToString());
                fMbr.Relation = (Relation)Enum.Parse(typeof(Relation), csvTable.Rows[i][1].ToString());
                fMbr.Contribution = Int32.Parse(csvTable.Rows[i][2].ToString());
                fMbr.Description = csvTable.Rows[i][3].ToString();
                fMbr.FirstName = csvTable.Rows[i][4].ToString();
                dbAdapter.AddFamilyMember(fMbr);
            }
        }
        public void ReadOrderExcel(string filename)
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(filename)), true))
            {
                csvTable.Load(csvReader);
            }
            Order order;
            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                order = new Order();
                order.Id = Int32.Parse(csvTable.Rows[i][0].ToString());
                order.Value = Int32.Parse(csvTable.Rows[i][1].ToString());
                order.PayDate = DateTime.Parse(csvTable.Rows[i][2].ToString());
                order.Customer = csvTable.Rows[i][3].ToString();
                dbAdapter.AddOrder(order);
            }
        }
        public void ReadReportExcel(string filename)
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(filename)), true))
            {
                csvTable.Load(csvReader);
            }
            Report report;

            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                report = new Report();
                report.Id = Int32.Parse(csvTable.Rows[i][0].ToString());
                report.Date = DateTime.Parse(csvTable.Rows[i][1].ToString());
                report.OrderId = Int32.Parse(csvTable.Rows[i][2].ToString());
                report.ActivityId = Int32.Parse(csvTable.Rows[i][3].ToString());
                report.Hours = Int32.Parse(csvTable.Rows[i][4].ToString());
                dbAdapter.AddReport(report);
            }
        }
        public void ReadPersonalExcel(string filename)
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(filename)), true))
            {
                csvTable.Load(csvReader);
            }
            Employee employee;

            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                employee = new Employee();
                employee.Id = Int32.Parse(csvTable.Rows[i][0].ToString());
                employee.LastName = csvTable.Rows[i][1].ToString();
                employee.FirstName = csvTable.Rows[i][2].ToString();
                employee.Gender = (Gender)Enum.Parse(typeof(Gender), csvTable.Rows[i][3].ToString());
                employee.Birthday = DateTime.Parse(csvTable.Rows[i][4].ToString());
                employee.City.Id = Int32.Parse(csvTable.Rows[i][5].ToString());
                employee.Address = csvTable.Rows[i][6].ToString();
                employee.Phonenumber = csvTable.Rows[i][7].ToString();
                employee.EntryDate = DateTime.Parse(csvTable.Rows[i][8].ToString());
                employee.BaseSalary = Int32.Parse(csvTable.Rows[i][9].ToString());
                employee.Department.Id = Int32.Parse(csvTable.Rows[i][10].ToString());
                dbAdapter.AddEmployee(employee);
            }
        }
    }
}
