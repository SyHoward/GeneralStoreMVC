using GeneralStoreMVC.Models.Customer;

namespace GeneralStoreMVC.Services.Customer;

    public interface ICustomerService
{
    Task<IEnumerable<CustomerIndexViewModel>> GetCustomersAsync();
    Task<bool> CreateCustomerAsync(CustomerCreateViewModel model);
    
}
