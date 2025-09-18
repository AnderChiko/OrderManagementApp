using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Services;

namespace OrderManagement.Application.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<List<T>> GetAllAsync() => _repository.GetAllAsync();

        public Task<T?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task AddAsync(T entity) => _repository.AddAsync(entity);

        public Task UpdateAsync(T entity) => _repository.UpdateAsync(entity);

        public Task DeleteAsync(T entity) => _repository.DeleteAsync(entity);
    }
}
