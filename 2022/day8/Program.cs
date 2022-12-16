// See https://aka.ms/new-console-template for more information
int[][] treeGrid = File.ReadAllLines("input.txt")
    .Select(line => line
        .ToCharArray()
        .Select(c => Convert.ToInt32(char.GetNumericValue(c)))
        .ToArray())
    .ToArray();

int numVisibleTrees = 0;
int gridWidth = treeGrid[0].Length;
int gridHeight = treeGrid.Length;
int maxScenicScore = -1;
int currentScenicScore = -1;

for (int i = 0; i < gridHeight; i++)
{
    for (int j = 0; j < gridWidth; j++)
    {
        numVisibleTrees += Convert.ToInt32(IsTreeVisible(i, j));
        currentScenicScore = GetScenicScore(i, j);
        if (currentScenicScore > maxScenicScore)
        {
            maxScenicScore = currentScenicScore;
        }
        
    }
}

bool IsTreeVisible(int row, int col)
{
    if (row == 0 || row == gridHeight - 1 || col == 0 || col == gridWidth - 1)
    {
        return true;
    }

    int treeHeight = treeGrid[row][col];
    
    // left
    bool visibleLeft = true;
    for (int j = 0; j < col; j++)
    {
        if (treeGrid[row][j] >= treeHeight)
        {
            visibleLeft = false;
            break;
        }
    }
    if (visibleLeft)
    {
        return true;
    }

    // right
    bool visibleRight = true;
    for (int j = gridWidth - 1; j > col; j--)
    {
        if (treeGrid[row][j] >= treeHeight)
        {
            visibleRight = false;
            break;
        }
    }
    if (visibleRight)
    {
        return true;
    }

    // top
    bool visibleTop = true;
    for (int i = 0; i < row; i++)
    {
        if (treeGrid[i][col] >= treeHeight)
        {
            visibleTop = false;
            break;
        }
    }
    if (visibleTop)
    {
        return true;
    }

    // bottom
    bool visibleBottom = true;
    for (int i = gridHeight - 1; i > row; i--)
    {
        if (treeGrid[i][col] >= treeHeight)
        {
            visibleBottom = false;
            break;
        }
    }
    if (visibleBottom)
    {
        return true;
    }

    return false;
}

int GetScenicScore(int row, int col)
{
    if (row == 0 || row == gridHeight - 1 || col == 0 || col == gridWidth - 1)
    {
        return 0;
    }
    int treeHeight = treeGrid[row][col];
    int scoreLeft = 0;
    int scoreRight = 0;
    int scoreTop = 0;
    int scoreBottom = 0;
    int counter;
    // look left
    counter = col - 1;
    while (counter >= 0)
    {
        scoreLeft++;
        if (treeGrid[row][counter] >= treeHeight)
        {
            break;
        }
        counter--;
    }

    // look right
    counter = col + 1;
    while (counter < gridWidth)
    {
        scoreRight++;
        if (treeGrid[row][counter] >= treeHeight)
        {
            break;
        }
        counter++;
    }

    // look up
    counter = row - 1;
    while (counter >= 0)
    {
        scoreTop++;
        if (treeGrid[counter][col] >= treeHeight)
        {
            break;
        }
        counter--;
    }

    // look down
    counter = row + 1;
    while (counter < gridHeight)
    {
        scoreBottom++;
        if (treeGrid[counter][col] >= treeHeight)
        {
            break;
        }
        counter++;
    }
    return scoreLeft * scoreRight * scoreTop * scoreBottom;

}
Console.WriteLine(numVisibleTrees);
Console.WriteLine(maxScenicScore);
Console.WriteLine("done");
