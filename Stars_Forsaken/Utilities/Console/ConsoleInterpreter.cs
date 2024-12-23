using SharpDX.XAPO.Fx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Stars_Forsaken.Utilities.ConsoleInterpreter
{
    public static class ConsoleInterpreter
    {
        public static Dictionary<string, Action<string[]>> Commands = new Dictionary<string, Action<string[]>>()
        {
            { "help", ShowHelp },
            { "echo", Echo },
            { "log", _Log },
            { "exit", Exit }
        };

        public static void ExecuteCommand(string command)
        {
            string[] parts = command.Split(' ');
            string commandName = parts[0];
            string[] args = parts.Skip(1).ToArray();

            if (Commands.TryGetValue(commandName, out Action<string[]> action))
            {
                action(args);
            }
            else
            {
                Console.WriteLine("Command not found. Type 'help' to see a list of available commands.");
            }
        }

        private static void ShowHelp(string[] args)
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  help - Show this help message.");
            Console.WriteLine("  echo [text] - Echo the input text back to the console.");
            Console.WriteLine("  log [text] - Log the input text.");
            Console.WriteLine("  exit - Exit the application.");
        }

        private static void Echo(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: echo [text]");
            }
            else
            {
                Console.WriteLine(string.Join(" ", args));
            }
        }

        private static void _Log(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: log [text]");
            }
            else
            {
                Log.Debug(string.Join(" ", args));
            }
        }

        private static void Exit(string[] args)
        {
            Console.WriteLine("Shutting down Stars Forsaken");
            Environment.Exit(0);
        }
    }
}
