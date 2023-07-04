using AdminPanel.Models.Entities;

namespace AdminPanel.Models.ViewModels;

public sealed class OrdersViewModel
{
    public IEnumerable<Order> Orders { get; set; } = null!;
    public IEnumerable<OrderStatus> Statuses { get; set; } = null!;
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}