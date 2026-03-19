using System.Text.Json;

namespace tasktracker;

public class TaskManager
{
    private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tasks.json");
    private static readonly JsonSerializerOptions _options = new()
    {
        Converters = { new StatusConverter() },
        WriteIndented = true
    };

    private Dictionary<int, MyTask> _tasks;

    public TaskManager()
    {
        _tasks = LoadFromFile();
    }

    public int AddTask(string description)
    {
        var id = GetNextId();
        _tasks[id] = new MyTask(id, description);
        Save();
        return id;
    }

    public bool UpdateTask(int id, string description)
    {
        if (!_tasks.TryGetValue(id, out var task)) return false;
        task.description = description;
        task.updatedAt = DateTime.Now;
        Save();
        return true;
    }

    public bool UpdateToInProgress(int id)
    {
        if (!_tasks.TryGetValue(id, out var task)) return false;
        task.status = Status.in_progress;
        task.updatedAt = DateTime.Now;
        Save();
        return true;
    }

    public bool UpdateToDone(int id)
    {
        if (!_tasks.TryGetValue(id, out var task)) return false;
        task.status = Status.done;
        task.updatedAt = DateTime.Now;
        Save();
        return true;
    }

    public bool DeleteTask(int id)
    {
        if (!_tasks.Remove(id)) return false;
        Save();
        return true;
    }

    public MyTask? GetTaskById(int id) => _tasks.GetValueOrDefault(id);

    public List<MyTask> GetAllTasks() => _tasks.Values.ToList();

    public List<MyTask> GetTasksByStatus(Status status) =>
        _tasks.Values.Where(t => t.status == status).ToList();

    private int GetNextId() => _tasks.Count == 0 ? 1 : _tasks.Keys.Max() + 1;

    private void Save() => File.WriteAllText(_filePath, JsonSerializer.Serialize(_tasks.Values.ToList(), _options));

    private Dictionary<int, MyTask> LoadFromFile()
    {
        if (!File.Exists(_filePath)) return [];

        var json = File.ReadAllText(_filePath);
        if (string.IsNullOrWhiteSpace(json)) return [];

        var list = JsonSerializer.Deserialize<List<MyTask>>(json, _options);
        if (list == null || list.Count == 0) return [];

        return list.ToDictionary(t => t.id);
    }
}