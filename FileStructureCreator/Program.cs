using System.ComponentModel.Design;

namespace FileStructureCreator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;


            Console.WriteLine("This script will scan the Parent Directory" +
                " and checks if there are any files that meets specific naming conventions");
            Console.WriteLine();
            Console.WriteLine("After that a directory will be created and the files " +
                "will be moved to it.");
            Console.WriteLine();
            Console.WriteLine($"Folgender Pfad wurde gefunden: {currentPath}");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Proceed? (Yes = y) (No = any key)");

            string? succeed = Console.ReadLine();

            if (succeed != null && succeed.ToLower() == "y")
            {

                if (Directory.Exists(currentPath))
                {
                    string[] files = Directory.GetFiles(currentPath);

                    foreach (string file in files)
                    {
                        Console.WriteLine($"File found {file}.");
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        string[] nameParts = fileName.Split('_');

                        if (nameParts != null && int.TryParse(nameParts[0], out int folderNumber))
                        {
                            Console.WriteLine("File meets the naming requirements.");
                            string folderPath = Path.Combine(currentPath, folderNumber.ToString());

                            if (!Directory.Exists(folderPath))
                            {
                                Console.WriteLine($"Folder doesnt exist...creating folder: {folderNumber.ToString()}");
                                Directory.CreateDirectory(folderPath);
                                Console.WriteLine("Folder createt.");
                            }

                            string newFilePath = Path.Combine(folderPath, Path.GetFileName(file));
                            if (!File.Exists(newFilePath))
                            {
                                Console.WriteLine("Moving file to folder...");
                                File.Move(file, newFilePath);
                                Console.WriteLine("File successful moved.");
                            }
                            else
                            {
                                Console.WriteLine($"File already exists: {newFilePath}");
                                Console.WriteLine("Abort.");
                            }
                        }

                        else
                        {
                            Console.WriteLine($"Invalid name: {file}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Could not find directory...maybe it´s protected through network...?");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Program finished...Press any key to close.");
            Console.ReadLine();
        }
    }
}