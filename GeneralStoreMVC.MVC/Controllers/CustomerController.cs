using GeneralStoreMVC.Data;
using GeneralStoreMVC.Models.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneralStoreMVC.MVC.Controllers;

public class CustomerController : Controller
{
    private readonly GeneralStoreDbContext _ctx;
    public CustomerController(GeneralStoreDbContext dbContext)
    {
        _ctx = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        List<CustomerIndexViewModel> customers = await _ctx.Customers
            .Select(customer => new CustomerIndexViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            })
            .ToListAsync();
            
        return View(customers);
    }


}
