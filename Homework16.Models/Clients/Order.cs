namespace Homework16.Models.Clients
{
    public class Order
    {
        public int Id { get; set; }
        public string ClientEmail { get; set; } = string.Empty;
        public int Code { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
