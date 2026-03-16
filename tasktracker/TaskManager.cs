using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace tasktracker;

public class TaskManager
{
    public static string file = "/home/wei/vs_ws/tasktracker/tasktracker/tasks.json";

    public Dictionary<int, MyTask> Tasks;
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        Converters = { new StatusConverter() },
        WriteIndented = true // 可选：让JSON格式更易读
    };
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

    public void SaveTasktoFile(MyTask task)
    {
        var serializedTask = JsonSerializer.Serialize<MyTask>(task, options);
        using var writer = new StreamWriter(file);
        writer.WriteLine(serializedTask);
    }

    public void SaveTaskstoFile(List<MyTask> tasks)
    {
        foreach (MyTask t in tasks)
        {
            SaveTasktoFile(t);
        }
    }

    public Dictionary<int, MyTask> ReadFromFile()
    {
        var jsonString = File.ReadAllText(file);
        List<MyTask>? tasks = JsonSerializer.Deserialize<List<MyTask>>(jsonString, options);
        if (tasks != null)
        {
            var taskDic = new Dictionary<int, MyTask>();
            foreach (var task in tasks)
            {
                taskDic.Add(task.id, task);
            }
            return taskDic;
        }
        else return new Dictionary<int, MyTask>();
    }

}
