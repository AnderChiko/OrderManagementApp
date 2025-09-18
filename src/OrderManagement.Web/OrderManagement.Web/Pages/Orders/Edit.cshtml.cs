using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Web.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly IOrderService _service;
        public EditModel(IOrderService service) => _service = service;

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
            if (!ModelState.IsValid) return Page();

            await _service.UpdateAsync(Order);
            return RedirectToPage("Index");
        }
    }
}
