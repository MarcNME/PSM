namespace PSM_Libary.model
{
    public enum Relation
    {
        F, //Ehefrau
        M, //Ehemann
        S, //Sohn
        T //Tochter
    }
    public class FamilyMember
    {
        public int Id { get; set; }
        public Relation Relation { get; set; }
        public int Contribution { get; set; }
        public string FirstName { get; set; }
    }
}