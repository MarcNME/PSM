namespace PSM_Libary.model
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Employee DepartmentHead { get; set; }
    }
}