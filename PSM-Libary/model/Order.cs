using System;

namespace PSM_Libary.model
{
    public class Order
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime PayDate { get; set; }
        public string Customer { get; set; }
    }
}