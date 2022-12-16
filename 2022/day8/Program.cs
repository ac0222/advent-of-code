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

for (int i = 0; i < gridHeight; i++)
{
    for (int j = 0; j < gridWidth; j++)
    {
        numVisibleTrees += Convert.ToInt32(IsTreeVisible(i, j));
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

Console.WriteLine(numVisibleTrees);
Console.WriteLine("done");
