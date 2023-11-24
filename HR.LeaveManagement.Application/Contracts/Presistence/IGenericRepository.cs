namespace HR.LeaveManagement.Application.Contracts.Presistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
