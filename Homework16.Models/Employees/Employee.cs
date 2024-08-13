namespace Homework16.Models.Employees
{
    public class Employee : Base
    {
        public string Password { get; set; } = string.Empty;
        public int BossId { get; set; }
    }
}
