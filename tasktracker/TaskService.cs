using System;
using System.Security.Principal;

namespace tasktracker;

public class TaskService : ITaskService
{
    private readonly TaskManager taskManager;

    public TaskService()
    {
        taskManager = new TaskManager();
    }

    public int AddTask(string description)
    {
        var id = taskManager.GetNextId();
        var task = new MyTask(id, description);
        taskManager.Tasks.Add(id, task);
        taskManager.SaveAllTasks();
        return id;
    }

    public bool DeleteTask(int id)
    {
        if (taskManager.Tasks.ContainsKey(id))
        {
            taskManager.Tasks.Remove(id);
            taskManager.SaveAllTasks();
            return true;
        }
        return false;
    }

    public MyTask? GetTaskByDescription(string description)
    {
        foreach (var task in taskManager.Tasks.Values)
        {
            if (task.description.Equals(description, StringComparison.OrdinalIgnoreCase))
            {
                return task;
            }
        }
        return null;
    }

    public MyTask? GetTaskById(int id)
    {
        if (taskManager.Tasks.TryGetValue(id, out var task))
        {
            return task;
        }
        return null;
    }

    public bool UpdateTask(int id, string description)
    {
        if (taskManager.Tasks.TryGetValue(id, out var task))
        {
            task.description = description;
            task.updatedAt = DateTime.Now;
            taskManager.SaveAllTasks();
            return true;
        }
        return false;
    }

    public bool UpdateToDone(int id)
    {
        if (taskManager.Tasks.TryGetValue(id, out var task))
        {
            task.status = Status.done;
            task.updatedAt = DateTime.Now;
            taskManager.SaveAllTasks();
            return true;
        }
        return false;
    }

    public bool UpdateToInProgress(int id)
    {
        if (taskManager.Tasks.TryGetValue(id, out var task))
        {
            task.status = Status.in_progress;
            task.updatedAt = DateTime.Now;
            taskManager.SaveAllTasks();
            return true;
        }
        return false;
    }

    public List<MyTask> GetAllTasks()
    {
        return taskManager.GetAllTasks();
    }

    public List<MyTask> GetTasksByStatus(Status status)
    {
        return taskManager.GetTasksByStatus(status);
    }
}