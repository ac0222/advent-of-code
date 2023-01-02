using System.Numerics;
using System.Text.RegularExpressions;

// See https://aka.ms/new-console-template for more information
string[] lines = File.ReadAllLines("input.txt");
List<SensorBeaconPair> sensorBeaconPairs = new List<SensorBeaconPair>();
List<List<(int, int)>> intersects = new List<List<(int, int)>>();
List<List<(int, int)>> simplifiedIntervals = new List<List<(int, int)>>();
HashSet<int> noBeacons = new HashSet<int>();
//int y0 = 2000000;
int yMin = 0;
int yMax = 4000000;
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
    for (int y0 = yMin; y0 <= yMax; y0++)
    {
        if (intersects.Count < y0+1) 
        {
            intersects.Add(new List<(int, int)>());
        }
        if (pair.ManhattanDistance >= Math.Abs(pair.SensorY - y0))
        {
            intersects[y0].Add(ComputeIntersect(pair, y0));
        }
    }
}

foreach (List<(int, int)> intervals in intersects)
{
    simplifiedIntervals.Add(SimplifyIntervals(intervals));
}

// foreach ((int, int) interval  in intersects)
// {
//     (int low, int high) = interval;
//     for (int i = low; i <= high; i++)
//     {
//         if (!sensorBeaconPairs.Any(p => p.BeaconX == i && p.BeaconY == y0))
//         {
//             noBeacons.Add(i);
//         }
//     }
// }


var yPos = simplifiedIntervals.FindIndex(si => si.Count > 1);
BigInteger xPos = simplifiedIntervals[yPos][0].Item2 + 1;
BigInteger tuningFrequency = xPos * 4000000 + yPos;

Console.WriteLine($"Distress beacon is at x = {xPos}, y = {yPos}");
Console.WriteLine($"Tuning Frequency is {tuningFrequency}");
Console.WriteLine(noBeacons.Count);

Console.WriteLine("done");

List<(int, int)> SimplifyIntervals(List<(int, int)> intervals)
{
    List<(int, int)> simplified = new List<(int, int)>();
    var sortedIntervals = intervals.OrderBy(iv => iv.Item1).ToList();
    for (int i = 0; i < sortedIntervals.Count; i++)
    {
        var currentInterval = sortedIntervals[i];
        while (i+1 < sortedIntervals.Count)
        {
            var nextInterval = sortedIntervals[i+1];
            if (nextInterval.Item1 <= currentInterval.Item2 + 1)
            {
                if (nextInterval.Item2 >= currentInterval.Item2)
                {
                    currentInterval = (currentInterval.Item1, nextInterval.Item2);
                }
            }
            else
            {
                break;
            }
            i++;
        }
        simplified.Add(currentInterval);
    }
    return simplified;
}

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



