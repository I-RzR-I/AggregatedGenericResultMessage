namespace InfoResultTests.Dtos
{
    internal sealed class OrderDto
    {
        public int ItemCount { get; set; }
        public decimal Total { get; set; }
        public string Customer { get; set; }
    }
}