using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Entities;
using TaskManager.API.Repositories.TaskRepository;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageTaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public ManageTaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet("GetAllTask")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks(Guid? userId = null)
        {
            var tasks = await _taskRepository.GetTasksList(userId);
            return Ok(tasks);
        }

        [HttpGet("GetTaskById/{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskById(Guid id)
        {
            var task = await _taskRepository.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost("CreateTask")]
        public async Task<ActionResult<TaskItem>> AddTask([FromBody] TaskItem task)
        {
            if (task == null)
            {
                return BadRequest("Task cannot be null.");
            }

            await _taskRepository.AddTask(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("UpdateTaskById/{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskItem task)
        {
            if (id != task.Id)
            {
                return BadRequest("Task ID mismatch.");
            }

            var existingTask = await _taskRepository.GetTaskById(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            await _taskRepository.UpdateTask(task);
            return NoContent();
        }

        [HttpDelete("DeleteTaskById/{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await _taskRepository.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }

            await _taskRepository.DeleteTaskById(id);
            return NoContent();
        }
    }
}
