using Domain.Entities;
using Repositories.Base;

namespace Repositories.Interface
{
    public interface ITask
    {
        Task<TaskEntity> Insert(int userId, TaskEntity entity);
        Task Update(int userId, TaskEntity entity);
        Task Delete(int userId, int Id);
        Task<PageList<TaskEntity>> GetAll(int userId, PageParams pageParams);
        Task<TaskEntity> GetById(int userId, int Id);
        Task<TaskEntity[]> GetTaskByName(int userId, string name);
    }

    
}
