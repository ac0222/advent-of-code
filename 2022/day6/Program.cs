// See https://aka.ms/new-console-template for more information
string[] lines = File.ReadAllLines("input.txt");
List<int> packetMarkerPositions = new List<int>();
List<int> messageMarkerPositions = new List<int>();

int NUM_DISTINCT_PACKET = 4;
int NUM_DISTINCT_MESSAGE = 14;

foreach(string line in lines) 
{
    packetMarkerPositions.Add(GetFirstMarkerPosition(line, NUM_DISTINCT_PACKET));
    messageMarkerPositions.Add(GetFirstMarkerPosition(line, NUM_DISTINCT_MESSAGE));

}


int GetFirstMarkerPosition(string message, int numDistinct)
{
    for (int i = 0; i < (message.Length - numDistinct); i++)
    {
        string section = message.Substring(i, numDistinct);
        int numUnique = section.ToCharArray().Distinct().Count();
        if (numUnique == numDistinct)
        {
            return i + numDistinct;
        }
    }
    return -1;
}

for (int i = 0; i < lines.Length; i++)
{
    Console.WriteLine($"packet position: {packetMarkerPositions[i]} || message position {messageMarkerPositions[i]}");
}
Console.WriteLine("done");