using System.Reflection;
using System.Text.RegularExpressions;

using solutions;

internal class Program
{
    private static void Main(String[] args)
    {
        foreach (var a in args)
        {
            Console.WriteLine(a);
        }
        Console.WriteLine("Enter the problem identifier (format: d00s0)");
        var s = Console.ReadLine() ?? "";
        s = s.Trim();
        var pattern = @"^d(\d\d)s(\d)$";
        var r = new Regex(pattern);

        var match = r.Match(s);

        var day = "ERR";
        var sol = "ERR";

        foreach (Group cap in match.Groups.Cast<Group>())
        {
            if (cap.Index == 1)
            {
                day = cap.Value;
            }
            else if (cap.Index == 4)
            {
                sol = cap.Value;
            }
        }

        day = day.PadLeft(2, '0');

        var className = $"solutions.Day{day}";
        var funcName = $"Solution{sol}";
        Type type = Type.GetType(className) ?? throw new Exception($"Invalid day: {className}");
        var func = type.GetMethod(funcName) ?? throw new Exception($"Invalid solution: {funcName}");
        var obj = Activator.CreateInstance(type);

        var filename = $"input/{day}.txt";
        if (args.Length > 0 && args[0] == "-e")
        {
            filename = $"example/{day}.txt";
        }

        var inputStr = File.ReadAllText(filename);

        func.Invoke(obj, [inputStr]);
    }
}
