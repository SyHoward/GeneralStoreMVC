using GeneralStoreMVC.Models.Product;
using GeneralStoreMVC.Services.Product;
using Microsoft.AspNetCore.Mvc;

namespace GeneralStoreMVC.MVC.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _service;
    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Name,QuantityInStock,Price")] ProductCreateVM product)
    {
        if(!ModelState.IsValid)
            return View(product);

        await _service.CreateProductAsync(product);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IList<ProductIndexVM> products = await _service.GetProductsAsync();
        return View(products);
    }


    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        ProductDetailVM? model = await _service.GetProductDetailAsync(id);

        if (model is null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var product = await _service.GetEditProductAsync(id);
        if (product == null)
            return NotFound();

        return View(product);
    } 

    [HttpPost]
    public async Task<IActionResult> Edit(int id, ProductEditVM model)
    {
        if (id != model.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            var edit = await _service.EditProductAsync(id, model);
            if (edit)
                return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var response = await _service.DeleteProductAsync(id);
        if (response.message == $"Product #{id} does not exist")
        {
            TempData["ErrorMsg"] = $"Product #{id} does not exist";
            return RedirectToAction(nameof(Index));
        }
            
        if (response.message == $"Cannot delete Product #{id}")
        {
            TempData["ErrorMsg"] = $"Cannot delete Product #{id}";
        }

        return RedirectToAction(nameof(Index));
    }

}