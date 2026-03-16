using System;

namespace tasktracker;

public class TaskService : ITaskService
{
    public TaskManager taskManager = new TaskManager();
    public bool AddTask(string description)
    {
        var _task = new MyTask(description);
        taskManager.SaveTasktoFile(_task);
        return true;
    }

    public bool DeleteTask(int id)
    {
        var task = GetTaskById(id);
        if (task is not null)
            taskManager.Tasks.Remove(task.id);
        return true;
    }

    public MyTask GetTaskByDescription(string description)
    {
        throw new NotImplementedException();
    }

    public MyTask? GetTaskById(int id)
    {
        if (taskManager.Tasks.ContainsKey(id))
        {
            return taskManager.Tasks.GetValueOrDefault(id);
        }
        return null;
    }

    public bool UpdateTask(int id, string description)
    {
        var task = taskManager.Tasks.GetValueOrDefault(id);
        if (task is null)
        {
            return false;
        }
        else
        {
            task.description = description;
        }
        return true;
    }

    public bool UpdateToDone(int id)
    {
        var task = taskManager.Tasks.GetValueOrDefault(id);
        if (task is null)
        {
            return false;
        }
        else
        {
            task.status = Status.done;
        }
        return true;
    }

    public bool UpdateToInProgress(int id)
    {
        var task = taskManager.Tasks.GetValueOrDefault(id);
        if (task is null)
        {
            return false;
        }
        else
        {
            task.status = Status.in_progress;
        }
        return true;
    }
}
