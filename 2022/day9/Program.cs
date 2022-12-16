// See https://aka.ms/new-console-template for more information
string[] lines = File.ReadAllLines("input.txt");

Tuple<int, int> headPosition = Tuple.Create<int, int>(0, 0);
Tuple<int, int> tailPosition = Tuple.Create<int, int>(0, 0);
Dictionary<char, Tuple<int, int>> unitVectors = new Dictionary<char, Tuple<int, int>>
{
    {'R', Tuple.Create<int, int>(1, 0)},
    {'L', Tuple.Create<int, int>(-1, 0)},
    {'U', Tuple.Create<int, int>(0, 1)},
    {'D', Tuple.Create<int, int>(0, -1)}
};

HashSet<Tuple<int, int>> tailVisited = new HashSet<Tuple<int, int>> { Tuple.Create<int, int>(0, 0) };

foreach(string line in lines)
{
    char direction = line.Split(" ")[0][0];
    int distance = Convert.ToInt32(line.Split(" ")[1]);
    for (int i = 0; i < distance; i++)
    {
        Tuple<int, int> u = unitVectors[direction];
        headPosition = Tuple.Create<int, int>(headPosition.Item1 + u.Item1, headPosition.Item2 + u.Item2);
        AdjustTail();
    }
}

void AdjustTail()
{
    Tuple<int, int> delta = Tuple.Create<int, int> (headPosition.Item1 - tailPosition.Item1, 
        headPosition.Item2 - tailPosition.Item2);
    if (Math.Abs(delta.Item1) > 1 || Math.Abs(delta.Item2) > 1)
    {
        int deltaX = delta.Item1 == 0 ? 0 : (int)(delta.Item1/Math.Abs(delta.Item1));
        int deltaY = delta.Item2 == 0 ? 0 : (int)(delta.Item2/Math.Abs(delta.Item2));

        Tuple<int, int> newTailPosition = Tuple.Create<int, int> (
            tailPosition.Item1 + deltaX,
            tailPosition.Item2 + deltaY
        );
        if (!tailVisited.Contains(newTailPosition))
        {
            tailVisited.Add(newTailPosition);
        }
        tailPosition = newTailPosition;
    }
}

Console.WriteLine(tailVisited.Count());
Console.WriteLine("done");


