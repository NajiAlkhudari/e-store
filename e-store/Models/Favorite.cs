namespace e_store.Models
{
    public class Favorite
    {
        public int FavoriteID { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
