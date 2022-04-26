using System;
using System.Collections.Generic;

namespace PSM_Libary.model
{
    public enum Gender
    {
        M, //Male
        F, //Female
        D //Divers
    }
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Phonenumber { get; set; }
        public int BaseSalary { get; set; }
        public Department Department { get; set; }
        public List<FamilyMember> FamilyMembers { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public DateTime EntryDate { get; set; }
    }
}