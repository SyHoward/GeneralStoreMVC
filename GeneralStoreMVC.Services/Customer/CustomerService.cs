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

    public async Task<bool> EditCustomerAsync(CustomerEditViewModel model)
    {
        CustomerEntity? entity = await _cxt.Customers.FindAsync(model.Id);

        if (entity is null)
            return false;

        entity.Name = model.Name;
        entity.Email = model.Email;
        return await _cxt.SaveChangesAsync() == 1;
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var entity = await _cxt.Customers
            .Include(c => c.Transactions)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (entity is null)
            return false;

        if (entity.Transactions.Count > 0)
        {
            _cxt.Transactions.RemoveRange(entity.Transactions);
        }

        _cxt.Customers.Remove(entity);

        if (_cxt.SaveChanges() != 1 + entity.Transactions.Count)
        {
            return false;
        }

        return true;
        
    }

}


