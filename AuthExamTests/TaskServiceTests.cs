using AuthExam.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using AuthExam;
using AuthExam.Data;
using AuthExamTests;
using AuthExam.Models.Requests; // Replace with the actual namespace of your application

public class TaskServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly DataContext _context;

    public TaskServiceTests(DatabaseFixture fixture)
    {
        _context = fixture.Context;
    }

    [Fact]
    public void CreateTask_ShouldAddTaskToDatabase()
    {
        // Arrange
        var taskService = new TaskService(_context);
        var newTask = new TaskRequest { Description = "New Task", CompletionFlag = "Todo", isActive = true };

        // Act
        taskService.CreateTask(newTask);

        // Assert
        var savedTask = _context.Tasks.FirstOrDefault(t => t.Description == "New Task");
        Assert.NotNull(savedTask);
    }

    [Fact]
    public async void GetTasks_ShouldReturnListOfTasks()
    {
        // Arrange
        var taskService = new TaskService(_context);

        // Add some tasks to the database for testing
        _context.Tasks.Add(new AuthExam.Models.Task { Description = "Task 1", CompletionFlag = "Todo", isActive = true });
        _context.Tasks.Add(new AuthExam.Models.Task { Description = "Task 2", CompletionFlag = "InProgress", isActive = true });
        _context.SaveChanges();

        // Act
        var groupedTasks = taskService.GetTasks();

        // Assert
        Assert.NotNull(groupedTasks);
        Assert.Equal(2, groupedTasks.Count); // Assuming two groups ("Todo" and "InProgress")

        // Check if the "Todo" group contains the expected task
        Assert.True(groupedTasks.ContainsKey("Todo"));
        Assert.Single(groupedTasks["Todo"]); // Assuming one task in the "Todo" group

        // Check if the "InProgress" group contains the expected task
        Assert.True(groupedTasks.ContainsKey("InProgress"));
        Assert.Single(groupedTasks["InProgress"]);
    }

    [Fact]
    public void UpdateTask_ShouldUpdateTaskInDatabase()
    {
        // Arrange
        // Add a task to the database
        var initialTask = new AuthExam.Models.Task { Description = "Initial Task", CompletionFlag = "Todo", isActive = true };
        _context.Tasks.Add(initialTask);
        _context.SaveChanges();

        var taskService = new TaskService(_context);
        var existingTask = _context.Tasks.First();

        // Act
        existingTask.Description = "Updated Task";
        taskService.UpdateTask(existingTask);

        // Assert
        var updatedTask = _context.Tasks.Find(existingTask.Id);
        Assert.NotNull(updatedTask);
        Assert.Equal("Updated Task", updatedTask.Description);
    }

    [Fact]
    public void DeleteTask_ShouldRemoveTaskFromDatabase()
    {
        // Arrange
        // Add a task to the database
        var taskToDelete = new AuthExam.Models.Task { Description = "Task to Delete", CompletionFlag = "Todo", isActive = true };
        _context.Tasks.Add(taskToDelete);
        _context.SaveChanges();

        var taskService = new TaskService(_context);
        var taskToDeleteFromDb = _context.Tasks.First();

        // Act
        taskService.DeleteTask(taskToDeleteFromDb);

        // Assert
        var deletedTask = _context.Tasks.Find(taskToDeleteFromDb.Id);
        Assert.Null(deletedTask);
    }
}
