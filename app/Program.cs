internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("\nAdvent of Code 2022!\n");
        // Day1();
        // Day2();
        Day3();
        Console.WriteLine("\nready");
        Console.Read();
    }
 
    static void Day1()
    {
        Console.WriteLine("\nDay 1:");

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
        Console.WriteLine("Part 1: "+elves.Max()+" calories"); //part 1
        Console.WriteLine("Part 2: "+elves.TakeLast(3).Sum()+" calories"); //part 2
    }

    static void Day2()
    {
        Console.WriteLine("\nDay 2: ");

        var lines = File.ReadLines("input/day2.txt");

        int score = 0;
        int scorePart2 = 0;

        foreach (var line in lines)
        {
            var opponent = line.Substring(0, 1);
            var me = line.Substring(2, 1);
            var part2 = "";

            if(me == "X")
            {
                score++;
                me = "rock";  
                part2 = "lose";              
            }
            else if (me == "Y")
            {
                score += 2;
                me = "paper";
                part2 = "draw";
            }
            else if (me == "Z")
            {
                score += 3;
                me = "scissors";
                part2 = "win";
            }

            if(opponent == "A")
            {
                opponent = "rock";
                scorePart2 += PartTwo(opponent, part2);
            }
            else if (opponent == "B")
            {
                opponent = "paper";
                scorePart2 += PartTwo(opponent, part2);
            }
            else if (opponent == "C")
            {
                opponent = "scissors";
                scorePart2 += PartTwo(opponent, part2);
            }

            // add 0 to score for a loss, 3 for a draw, 6 for a win

            if (me == "rock" && opponent == "scissors")
            {
                score += 6;
            }
            else if (me == "rock" && opponent == "paper")
            {
                score += 0;
            }
            else if (me == "rock" && opponent == "rock")
            {
                score += 3;
            }
            else if (me == "paper" && opponent == "scissors")
            {
                score += 0;
            }
            else if (me == "paper" && opponent == "paper")
            {
                score += 3;
            }
            else if (me == "paper" && opponent == "rock")
            {
                score += 6;
            }
            else if (me == "scissors" && opponent == "scissors")
            {
                score += 3;
            }
            else if (me == "scissors" && opponent == "paper")
            {
                score += 6;
            }
            else if (me == "scissors" && opponent == "rock")
            {
                score += 0;
            }
        }

        Console.WriteLine("Part 1 score: "+score);
        Console.WriteLine("Part 2 score: "+scorePart2);
    }

    private static int PartTwo(string opponent, string part2)
    {
        int s = 0;
        switch (opponent)
        {
            case "rock":
                if (part2 == "win")
                    s = 6+2;
                else if (part2 == "draw")
                    s = 3+1;
                else if (part2 == "lose")
                    s = 0+3;
                break;
            case "paper":
                if (part2 == "win")
                    s = 6+3;
                else if (part2 == "draw")
                    s = 3+2;
                else if (part2 == "lose")
                    s = 0+1;
                break;
            case "scissors":
                if (part2 == "win")
                    s = 6+1;
                else if (part2 == "draw")
                    s = 3+3;
                else if (part2 == "lose")
                    s = 0+2;
                break;
        }

        return s;

    }

    static void Day3()
    {
        string[] input = File.ReadAllLines("input/day3.txt");

        string sack1 = "";
        string sack2 = "";

        int total = 0;
        int part2total = 0;

        // create a dictionary of lowercase a thru z, with secondary value of 1 thru 26
        Dictionary<char, int> lowercases = new();
        int i = 1;
        for (char c = 'a'; c <= 'z'; c++)
        {
            lowercases.Add(c, i);
            i++;
        }

        // create a dictionary of uppercase A thru Z, with secondary value of 27 thru 52
        Dictionary<char, int> uppercases = new();
        i = 27;
        for (char c = 'A'; c <= 'Z'; c++)
        {
            uppercases.Add(c, i);
            i++;
        }

        // for each line of input, split the string in half, assigning the first half to sack1 and the second half to sack2
        foreach (var line in input)
        {
            var half = line.Length / 2;
            sack1 = line.Substring(0, half);
            sack2 = line.Substring(half, half);

            // find the common characters between sack1 and sack2
            var common = sack1.Intersect(sack2);

            // for each common character, find the value in the dictionary and add it to the total
            foreach (var c in common)
            {
                if (lowercases.ContainsKey(c))
                    total += lowercases[c];
                else if (uppercases.ContainsKey(c))
                    total += uppercases[c];
            }
        }

        Console.WriteLine("Part 1: "+total);

        // part 2
        // get groups of 3 lines at a time using modulo

        for (int j = 0; j < input.Length; j++)
        {
            if (j % 3 == 0)
            {
                var line1 = input[j];
                var line2 = input[j + 1];
                var line3 = input[j + 2];

                // find the common character shared by all 3 lines
                var common = line1.Intersect(line2).Intersect(line3);

                // for each common character, find the value in the dictionary and add it to the total
                foreach (var c in common)
                {
                    if (lowercases.ContainsKey(c))
                        part2total += lowercases[c];
                    else if (uppercases.ContainsKey(c))
                        part2total += uppercases[c];
                }
            }
        }

        Console.WriteLine("Part 2: "+part2total);
    }
}
