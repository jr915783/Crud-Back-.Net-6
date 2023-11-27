using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repositories.Repository
{
    public class TaskRepository : ITask
    {
        public readonly DataContext _dataContex;

        
        public TaskRepository(DataContext dataContex)
        {
            _dataContex = dataContex;
        }

        public async Task Delete(int userId, int Id)
        {
            var entity = await GetById(userId, Id);

            _dataContex.Task.Remove(entity);

            await _dataContex.SaveChangesAsync();

        }

        public async Task<PageList<TaskEntity>> GetAll(int userId, PageParams pageParams)
        {
            
            IQueryable<TaskEntity> query = _dataContex.Task.Where(e => e.UserId == userId && e.Title.ToLower().Contains(pageParams.Term.ToLower()))
                                                                        .AsNoTracking().OrderBy(e => e.Id); 
            return await PageList<TaskEntity>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<TaskEntity[]> GetTaskByName(int userId, string name)
        {
            return await _dataContex.Task.Where(x => x.UserId == userId && x.Title.ToLower().Contains(name.ToLower())).ToArrayAsync();
        }

        public async Task<TaskEntity> GetById(int userId, int Id)
        {
            return await _dataContex.Task.Where(x => x.UserId == userId && x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<TaskEntity> Insert(int userId, TaskEntity entity)
        {
            entity.UserId = userId;
             _dataContex.Task.AddAsync(entity);
            await _dataContex.SaveChangesAsync();
            return entity;
        }

        public async Task Update(int userId, TaskEntity entity)
        {
            entity.UserId = userId;
            _dataContex.Task.Update(entity);
            await _dataContex.SaveChangesAsync();
        }        
    }
}
