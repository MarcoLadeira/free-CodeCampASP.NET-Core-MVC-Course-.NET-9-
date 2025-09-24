namespace practiceReal.Models
{
    public class ItemClient
    {
        public int ItemId { get; set; }
        public Item item { get; set; } = null!; 

        public int ClientId { get; set; }
        
        public Client Client { get; set; } = null!;

    }
}
