namespace Domain
{
    public class Price
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Details { get; set; }
        public string Order { get; set; }

        //foreign key
        public int UserId { get; set; }
        public User User { get; set; }
    }
}