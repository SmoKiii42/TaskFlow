using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.Views;


namespace TaskFlow
{    
    class TaskService
    {
        public TaskService()
        {
            _context = new AppDbContext();
        }


        private readonly AppDbContext _context;
        public void AddTask(
        string title,
        string description,
        string priority,
        DateTime dueDate,
        int workspaceId)

        {

            TaskItem task = new TaskItem
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                WorkspaceId = workspaceId,
                Status = priority,
                IsCompleted = false
            };


            _context.TaskItems.Add(task);

            _context.SaveChanges();
        }




        public void RemoveTask(int taskId)
        {
            var task = _context.TaskItems.FirstOrDefault(t => t.Id == taskId);


            if (task != null)
            {
                _context.TaskItems.Remove(task);
                _context.SaveChanges();
            }
        }



        public List<TaskItem> GetTaskItems(int workspaceId)
        {
            return _context.TaskItems.Where(t => t.WorkspaceId == workspaceId).ToList();
        }
    }



}
