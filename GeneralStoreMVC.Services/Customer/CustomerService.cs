using GeneralStoreMVC.Data;
using GeneralStoreMVC.Data.Entities;
using GeneralStoreMVC.Models.Customer;
using Microsoft.EntityFrameworkCore;

namespace GeneralStoreMVC.Services.Customer;

public class CustomerService : ICustomerService
{
    private readonly GeneralStoreDbContext _cxt;
    public CustomerService(GeneralStoreDbContext cxt)
    {
        _cxt = cxt;
    }

    public async Task<bool> CreateCustomerAsync(CustomerCreateViewModel model)
    {
        CustomerEntity entity = new()
        {
            Name = model.Name,
            Email = model.Email
        };
        _cxt.Customers.Add(entity);
        return await _cxt.SaveChangesAsync() == 1;
    }

    public async Task<IEnumerable<CustomerIndexViewModel>> GetCustomersAsync()
    {
        List<CustomerIndexViewModel> customers = await _cxt.Customers
            .Select(customer => new CustomerIndexViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            })
            .ToListAsync();

        return customers;
    }

    public async Task<CustomerDetailViewModel?> GetCustomerDetailAsync(int id)
    {
        CustomerEntity? customer = await _cxt.Customers
            .FirstOrDefaultAsync(c => c.Id == id);

        return customer is null ? null : new()
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email
        };
    }
}


