namespace DomainLayer.Entities.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; } = null!;

        public List<BasketItem> Items { get; set; } = [];
    }
}