// See https://aka.ms/new-console-template for more information
string[] lines = File.ReadAllLines("input.txt");
List<int> priorities  = new List<int>();
foreach(string line in lines) {
    
    char[] compartment1 = line.Substring(0, line.Length/2).ToCharArray();
    char[] compartment2 = line.Substring(line.Length/2).ToCharArray();
    HashSet<char> compartment2Hashset = new HashSet<char>(compartment2);
    foreach (char c in compartment1) 
    {
        if (compartment2Hashset.Contains(c)) 
        {
            priorities.Add(GetPriority(c));
            break;
        }
    }
}

int GetPriority(char c) 
{
    if (char.IsLower(c)) 
    {
        return ((int)c) - 96;
    }
    else
    {
        return ((int)c) - 64 + 26;
    }
}

Console.WriteLine(priorities.Sum());

// begin part 2
List<int> badgePriorities = new List<int>();
List<ElfGroup> elfGroups = new List<ElfGroup>();

var batchedLines = lines.Chunk(3);
foreach (var triplet in batchedLines) {
    var elfGroup = new ElfGroup 
    {
        RucksackContents1 = triplet[0].ToCharArray(),
        RucksackContents2 = triplet[1].ToCharArray(),
        RucksackContents3 = triplet[2].ToCharArray(),
    };
    badgePriorities.Add(GetPriority(elfGroup.Badge));
}

Console.WriteLine(badgePriorities.Sum());


class ElfGroup 
{
    private char _badge = ' ';
    public char Badge 
    {
        get 
        {
            if (_badge == ' ') 
            {
                _badge = ComputeBadge();
            }
            return _badge;
        }
    }

    public char[] RucksackContents1 {get; set;}
    public char[] RucksackContents2 {get; set;}
    public char[] RucksackContents3 {get; set;}

    private char ComputeBadge() 
    {
        HashSet<char> rucksackHashset2 = new HashSet<char>(RucksackContents2);
        HashSet<char> rucksackHashset3 = new HashSet<char>(RucksackContents3);
        foreach (char c in RucksackContents1) 
        {
            if (rucksackHashset2.Contains(c) && rucksackHashset3.Contains(c)) 
            {
                return c;
            }
        }
        return '!';
    }
}

