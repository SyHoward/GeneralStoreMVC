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
        IEnumerable<CustomerIndexViewModel> customers = await _service.GetCustomersAsync();
        return View(customers);
    }
    
    public async Task<IActionResult> Details(int id)
    {
        CustomerDetailViewModel? model = await _service.GetCustomerDetailAsync(id);

        if (model is null)
            return RedirectToAction(nameof(Index));
    
        return View(model);
    }
}






