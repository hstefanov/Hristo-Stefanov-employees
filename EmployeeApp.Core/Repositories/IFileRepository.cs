using System.Collections.Generic;

namespace EmployeeApp.Core.Repositories
{
    public interface IFileRepository<TEntity> 
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
    }
}
