namespace GeneralStoreMVC.Data.Entities;

public partial class TransactionEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int CustomerId { get; set; }

    public int Quantity { get; set; }

    public DateTime DateOfTransaction { get; set; }

    public virtual CustomerEntity Customer { get; set; } = null!;

    public virtual ProductEntity Product { get; set; } = null!;
}
