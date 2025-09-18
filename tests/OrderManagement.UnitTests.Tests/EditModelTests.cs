using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Entities;
using OrderManagement.Web.Pages.Orders;

namespace OrderManagement.UnitTests.Tests
{

    public class EditModelTests
    {
        [Fact]
        public async Task OnGetAsync_OrderExists_SetsOrderProperty()
        {
            // Arrange
            var mockService = new Mock<IOrderService>(null!);
            var order = new Order { Id = 1, CustomerName = "Test", OrderDate = System.DateTime.Today };
            mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(order);

            var pageModel = new EditModel(mockService.Object);

            // Act
            var result = await pageModel.OnGetAsync(1);

            // Assert
            Assert.IsType<PageResult>(result);
            Assert.Equal(order, pageModel.Order);
        }

        [Fact]
        public async Task OnGetAsync_OrderDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var mockService = new Mock<OrderService>(null!);
            mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((Order?)null);

            var pageModel = new EditModel(mockService.Object);

            // Act
            var result = await pageModel.OnGetAsync(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnPostAsync_ModelInvalid_ReturnsPage()
        {
            // Arrange
            var mockService = new Mock<OrderService>(null!);
            var pageModel = new EditModel(mockService.Object);
            pageModel.ModelState.AddModelError("CustomerName", "Required");
            pageModel.Order = new Order();

            // Act
            var result = await pageModel.OnPostAsync();

            // Assert
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public async Task OnPostAsync_ModelValid_CallsUpdateAndRedirects()
        {
            // Arrange
            var mockService = new Mock<OrderService>(null!);
            var order = new Order { Id = 1, CustomerName = "Test", OrderDate = System.DateTime.Today };
            var pageModel = new EditModel(mockService.Object) { Order = order };

            // Act
            var result = await pageModel.OnPostAsync();

            // Assert
            mockService.Verify(s => s.UpdateAsync(order), Times.Once);
            var redirect = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("Index", redirect.PageName);
        }
    }

}
