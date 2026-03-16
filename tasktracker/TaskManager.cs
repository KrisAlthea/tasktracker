using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace tasktracker;

public class TaskManager
{
    private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tasks.json");
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        Converters = { new StatusConverter() },
        WriteIndented = true
    };

    public Dictionary<int, MyTask> Tasks;

    public TaskManager()
    {
        Tasks = ReadFromFile();
    }

    public List<MyTask> GetTasksByStatus(Status status)
    {
        var tasks = new List<MyTask>();
        foreach (var task in Tasks.Values)
        {
            if (task.status == status)
            {
                tasks.Add(task);
            }
        }
        return tasks;
    }

    public List<MyTask> GetAllTasks()
    {
        return Tasks.Values.ToList();
    }

    public void SaveAllTasks()
    {
        var taskList = Tasks.Values.ToList();
        var jsonString = JsonSerializer.Serialize(taskList, options);
        File.WriteAllText(filePath, jsonString);
    }

    public Dictionary<int, MyTask> ReadFromFile()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return new Dictionary<int, MyTask>();
            }

            var jsonString = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return new Dictionary<int, MyTask>();
            }

            List<MyTask>? tasks = JsonSerializer.Deserialize<List<MyTask>>(jsonString, options);
            if (tasks != null && tasks.Count > 0)
            {
                var taskDic = new Dictionary<int, MyTask>();
                foreach (var task in tasks)
                {
                    taskDic.Add(task.id, task);
                }
                return taskDic;
            }
            return new Dictionary<int, MyTask>();
        }
        catch (Exception)
        {
            return new Dictionary<int, MyTask>();
        }
    }

    public int GetNextId()
    {
        if (Tasks.Count == 0)
        {
            return 1;
        }
        return Tasks.Keys.Max() + 1;
    }
}