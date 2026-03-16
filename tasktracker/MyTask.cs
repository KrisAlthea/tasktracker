using System.Diagnostics;
using System.Text.Json.Serialization;

namespace tasktracker;

public class MyTask
{
    private static int cnt = 1;
    public int id { get; set; }
    public string description { get; set; }
    /// <summary>
    /// todo, in-progress, done
    /// </summary>
    [JsonPropertyName("status")]
    public Status status { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }

    public MyTask(string description)
    {
        this.id = cnt++;
        this.description = description;
        this.status = Status.todo;
        this.createdAt = DateTime.Now;
        this.updatedAt = DateTime.Now;
    }

}

public enum Status
{
    [JsonPropertyName("todo")]
    todo,
    
    [JsonPropertyName("in-progress")]
    in_progress,
    
    [JsonPropertyName("done")]
    done
}

public static class StatusHelper
{
    public static Status GetStatus(string statusString)
    {
        return statusString switch
        {
            "todo" => Status.todo,
            "in-progress" => Status.in_progress,
            "done" => Status.done,
            _ => Status.done
        };
    }
}
