namespace tasktracker;

public class MyTask
{
    public int id { get; set; }
    public string description { get; set; }
    /// <summary>
    /// todo, in-progress, done
    /// </summary>
    public Status status { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }

    public MyTask(int id, string description)
    {
        this.id = id;
        this.description = description;
        this.status = Status.todo;
        this.createdAt = DateTime.Now;
        this.updatedAt = DateTime.Now;
    }
}

public enum Status
{
    todo,
    in_progress,
    done
}

public static class StatusHelper
{
    public static Status GetStatus(string? statusString)
    {
        return statusString?.ToLower() switch
        {
            "todo" => Status.todo,
            "in-progress" => Status.in_progress,
            "done" => Status.done,
            _ => Status.todo  // 默认返回 todo 而非 done
        };
    }
}