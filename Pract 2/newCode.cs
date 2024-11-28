using System;
using System.Collections.Generic;

namespace TaskManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            var taskManager = new TaskManager();
            while (true)
            {
                Console.WriteLine("\n===================");
                Console.WriteLine("Task Manager Menu");
                Console.WriteLine("===================");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View Tasks");
                Console.WriteLine("3. Delete Task");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter task name: ");
                        string taskName = Console.ReadLine();
                        taskManager.AddTask(taskName); // Remove Parameter применён
                        break;
                    case "2":
                        Console.WriteLine("\n===== Task List =====");
                        taskManager.DisplayTasks();
                        Console.WriteLine("=====================\n");
                        break;
                    case "3":
                        Console.Write("Enter task index to delete: ");
                        int index;
                        if (int.TryParse(Console.ReadLine(), out index))
                        {
                            taskManager.DeleteTaskByIndex(index);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }

    public abstract class BaseTaskManager
    {
        protected List<Task> tasks = new List<Task>();

        public abstract void DisplayTasks();

        protected void RemoveTask(int index)
        {
            if (index >= 1 && index <= tasks.Count)
            {
                tasks.RemoveAt(index - 1);
                Console.WriteLine("Task removed.");
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }
    }

    public class TaskManager : BaseTaskManager
    {
        public void AddTask(string name)
        {
            tasks.Add(new Task { Name = name, CreationDate = DateTime.Now });
            Console.WriteLine("Task added.");
        }

        public void DeleteTaskByIndex(int index)
        {
            RemoveTask(index); // Hidden Method
        }

        public override void DisplayTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks.");
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i].Name} (Created: {tasks[i].CreationDate})");
            }
        }
    }

    public class Task
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
