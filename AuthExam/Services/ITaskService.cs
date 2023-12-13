using AuthExam.Models.Requests;

namespace AuthExam.Services
{
    public interface ITaskService
    {
        /// <summary>
        /// Get Tasks
        /// </summary>
        /// <returns>Task Collections</returns>
        Dictionary<string, List<Models.Task>> GetTasks();

        /// <summary>
        /// Get Task By Id
        /// </summary>
        /// <param name="id">Task ID</param>
        /// <returns>Specific Task</returns>
        Task<Models.Task> GetTaskById(int  taskId);

        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="task">Updated task values</param>
        void CreateTask(TaskRequest task);

        /// <summary>
        /// Create Task
        /// </summary>
        /// <param name="task">Create Task Values</param>
        void UpdateTask(Models.Task task);

        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="task">Delete Task Values</param>
        void DeleteTask(Models.Task task);
    }
}
