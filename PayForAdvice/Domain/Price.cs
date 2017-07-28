namespace Domain
{
    public class Price : Idable
    {
        public double Amount { get; set; }
        public string Details { get; set; }
        public string Order { get; set; }

        //foreign key
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}