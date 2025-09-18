using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Web.Pages.Orders
{

    public class CreateModel : PageModel
    {
        private readonly IOrderService _service;
        public CreateModel(IOrderService service) => _service = service;

        [BindProperty]
        public Order Order { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            await _service.AddAsync(Order);
            return RedirectToPage("Index");
        }
    }
}
