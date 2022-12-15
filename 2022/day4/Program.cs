// See https://aka.ms/new-console-template for more information
string[] lines = File.ReadAllLines("input.txt");

int fullOverlap = 0;
int partialOverlap = 0;

foreach (string line in lines) 
{

    string[] tokens = line.Split(new char[] {',', '-'});
    int elf1Left = Convert.ToInt32(tokens[0]);
    int elf1Right = Convert.ToInt32(tokens[1]);
    int elf2Left = Convert.ToInt32(tokens[2]);
    int elf2Right = Convert.ToInt32(tokens[3]);

    if (elf1Left <= elf2Left && elf1Right >= elf2Right) 
    {
        fullOverlap++;   
    }
    else if (elf2Left <= elf1Left && elf2Right >= elf1Right) 
    {
        fullOverlap++;
    }

    if (elf1Left <= elf2Left) 
    {
        if (elf1Right >= elf2Left)
        {
            partialOverlap++;
        }
    }
    else
    {
        if (elf2Right >= elf1Left)
        {
            partialOverlap++;
        }
    }
}

Console.WriteLine(fullOverlap);
Console.WriteLine(partialOverlap);
