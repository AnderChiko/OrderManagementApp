using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Web.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _service;
        public IndexModel(IOrderService service) => _service = service;

        public List<Order> Orders { get; set; } = new();

        public async Task OnGetAsync()
        {
            Orders = await _service.GetAllAsync();
        }
    }
}
