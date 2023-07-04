using AdminPanel.Models.Entities;

namespace AdminPanel.Models.ViewModels;

public sealed class ChangeOrderStatusViewModel
{
    public int OrderId { get; set; }
    public string OrderStatus { get; set; } = null!;
    public IEnumerable<OrderStatus> Statuses { get; set; } = null!;
}