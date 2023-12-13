using AuthExam.Data;
using AuthExam.Models.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuthExam.Services
{
    public class TaskService : ITaskService
    {
        private readonly DataContext _context;

        /// <summary>
        /// Task Service
        /// </summary>
        /// <param name="context">Database Context</param>
        public TaskService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get Tasks
        /// </summary>
        /// <returns>Task Collections</returns>
        public  Dictionary<string, List<Models.Task>> GetTasks()
        {

            return _context.Tasks
                    .AsEnumerable() // Fetch data from the database
                    .GroupBy(task => task.CompletionFlag)
                    .ToDictionary(group => group.Key, group => group.ToList());
        }

        /// <summary>
        /// Get Task By Id
        /// </summary>
        /// <param name="id">Task ID</param>
        /// <returns>Specific Task</returns>
        public async Task<Models.Task> GetTaskById(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="task">Updated task values</param>
        public async void UpdateTask(Models.Task task)
        { 
            _context.Tasks.Update(task);
           await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Create Task
        /// </summary>
        /// <param name="task">Create Task Values</param>
        public async void CreateTask(TaskRequest task)
        {
            var taskModel = new Models.Task()
            {
                Description = task.Description,
                CompletionFlag = task.CompletionFlag,
                isActive = task.isActive
            };

            _context.Tasks.Add(taskModel);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="task">Delete Task Values</param>
        public async void DeleteTask(Models.Task task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
