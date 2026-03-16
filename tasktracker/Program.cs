/*
Task Tracker
# Adding a new task
task-cli add "Buy groceries"
# Output: Task added successfully (ID: 1)

# Updating and deleting tasks
task-cli update 1 "Buy groceries and cook dinner"
task-cli delete 1

# Marking a task as in progress or done
task-cli mark-in-progress 1
task-cli mark-done 1

# Listing all tasks
task-cli list

# Listing tasks by status
task-cli list done
task-cli list todo
task-cli list in-progress

*/


using System;
using tasktracker;
Console.WriteLine("welcome to task tracker!");

var taskManager = new TaskManager();
System.Console.WriteLine("use the commands below: <> is needed, () is optional");
System.Console.WriteLine("add <\"description\">");
System.Console.WriteLine("update <id> <\"new description\">");
System.Console.WriteLine("delete <id>");
System.Console.WriteLine("mark-in-progress <id>");
System.Console.WriteLine("mark-done <id>");
System.Console.WriteLine("list (done|todo|in-progress)");

var input = Console.ReadLine();
while (string.IsNullOrWhiteSpace(input))
{
    input = Console.ReadLine();
}
var taskService = new TaskService();
var commands = input.Split(' ');

switch (commands[0])
{
    case "add": taskService.AddTask(commands[1]); break;
    case "update": taskService.UpdateTask(int.Parse(commands[1]), commands[2]); break;
    case "delete": taskService.DeleteTask(int.Parse(commands[1])); break;
    case "mark-in-progress": taskService.UpdateToInProgress(int.Parse(commands[1])); break;
    case "mark-done": taskService.UpdateToDone(int.Parse(commands[1])); break;
    case "list": taskManager.GetTasksByStatus(StatusHelper.GetStatus(commands[1])); break;
    default: System.Console.WriteLine("invalid command"); break;
}