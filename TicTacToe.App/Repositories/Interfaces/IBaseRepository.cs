using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.App.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity, TPrimaryKey>
    {
        Task<TEntity> Create(TEntity src);
        Task<TEntity> Get(TPrimaryKey id);
        Task<List<TEntity>> GetAll();
        public Task Update(TEntity src);
        Task Delete(Guid id);
    }
}
