internal class Program
{
    static void Main(string[] args)
    {
        Day1();
        Console.WriteLine("\nready");
        Console.Read();
    }
 
    static void Day1()
    {
        List<int> elves = new();
        int i = 0;
 
        foreach (var line in File.ReadLines("input_1.txt"))
        {
            if (int.TryParse(line, out var n))
                i += n;
            else
            {
                elves.Add(i);
                i = 0;
            }
        }
 
        elves.Sort();
        Console.WriteLine(elves.Max()); //part 1
        Console.WriteLine(elves.TakeLast(3).Sum()); //part 2
    }
}