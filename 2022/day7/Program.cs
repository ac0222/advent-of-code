// See https://aka.ms/new-console-template for more information

string[] lines = File.ReadAllLines("input.txt");
ElfDirectory rootDir = null;
ElfDirectory currentDir = null;

// create directory structures
foreach (string line in lines)
{
    string[] tokens = line.Split(" ");
    if (tokens[0] == "$") 
    {
        string command = tokens[1];
        if (command == "cd")
        {
            string dir = tokens[2];
            if (dir == "..")
            {
                currentDir = currentDir.Parent;
            }
            else if (dir == "/")
            {
                // init root dir
                if (rootDir == null)
                {
                    rootDir = new ElfDirectory 
                    {
                        Parent = null,
                        DirectoryFullPath = dir,
                        DirectoryName = dir
                    };
                }
                currentDir = rootDir;
            }
            else
            {
                currentDir = currentDir.Directories[dir];
            }
        }
        else if (command == "ls")
        {
            continue;
        }
    }
    else
    {
        if (tokens[0] == "dir")
        {
            string dir = tokens[1];
            ElfDirectory newChildDir = new ElfDirectory
            {
                Parent = currentDir,
                DirectoryFullPath = $"{currentDir.DirectoryFullPath}{dir}/",
                DirectoryName = dir
            };
            currentDir.Directories.Add(dir, newChildDir);
        }
        else
        {
            currentDir.Files.Add(tokens[1], Convert.ToInt32(tokens[0]));
        }
    }
}

// get directory sizes
Dictionary<string, int> sizes = new Dictionary<string, int>();
int rootDirSize = GetDirectorySize(rootDir, sizes);

int GetDirectorySize (ElfDirectory dir, Dictionary<string, int> sizes)
{
    int totalFileSize = dir.Files.Values.Sum();
    int dirSize = totalFileSize + dir.Directories.Values.Select((childDir) => {
        if (sizes.ContainsKey(childDir.DirectoryFullPath)) 
        {   
            return sizes[childDir.DirectoryFullPath];
        }
        else
        {
            return GetDirectorySize(childDir, sizes);
        }
    })
    .Sum();

    if (!sizes.ContainsKey(dir.DirectoryFullPath))
    {
        sizes.Add(dir.DirectoryFullPath, dirSize);
    }
    return dirSize;
}

int CUTOFF = 100_000;
int sumOfBelowCutoff = sizes.Values.Where(x => x <= CUTOFF).Sum();
Console.WriteLine(sumOfBelowCutoff);
Console.WriteLine("done");