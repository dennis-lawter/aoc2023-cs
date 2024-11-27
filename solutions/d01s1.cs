namespace solutions
{
    public class Day01
    {
        public void Solution1(String input)
        {
            var output = 0;
            input = input.ReplaceLineEndings();
            var lines = input.Split(Environment.NewLine);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
                var leftDigit = 0;
                var rightDigit = 0;
                var chars = line.ToCharArray();
                foreach (Char c in chars)
                {
                    try
                    {
                        leftDigit = Int16.Parse(c.ToString());
                        Console.WriteLine(c);
                        break;
                    }
                    catch (Exception) { }
                }
                foreach (Char c in chars.Reverse())
                {

                    try
                    {
                        rightDigit = Int16.Parse(c.ToString());
                        Console.WriteLine(c);
                        break;
                    }
                    catch (Exception) { }
                }
                output += leftDigit * 10 + rightDigit;
            }
            Console.WriteLine("================================");
            Console.WriteLine(output);
            Console.WriteLine("================================");
        }



        private static string ReverseString(String input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public void Solution2(String input)
        {
            Dictionary<string, int> map = new();
            map.Add("1", 1);
            map.Add("2", 2);
            map.Add("3", 3);
            map.Add("4", 4);
            map.Add("5", 5);
            map.Add("6", 6);
            map.Add("7", 7);
            map.Add("8", 8);
            map.Add("9", 9);
            map.Add("one", 1);
            map.Add("two", 2);
            map.Add("three", 3);
            map.Add("four", 4);
            map.Add("five", 5);
            map.Add("six", 6);
            map.Add("seven", 7);
            map.Add("eight", 8);
            map.Add("nine", 9);

            var output = 0;
            input = input.ReplaceLineEndings();
            string[] lines = input.Split(Environment.NewLine) ?? throw new Exception("Failed to split");

            foreach (var line in lines)
            {
                Console.WriteLine(line);

                var revLine = Day01.ReverseString(line);
                Console.WriteLine(revLine);
                var leftDigit = 0;
                var rightDigit = 0;

                for (int i = 0; i < line.Length; i++)
                {
                    try
                    {
                        var subline = line.Substring(i);
                        var subRevline = revLine.Substring(i);

                        foreach (var key in map.Keys)
                        {
                            if (leftDigit == 0 && subline.StartsWith(key))
                            {
                                leftDigit = map[key];
                            }
                            if (rightDigit == 0 && subRevline.StartsWith(Day01.ReverseString(key)))
                            {
                                rightDigit = map[key];
                            }
                            if (leftDigit != 0 && rightDigit != 0)
                            {
                                break;
                            }
                        }
                    }
                    catch (System.ArgumentOutOfRangeException e)
                    {
                        throw new System.ArgumentOutOfRangeException($"I can't substring with {i}!", e);
                    }
                }
                output += leftDigit * 10 + rightDigit;
            }
            Console.WriteLine("================================");
            Console.WriteLine(output);
            Console.WriteLine("================================");
        }
    }
}
