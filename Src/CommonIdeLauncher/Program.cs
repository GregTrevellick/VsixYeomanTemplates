namespace CommonIdeLauncher
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ideManager = new IdeManager();
            var generationDirectory = args[0];
            ideManager.OpenNewlyCreatedYoProject(generationDirectory);
        }
    }
}
