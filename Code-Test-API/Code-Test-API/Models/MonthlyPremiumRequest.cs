namespace Code_Test_API.Models
{
    public class MonthlyPremiumRequest
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public int DeathBenefit { get; set; }

        public MonthlyPremiumRequest() { }
    }
}
