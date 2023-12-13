using GeneralStoreMVC.Models.Customer;

namespace GeneralStoreMVC.Services.Customer;

    public interface ICustomerService
{
    Task<IEnumerable<CustomerIndexViewModel>> GetCustomersAsync();
    Task<bool> CreateCustomerAsync(CustomerCreateViewModel model);
    Task<CustomerDetailViewModel?> GetCustomerDetailAsync(int id);
    Task<bool> EditCustomerAsync(CustomerEditViewModel model);
    Task<bool> DeleteCustomerAsync(int id);
}
