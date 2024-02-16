namespace Task2
{
    internal class Program
    {
        static object block = new object();
        static string ReadContentFromTwoFiles(string firstFileName,  string secondFileName)
        {
            string filesContent;
            using(StreamReader reader = new StreamReader(firstFileName))
            {
                filesContent = reader.ReadToEnd();
            }
            using(StreamReader reader = new StreamReader(secondFileName))
            {
                filesContent += "\n";
                filesContent += reader.ReadToEnd();
            }
            Console.WriteLine("reading successful");
            return filesContent;
        }

        static void WriteCOntentToFile(string fileName, string content)
        {
            lock (block)
            {
                using(StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.Write(content);
                }
            }
            Console.WriteLine("writing successful");
        }

        static void ReadAndWrite()
        {
            WriteCOntentToFile("output.txt", ReadContentFromTwoFiles("input1.txt", "input2.txt"));
        }
        static void Main(string[] args)
        {
            Thread first = new Thread(ReadAndWrite);
            Thread second = new Thread(ReadAndWrite);
            second.Start();
            first.Start();
        }
    }
}
