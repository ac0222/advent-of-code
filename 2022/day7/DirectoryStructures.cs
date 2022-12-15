public class ElfDirectory
{
    public ElfDirectory Parent {get; set;}
    public string DirectoryFullPath {get; set;}
    public string DirectoryName {get; set;}
    public Dictionary<string, int> Files {get; set;} = new();
    public Dictionary<string, ElfDirectory> Directories {get; set;} = new();
}