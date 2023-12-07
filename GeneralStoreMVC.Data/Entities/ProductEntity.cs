namespace GeneralStoreMVC.Data.Entities;

public partial class ProductEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int QuantityInStock { get; set; }

    public double Price { get; set; }

    public virtual ICollection<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();
}
