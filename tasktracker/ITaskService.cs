using System;
using Microsoft.VisualBasic.FileIO;

namespace tasktracker;

public interface ITaskService
{
    public MyTask? GetTaskById(int id);
    public MyTask GetTaskByDescription(string description);
    public bool AddTask(string description);
    public bool UpdateTask(int id, string description);
    public bool UpdateToInProgress(int id);
    public bool UpdateToDone(int id);
    public bool DeleteTask(int id);

}
