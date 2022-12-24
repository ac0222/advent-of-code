using System.Text.RegularExpressions;

// See https://aka.ms/new-console-template for more information
string[] lines = File.ReadAllLines("input.txt");
List<SensorBeaconPair> sensorBeaconPairs = new List<SensorBeaconPair>();
List<(int, int)> intersects = new List<(int, int)>();
HashSet<int> noBeacons = new HashSet<int>();
int y0 = 2000000;
foreach (string line in lines)
{
    Regex rx = new Regex(@"Sensor at x=(-?\d*), y=(-?\d*): closest beacon is at x=(-?\d*), y=(-?\d*)", 
        RegexOptions.Compiled | RegexOptions.IgnoreCase);
    Match match = rx.Match(line);
    int sensorX = Convert.ToInt32(match.Groups[1].Value);
    int sensorY = Convert.ToInt32(match.Groups[2].Value);
    int beaconX = Convert.ToInt32(match.Groups[3].Value);
    int beaconY = Convert.ToInt32(match.Groups[4].Value);
    var pair = new SensorBeaconPair {
        SensorX = sensorX, 
        SensorY = sensorY, 
        BeaconX = beaconX, 
        BeaconY = beaconY
    };
    sensorBeaconPairs.Add(pair);
    if (pair.ManhattanDistance >= Math.Abs(pair.SensorY - y0))
    {
        intersects.Add(ComputeIntersect(pair, y0));
    }
}

foreach ((int, int) interval  in intersects)
{
    (int low, int high) = interval;
    for (int i = low; i <= high; i++)
    {
        if (!sensorBeaconPairs.Any(p => p.BeaconX == i && p.BeaconY == y0))
        {
            noBeacons.Add(i);
        }
    }
}

Console.WriteLine(noBeacons.Count);

Console.WriteLine("done");

(int a, int b) ComputeIntersect(SensorBeaconPair pair, int y)
{
    int overflow = pair.ManhattanDistance - Convert.ToInt32(Math.Abs(y - pair.SensorY));
    int a = pair.SensorX - overflow;
    int b = pair.SensorX + overflow;
    return (a, b);
}
public struct SensorBeaconPair
{
    public int SensorX {get; set;}
    public int SensorY {get; set;}
    public int BeaconX {get; set;}
    public int BeaconY {get; set;}
    public int ManhattanDistance {
        get {
            return Convert.ToInt32((Math.Abs(SensorX - BeaconX) + Math.Abs(SensorY - BeaconY)));
        }
    }
}



