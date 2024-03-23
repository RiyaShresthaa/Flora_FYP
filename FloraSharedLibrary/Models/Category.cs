namespace FloraSharedLibrary.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        //Relationship : one to many
        public List<Product>? Products { get; set; }
    }
}
