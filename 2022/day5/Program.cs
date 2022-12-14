// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
bool part1 = false;
string[] lines = File.ReadAllLines("input.txt");

List<Stack<char>> stacks = new List<Stack<char>>();
List<Stack<char>> reversedStacks = new List<Stack<char>>();
bool readingStacks = true;
foreach(string line in lines)
{
    if (readingStacks)
    {
        if (line.Length >= 1 && line[1] == '1')
        {
            readingStacks = false;
            foreach (Stack<char> stack in stacks)
            {
                reversedStacks.Add(new Stack<char>(stack));
            }
        }
        else
        {
            var crates = line.Chunk(4).Select(c => new string(c));
            if (stacks.Count == 0)
            {
                for( int i = 0; i < crates.Count(); i++)
                {
                    stacks.Add(new Stack<char>());
                }
            }
            int stackNumber = 0;
            foreach (string crate in crates)
            {
                if (!string.IsNullOrWhiteSpace(crate))
                {
                    char crateContents = crate[1];
                    stacks[stackNumber].Push(crateContents);
                }
                stackNumber++;
                
            }
        }
    }
    else
    {
        if (!string.IsNullOrWhiteSpace(line))
        {
            Regex rx = new Regex(@"move (\d*) from (\d*) to (\d*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Match match = rx.Match(line);
            int numToMove = Convert.ToInt32(match.Groups[1].Value);
            int sourceStack = Convert.ToInt32(match.Groups[2].Value) - 1;
            int destStack = Convert.ToInt32(match.Groups[3].Value) - 1;

            Stack<char> tmpStack = new Stack<char>();
            for (int i = 0; i < numToMove; i++)
            {
                char poppedCrate = reversedStacks[sourceStack].Pop();
                if (part1)
                {
                    reversedStacks[destStack].Push(poppedCrate);
                }
                else
                {
                    tmpStack.Push(poppedCrate);
                }

            }

            if (!part1)
            {
                while(tmpStack.Count > 0)
                {
                    char tmpCrate = tmpStack.Pop();
                    reversedStacks[destStack].Push(tmpCrate);
                }
            }

        }
    }
}

string topCrates = new string(reversedStacks.Select(s => s.Peek()).ToArray());
Console.WriteLine(topCrates);
Console.WriteLine("done");

