// See https://aka.ms/new-console-template for more information
string[] lines = File.ReadAllLines("input.txt");
HashSet<(int x, int y)> rocks = new HashSet<(int x, int y)>();

// locate rocks
foreach (string line in lines)
{
    List<(int x, int y)> waypoints = line.Split(" -> ")
        .Select(xy => (Convert.ToInt32(xy.Split(",")[0]), Convert.ToInt32(xy.Split(",")[1])))
        .ToList();
    for (int i = 0; i < waypoints.Count - 1; i++)
    {
        var source = waypoints[i];
        var dest = waypoints[i+1];
        if (source.x != dest.x)
        {
            if (source.x < dest.x)
            {
                for (int j = source.x; j <= dest.x - 1; j++)
                {
                    rocks.Add((j, source.y));
                }
            }
            else
            {
                for (int j = source.x; j >= dest.x + 1; j--)
                {
                    rocks.Add((j, source.y));
                }

            }
        }   
        else
        {
            if (source.y < dest.y)
            {
                for (int k = source.y; k <= dest.y - 1; k++)
                {
                    rocks.Add((source.x, k));
                }
            }
            else
            {
                for (int k = source.y; k >= dest.y + 1; k--)
                {
                    rocks.Add((source.x, k));
                }

            }
        }
        rocks.Add(waypoints[i+1]);
    }
}

HashSet<(int x, int y)> restingSand = new HashSet<(int x, int y)>();
(int x, int y) lowestRock = rocks.MaxBy(r => r.y);
bool full = false;
while (!full)
{
    (int x, int y) currentSandLocation = (500, 0);
    while (true)
    {
        (int x, int y) newSandLocation = TryToMoveSand(currentSandLocation);
        if (currentSandLocation == newSandLocation)
        {
            restingSand.Add(newSandLocation);
            if (newSandLocation == (500, 0))
            {
                full = true;
            }
            break;
        }
        else if (newSandLocation.y == lowestRock.y + 1)
        {
            restingSand.Add(newSandLocation);
            break;
        }
        currentSandLocation = newSandLocation;
    }
}

(int x, int y) leftBound = new HashSet<(int x, int y)>(rocks.Union(restingSand)).MinBy(r => r.x);
(int x, int y) rightBound = new HashSet<(int x, int y)>(rocks.Union(restingSand)).MaxBy(r => r.x);

List<string> image = new List<string>();
for (int i = 0; i <= lowestRock.y + 3; i++)
{
    string currentLine = $"{i}".PadRight(5);
    for (int j = leftBound.x - 3; j <= rightBound.x + 3; j++)
    {
        if (rocks.Contains((j, i)) || i == lowestRock.y + 2) 
        {
            currentLine += "#";
        }
        else if (restingSand.Contains((j, i)))
        {
            currentLine += "o";
        }
        else
        {
            currentLine += ".";
        }
    }
    image.Add(currentLine);
}

foreach (string imageLine in image)
{
    Console.WriteLine(imageLine);
}
File.WriteAllLines("output.txt",  image);


Console.WriteLine($"Resting units of sand: {restingSand.Count}");
Console.WriteLine("done");

(int x, int y) TryToMoveSand((int x, int y) sandLocation)
{
    List<(int x, int y)> possibleLocations = new List<(int x, int y)> {
        (sandLocation.x, sandLocation.y + 1), (sandLocation.x-1, sandLocation.y + 1), (sandLocation.x+1, sandLocation.y + 1)
    };
    foreach ((int x, int y) pl in possibleLocations)
    {
        if (!rocks.Contains(pl) && !restingSand.Contains(pl))
        {
            return pl;
        }
    }
    return sandLocation;
}
