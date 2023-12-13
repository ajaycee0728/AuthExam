
using AuthExam.Models.Requests;
using AuthExam.Services;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 

namespace AuthExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        /// <summary>
        /// Task Service Interface
        /// </summary>
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Get All Tasks
        /// </summary>
        /// <returns>Collection of Tasks</returns>
        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _taskService.GetTasks();
            return Ok(tasks);
        }

        /// <summary>
        /// Get Task By Id
        /// </summary>
        /// <param name="id">Task ID</param>
        /// <returns>Specific Task</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _taskService.GetTaskById(id);

            if (task is null)
            {
                return NotFound($"Task with ID {id} not found");
            }

            return Ok(task);
        }

        /// <summary>
        /// Create Task
        /// </summary>
        /// <param name="task">Create Task Values</param>
        [Authorize]
        [HttpPost]
        public IActionResult CreateTask(TaskRequest task)
        {  
            _taskService.CreateTask(task);
            return Ok(task);
        }

        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="task">Updated task values</param>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskRequest updatedTask)
        {
            var existingTask = await _taskService.GetTaskById(id);

            if (existingTask is null)
            {
                return NotFound($"Task with ID {id} not found");
            }

            existingTask.Description = updatedTask.Description;
            existingTask.CompletionFlag = updatedTask.CompletionFlag;
            existingTask.isActive = updatedTask.isActive;

            _taskService.UpdateTask(existingTask);

            return NoContent();
        }

        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="id">Task ID</param>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var existingTask = await _taskService.GetTaskById(id);

            if (existingTask is null)
            {
                return NotFound($"Task with ID {id} not found");
            }

            _taskService.DeleteTask(existingTask);
            return NoContent();
        }
    }
}
