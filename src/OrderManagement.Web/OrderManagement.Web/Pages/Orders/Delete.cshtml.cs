using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Web.Pages.Orders
{

    public class DeleteModel : PageModel
    {
        private readonly IOrderService _service;
        public DeleteModel(IOrderService service) => _service = service;

        [BindProperty]
        public Order Order { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var order = await _service.GetByIdAsync(id);
            if (order == null) return NotFound();
            Order = order;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Order == null) return NotFound();
            await _service.DeleteAsync(Order);
            return RedirectToPage("Index");
        }
    }
}
