// See https://aka.ms/new-console-template for more information
string[] lines = File.ReadAllLines("input.txt");

var monkeyInfoList = lines.Chunk(7);
List<Monkey> monkeys = new List<Monkey>();
foreach (var monkeyInfo in monkeyInfoList)
{
    Monkey currentMonkey = new Monkey();
    // add the items
    var tokensLine2 = monkeyInfo[1].Split(":");
    if (tokensLine2.Length >= 2)
    {
        var items = tokensLine2[1].Split(",").Select(w => Convert.ToInt32(w.Trim()));
        foreach (int item in items)
        {
            currentMonkey.Items.Enqueue(item);
        }
    }

    // define monkey's inspect operation
    var tokensLine3 = monkeyInfo[2].Trim().Split(" ");
    string opString = tokensLine3[4];
    string valString = tokensLine3[5];
    if (opString == "*" && valString == "old")
    {
        currentMonkey.InspectOp = 's';
    }
    else
    {
        currentMonkey.InspectOp = opString[0];
        currentMonkey.InspectVal = Convert.ToInt32(valString);
    }

    // define monkey's throw behaviour
    currentMonkey.Divisor = Convert.ToInt32(monkeyInfo[3].Trim().Split(" ").Last());
    currentMonkey.TargetIfTrue = Convert.ToInt32(monkeyInfo[4].Trim().Split(" ").Last());
    currentMonkey.TargetIfFalse = Convert.ToInt32(monkeyInfo[5].Trim().Split(" ").Last());

    // done, add the monkey to the list of monkeys
    monkeys.Add(currentMonkey);
}

// run the simulation
const int NUM_ROUNDS = 20;
for (int i = 0; i < NUM_ROUNDS; i++)
{
    foreach (Monkey monkey in monkeys)
    {
        while (monkey.Items.Count> 0)
        {
            int x = monkey.Items.Dequeue();
            int worryLevelPostInspect = monkey.Inspect(x) / 3;
            if (worryLevelPostInspect % monkey.Divisor == 0)
            {
                monkeys[monkey.TargetIfTrue].Items.Enqueue(worryLevelPostInspect);
            }
            else
            {
                monkeys[monkey.TargetIfFalse].Items.Enqueue(worryLevelPostInspect);
            }
        }
    }
    Console.WriteLine($"After round {i+1} the monkeys are holding items with these worry levels:");
    for (int m = 0; m < monkeys.Count; m++)
    {
        Monkey monkey = monkeys[m];
        Console.WriteLine($"Monkey {m}: {string.Join(", ", monkey.Items)}");
    }

}

int monkeyBusiness = monkeys
    .Select(m => m.NumberOfInspections)
    .OrderDescending()
    .Take(2)
    .Aggregate(1, (prod, next) => prod * next);
Console.WriteLine($"Level of monkey business is {monkeyBusiness}");
Console.WriteLine("done");

public class Monkey 
{
    public int NumberOfInspections {get; private set;} = 0;
    public Queue<int> Items {get; set;} = new Queue<int> ();
    public int Divisor {get; set;} = 1;
    public int TargetIfTrue {get; set;}
    public int TargetIfFalse {get; set;}
    public char InspectOp {get; set;}
    public int InspectVal {get; set;}
    public int Inspect (int x)
    {
        NumberOfInspections++;
        switch (InspectOp)
        {
            case '*':
                return x * InspectVal;
            case '+':
                return x + InspectVal;
            case 's':
                return x * x;
            default:
                return x;
        }
    }

}
