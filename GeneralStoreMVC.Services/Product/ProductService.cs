using GeneralStoreMVC.Data;
using GeneralStoreMVC.Data.Entities;
using GeneralStoreMVC.Models.Product;
using GeneralStoreMVC.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace GeneralStoreMVC.Services.Product;

public class ProductService : IProductService
{
    private readonly GeneralStoreDbContext _cxt;
    public ProductService(GeneralStoreDbContext cxt)
    {
        _cxt = cxt;
    }

    public async Task<bool> CreateProductAsync(ProductCreateVM product)
    {
        ProductEntity entity = new()
        {
            Name = product.Name,
            Price = product.Price,
            QuantityInStock = product.QuantityInStock
        };
    _cxt.Products.Add(entity);
    return await _cxt.SaveChangesAsync() == 1;
    }

    public async Task<List<ProductIndexVM>> GetProductsAsync()
    {
        List<ProductIndexVM> products =await _cxt.Products
            .Select(p => new ProductIndexVM
            {
                Id = p.Id,
                Name = p.Name, 
                QuantityInStock = p.QuantityInStock
            })
            .ToListAsync();
            
        return products;
    }

    public async Task<ProductDetailVM?> GetProductDetailAsync(int id)
    {
        ProductEntity? product = await _cxt.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        return product is null ? null : new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            QuantityInStock = product.QuantityInStock
        };
    }

    public Task<ProductEditVM> GetEditProductAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EditProductAsync(int id, ProductEditVM product)
    {
        throw new NotImplementedException();
    }

    public async Task<TextResponse> DeleteProductAsync(int id)
    {
        var entity = await _cxt.Products
            .Include(c => c.Transactions)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (entity is null)
            return new TextResponse($"Product #{id} does not exist");

        if (entity.Transactions.Count > 0)
            _cxt.Transactions.RemoveRange(entity.Transactions);

        _cxt.Products.Remove(entity);

        if (_cxt.SaveChanges() != 1 + entity.Transactions.Count)
            return new TextResponse($"Cannot delete Product #{id}");

        return new TextResponse("Product deleted successfully");
    }
}
