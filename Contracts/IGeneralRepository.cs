namespace Booking_Api.Contracts
{
    public interface IGeneralRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity? GetById(Guid guid);
        TEntity? Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
    }
}
