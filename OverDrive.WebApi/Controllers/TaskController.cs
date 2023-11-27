using Data.Context;
using Domain.Entities;
using Inlog.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;

namespace TesteCrud.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITask _task;        

        public TaskController(ITask task, DataContext dataContex)
        {
            _task = task;           
        }

        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask(TaskEntity task)
        {
            try
            {
                if (task == null)
                {
                    return BadRequest($"Não Foi possível adicionar essa tarefa {task} !");
                }
               var taskReturn =  await _task.Insert(User.GetUserId(), task);
                return Ok(taskReturn);
                      
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        [HttpGet("ListTask")]
        public async Task<IActionResult> ListTask([FromQuery] PageParams pageParams)
        {
            var result = await _task.GetAll(User.GetUserId(), pageParams);
            Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
            return Ok(result.ToList());
        }

        [HttpGet("GetTaskById/{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var result = await _task.GetById(User.GetUserId(), id);
            if(result == null)
            {
                return NotFound($"Tarefa com id {id}, não encontrado!");
            }
            return Ok(result);
        }

        [HttpGet("GetTaskByName/{name}")]
        public async Task<IActionResult> GetTaskByName(string name)
        {
            var result = await _task.GetTaskByName(User.GetUserId(), name);
          
            return Ok(result);
        }

        [HttpDelete("DeletarTask/{id}")]
        public async Task<IActionResult> DeletarTask(int id)
        {
            var result = await _task.GetById(User.GetUserId(), id);
            if (result == null)
            {
                return NotFound($"id {id}, não encontrado!");
            }
            await _task.Delete(User.GetUserId(), id);           
            return Ok(new { result = "Tarefa deletada com sucesso!" });
        }

        [HttpPut("UpdateTask")]
        public async Task<IActionResult> UpdateTask(TaskEntity task)
        {          

            if(task != null)
            {
                try
                {
                    await _task.Update(User.GetUserId(), task);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return Ok(new { result  = "Tarefa atualizada com sucesso!" });

            }
            else { return BadRequest("Objeto não encontrado!"); }            

        }
       
    }
}
