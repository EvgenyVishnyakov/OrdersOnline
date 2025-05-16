namespace OnlineOrderWebApp.DTO
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; } = null!;
        public string Created { get; set; } = null!;
        public List<OrderLineDto> Lines { get; set; } = new();
    }
}
