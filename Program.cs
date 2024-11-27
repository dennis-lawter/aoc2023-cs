using System.Reflection;
using System.Text.RegularExpressions;

using solutions;

internal class Program
{
    private static (string, string)? FindMatch(String s)
    {
        var pattern = @"^d(\d\d)s(\d)$";
        var r = new Regex(pattern);

        var match = r.Match(s);
        if (match == null)
        {
            return null;
        }

        String day = "";
        String sol = "";

        foreach (Group cap in match.Groups.Cast<Group>())
        {
            if (cap.Index == 1)
            {
                day = cap.Value ?? "";
            }
            else if (cap.Index == 4)
            {
                sol = cap.Value ?? "";
            }
        }
        return (day, sol);
    }
    private static void Main(String[] args)
    {
        string day = "";
        string sol = "";
        foreach (var arg in args)
        {
            var match = Program.FindMatch(arg);
            if (match != null)
            {
                (string, string) matchConfirmed = ((string, string))match;
                day = matchConfirmed.Item1;
                sol = matchConfirmed.Item2;
            }
        }
        while (day == "" || sol == "")
        {
            Console.WriteLine("Enter the problem identifier (format: d00s0)");
            var s = Console.ReadLine() ?? "";
            s = s.Trim();
            var match = Program.FindMatch(s);
            if (match != null)
            {
                (string, string) matchConfirmed = ((string, string))match;
                day = matchConfirmed.Item1;
                sol = matchConfirmed.Item2;
            }
        }

        var className = $"solutions.Day{day}";
        var funcName = $"Solution{sol}";
        Type type = Type.GetType(className) ?? throw new Exception($"Invalid day: {className}");
        var func = type.GetMethod(funcName) ?? throw new Exception($"Invalid solution: {funcName}");
        var obj = Activator.CreateInstance(type);

        var filename = $"input/{day}.txt";
        if (args.Contains("-e"))
        {
            filename = $"example/{day}.txt";
        }

        var inputStr = File.ReadAllText(filename);

        var start = DateTime.Now;

        func.Invoke(obj, [inputStr]);

        var end = DateTime.Now;

        var timeRunning = end - start;

        Console.WriteLine("Time Elapsed: " + timeRunning.ToString());
    }
}
