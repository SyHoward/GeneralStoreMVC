using GeneralStoreMVC.Models.Customer;
using GeneralStoreMVC.Services.Customer;
using Microsoft.AspNetCore.Mvc;

namespace GeneralStoreMVC.MVC.Controllers;

public class CustomerController : Controller
{
    private readonly ICustomerService _service;
    public CustomerController(ICustomerService service)
    {
        _service = service;
    }

[HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CustomerCreateViewModel model)
    {
        if(!ModelState.IsValid)
            return View(model);

        await _service.CreateCustomerAsync(model);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<CustomerIndexViewModel> customers = (List<CustomerIndexViewModel>)await _service.GetCustomersAsync();
        return View(customers);
    }
    

    
}






