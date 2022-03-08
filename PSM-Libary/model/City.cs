namespace PSM_Libary.model
{
    public class City
    { 
        public int Id { get; set; }
        public string Plz { get; set; }
        public string Name { get; set; }
        public string Addition { get; set; }
        public string Prefix { get; set; }

        public City(string plz, string name, string addition, string prefix)
        {
            this.Plz = plz;
            this.Name = name;
            this.Addition = addition;
            this.Prefix = prefix;
        }
    }
}