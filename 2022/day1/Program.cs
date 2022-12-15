// See https://aka.ms/new-console-template for more information
string[] lines = File.ReadAllLines("input.txt");
List<int> caloriesPerElf = new List<int>();
int currentCalories = 0;

foreach (string line in lines) 
{
    if (string.IsNullOrWhiteSpace(line)) 
    {
        caloriesPerElf.Add(currentCalories);
        currentCalories = 0;
    }
    else
    {
        currentCalories += Convert.ToInt32(line.Trim());
    }
}

if (currentCalories != 0) 
{
    caloriesPerElf.Add(currentCalories);
}

int maxCalories = caloriesPerElf.Max();
int highestCalorieElf = caloriesPerElf.IndexOf(caloriesPerElf.Max());

int sumOfTopThree = caloriesPerElf.Order().Reverse().Take(3).Sum();

Console.WriteLine($"Elf carrying the most calories is carrying: {maxCalories} Calories!");
Console.WriteLine($"Sum of top 3 is: {sumOfTopThree} Calories!");
