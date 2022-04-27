namespace PSM_Libary.model
{
    public class Activity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}