namespace cyaFramework.Domain.Contracts.Entities
{
    public interface IEntityBase<TId>
    {
        TId Id { get; set; }
        bool IsNew();
    }
}