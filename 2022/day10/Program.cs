// See https://aka.ms/new-console-template for more information
string[] lines = File.ReadAllLines("input.txt");
List<int> cycleValues = new List<int>();
int x = 1;
foreach (string line in lines)
{
    string op = line.Split(" ")[0];
    int v = 0;
    if (op != "noop")
    {
        v = Convert.ToInt32(line.Split(" ")[1]);
        cycleValues.Add(x);
        cycleValues.Add(x);
        x = x + v;
    }
    else
    {
        cycleValues.Add(x);
    }
}

// part 1
const int PERIOD = 40;
const int NUM_PERIODS = 6;
const int OFFSET = 20;
List<int> signalStrengths = new List<int>();
for (int cycle = OFFSET; cycle < OFFSET + NUM_PERIODS*PERIOD; cycle += PERIOD)
{
    signalStrengths.Add(GetSignalStrength(cycle));
}

Console.WriteLine(signalStrengths.Sum());

int GetSignalStrength(int cycle)
{
    int cycleValue = cycle <= cycleValues.Count ? cycleValues[cycle-1] : cycleValues.Last();
    return cycle * cycleValue;
}


// part 2
char[][] crtScreen = new char[6][];
for (int row = 0; row < 6; row++)
{
    crtScreen[row] = new char[40];
}

for (int i = 0; i < cycleValues.Count; i++)
{
    int pixelRow = i / 40;
    int pixelCol = i % 40;
    int spriteLeft = cycleValues[i] - 1;
    int spriteRight = cycleValues[i] + 1;
    if (pixelCol >= spriteLeft && pixelCol <= spriteRight)
    {
        crtScreen[pixelRow][pixelCol] = '#';
    }
    else
    {
        crtScreen[pixelRow][pixelCol] = '.';
    }
}

for (int i = 0; i < 6; i++)
{
    Console.WriteLine(new string(crtScreen[i]));
}

Console.WriteLine("done");

