using GeneralStoreMVC.Models.Customer;
using GeneralStoreMVC.Services.Customer;
using GeneralStoreMVC.Data.Entities;
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
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        CustomerDetailViewModel? model = await _service.GetCustomerDetailAsync(id);

        if (model is null)
            return RedirectToAction(nameof(Index));
    
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        CustomerDetailViewModel? customer = await _service.GetCustomerDetailAsync(id);
        if (customer is null)
        {
            return RedirectToAction(nameof(Index));
        }

        CustomerEditViewModel? model = new()
        {
            Id = customer.Id,
            Name = customer.Name ?? "",
            Email = customer.Email ?? ""
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, CustomerEditViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (await _service.EditCustomerAsync(model))
            return RedirectToAction(nameof(Index), new { id = id });

        ModelState.AddModelError("Save Error", "Unable to save to the database. Please try again later.");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete (int id)
    {
        var entity = await _service.DeleteCustomerAsync(id);
        if (entity == false)
        {
            TempData["ErrorMsg"] = $"Customer #{id} does not exist";
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }

}