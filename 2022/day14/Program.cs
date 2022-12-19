// See https://aka.ms/new-console-template for more information
string[] lines = File.ReadAllLines("input_test.txt");
HashSet<(int x, int y)> rocks = new HashSet<(int x, int y)>();

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
            for (int j = source.x; j <= dest.x - 1; j++)
            {
                rocks.Add((j, source.y));
            }
        }   
        else
        {
            for (int k = source.y; k <= dest.y - 1; k++)
            {
                rocks.Add((source.x, k));
            }
        }
        rocks.Add(waypoints[i+1]);
    }
}

Console.WriteLine("done");
