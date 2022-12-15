// See https://aka.ms/new-console-template for more information
string[] lines = File.ReadAllLines("input.txt");
int totalScore = 0;
int totalScorePart2 = 0;


Dictionary<char, Dictionary<char, int>> resultMatrix = 
    new Dictionary<char, Dictionary<char, int>> 
    {
        {
            'A', 
            new Dictionary<char, int> 
            {
                {'X', 3}, {'Y', 6}, {'Z', 0}
            }
        },
        {
            'B', 
            new Dictionary<char, int> 
            {
                {'X', 0}, {'Y', 3}, {'Z', 6}
            }
        },
        {
            'C', 
            new Dictionary<char, int> 
            {
                {'X', 6}, {'Y', 0}, {'Z', 3}
            }
        }
    };


Dictionary<char, Dictionary<char, int>> resultMatrixPart2 = 
    new Dictionary<char, Dictionary<char, int>> 
    {
        {
            'A', 
            new Dictionary<char, int> 
            {
                {'X', 3}, {'Y', 4}, {'Z', 8}
            }
        },
        {
            'B', 
            new Dictionary<char, int> 
            {
                {'X', 1}, {'Y', 5}, {'Z', 9}
            }
        },
        {
            'C', 
            new Dictionary<char, int> 
            {
                {'X', 2}, {'Y', 6}, {'Z', 7}
            }
        }
    };

int ComputeResult(char p1, char p2) 
{
    int score = 0;
    if (p2 == 'X') 
    {
        score += 1;
    }
    else if (p2 == 'Y')
    {
        score += 2;
    }
    else if (p2 == 'Z')
    {
        score += 3;
    }

    score += resultMatrix[p1][p2];
    return score;
}

int ComputeResultPart2(char p1, char p2) 
{
    return resultMatrixPart2[p1][p2];
}

foreach(string line in lines) 
{
    char p1 = Convert.ToChar(line.Split(" ")[0]);
    char p2 = Convert.ToChar(line.Split(" ")[1]);
    totalScore += ComputeResult(p1, p2);
    totalScorePart2 += ComputeResultPart2(p1, p2);
}

Console.WriteLine(totalScore);
Console.WriteLine(totalScorePart2);
Console.WriteLine("done");

