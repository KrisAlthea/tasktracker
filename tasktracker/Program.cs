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
using System.Text.RegularExpressions;
using tasktracker;

Console.WriteLine("Welcome to Task Tracker!");
Console.WriteLine("Use the commands below: <> is required, () is optional");
Console.WriteLine("  add <\"description\">");
Console.WriteLine("  update <id> <\"new description\">");
Console.WriteLine("  delete <id>");
Console.WriteLine("  mark-in-progress <id>");
Console.WriteLine("  mark-done <id>");
Console.WriteLine("  list (done|todo|in-progress)");
Console.WriteLine("  exit");
Console.WriteLine();

var taskService = new TaskService();

while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
    {
        continue;
    }

    var commands = ParseCommand(input);

    if (commands.Length == 0)
    {
        Console.WriteLine("Invalid command. Please try again.");
        continue;
    }

    switch (commands[0])
    {
        case "help":
            Console.WriteLine("Available commands:");
            Console.WriteLine("  add <\"description\">          - Add a new task");
            Console.WriteLine("  update <id> <\"new description\"> - Update a task");
            Console.WriteLine("  delete <id>                   - Delete a task");
            Console.WriteLine("  mark-in-progress <id>         - Mark task as in-progress");
            Console.WriteLine("  mark-done <id>                - Mark task as done");
            Console.WriteLine("  list (done|todo|in-progress)  - List tasks");
            Console.WriteLine("  exit / quit                   - Exit the program");
            break;

        case "exit":
        case "quit":
            Console.WriteLine("Goodbye!");
            return;

        case "add":
            if (commands.Length < 2)
            {
                Console.WriteLine("Error: Missing description. Usage: add \"description\"");
            }
            else
            {
                var id = taskService.AddTask(commands[1]);
                Console.WriteLine($"Task added successfully (ID: {id})");
            }
            break;

        case "update":
            if (commands.Length < 3)
            {
                Console.WriteLine("Error: Missing arguments. Usage: update <id> \"new description\"");
            }
            else if (!int.TryParse(commands[1], out int updateId))
            {
                Console.WriteLine("Error: Invalid ID. ID must be a number.");
            }
            else
            {
                if (taskService.UpdateTask(updateId, commands[2]))
                {
                    Console.WriteLine($"Task {updateId} updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Error: Task with ID {updateId} not found.");
                }
            }
            break;

        case "delete":
            if (commands.Length < 2)
            {
                Console.WriteLine("Error: Missing ID. Usage: delete <id>");
            }
            else if (!int.TryParse(commands[1], out int deleteId))
            {
                Console.WriteLine("Error: Invalid ID. ID must be a number.");
            }
            else
            {
                if (taskService.DeleteTask(deleteId))
                {
                    Console.WriteLine($"Task {deleteId} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Error: Task with ID {deleteId} not found.");
                }
            }
            break;

        case "mark-in-progress":
            if (commands.Length < 2)
            {
                Console.WriteLine("Error: Missing ID. Usage: mark-in-progress <id>");
            }
            else if (!int.TryParse(commands[1], out int progressId))
            {
                Console.WriteLine("Error: Invalid ID. ID must be a number.");
            }
            else
            {
                if (taskService.UpdateToInProgress(progressId))
                {
                    Console.WriteLine($"Task {progressId} marked as in-progress.");
                }
                else
                {
                    Console.WriteLine($"Error: Task with ID {progressId} not found.");
                }
            }
            break;

        case "mark-done":
            if (commands.Length < 2)
            {
                Console.WriteLine("Error: Missing ID. Usage: mark-done <id>");
            }
            else if (!int.TryParse(commands[1], out int doneId))
            {
                Console.WriteLine("Error: Invalid ID. ID must be a number.");
            }
            else
            {
                if (taskService.UpdateToDone(doneId))
                {
                    Console.WriteLine($"Task {doneId} marked as done.");
                }
                else
                {
                    Console.WriteLine($"Error: Task with ID {doneId} not found.");
                }
            }
            break;

        case "list":
            List<MyTask> tasks;
            if (commands.Length >= 2)
            {
                var status = StatusHelper.GetStatus(commands[1]);
                tasks = taskService.GetTasksByStatus(status);
            }
            else
            {
                tasks = taskService.GetAllTasks();
            }

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                Console.WriteLine("ID  | Status      | Description");
                Console.WriteLine("----|-------------|------------");
                foreach (var task in tasks.OrderBy(t => t.id))
                {
                    var statusStr = task.status switch
                    {
                        Status.todo => "todo",
                        Status.in_progress => "in-progress",
                        Status.done => "done",
                        _ => "unknown"
                    };
                    Console.WriteLine($"{task.id,-3} | {statusStr,-11} | {task.description}");
                }
            }
            break;

        default:
            Console.WriteLine($"Unknown command: {commands[0]}. Type 'help' for available commands.");
            break;
    }
}

/// <summary>
/// 解析命令行输入，支持带引号的字符串参数
/// </summary>
static string[] ParseCommand(string input)
{
    var result = new List<string>();
    var regex = new Regex(@"[\""]([^\""]*)[\""]|(\S+)");
    var matches = regex.Matches(input);

    foreach (Match match in matches)
    {
        // 如果匹配到引号内的内容，取第一个组；否则取第二个组（无引号的词）
        var value = match.Groups[1].Success ? match.Groups[1].Value : match.Groups[2].Value;
        result.Add(value);
    }

    return result.ToArray();
}