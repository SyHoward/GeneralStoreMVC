using GeneralStoreMVC.Models.Product;
using GeneralStoreMVC.Models.Responses;

namespace GeneralStoreMVC.Services.Product;

public interface IProductService
{
    Task<bool> CreateProductAsync(ProductCreateVM product);
    Task<List<ProductIndexVM>> GetProductsAsync();
    Task<ProductDetailVM?> GetProductDetailAsync(int id);
    Task<ProductEditVM> GetEditProductAsync(int? id);
    Task<bool> EditProductAsync(int id, ProductEditVM product);
    Task<TextResponse> DeleteProductAsync(int id);


}
