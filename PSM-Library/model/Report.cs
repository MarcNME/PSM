using System;

namespace PSM_Libary.model
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public int EmployeeId { get; set; }
        public int ActivityId { get; set; }
        public int OrderId { get; set; }
    }
}