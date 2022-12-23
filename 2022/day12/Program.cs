// See https://aka.ms/new-console-template for more information
List<List<char>> elevationMap = File.ReadAllLines("input_test.txt")
    .Select(line => line.ToCharArray().ToList())
    .ToList();

int mapHeight = elevationMap.Count();
int mapWidth = elevationMap[0].Count();

Queue<Waypoint> bfsQueue = new Queue<Waypoint>();
for (int i = 0; i < mapHeight; i++)
{
    for (int j = 0; j < mapWidth; j++)
    {
        if (elevationMap[i][j] == 'S')
        {
            bfsQueue.Enqueue(
                new Waypoint {
                    X = i, Y = j, Elevation = 'a'
                }
            );
        }
    }
}


Console.WriteLine("done");


public class Waypoint 
{
    public List<Waypoint> Path {get; set;} = new List<Waypoint>();
    public int X {get; set;}
    public int Y {get; set;}
    public char Elevation {get; set;}
}
