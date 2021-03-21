using System;
using System.IO;
using System.Text;
using LibInMemoryFileSystem;

namespace InMemoryFSTest
{
    class Program
    {
        public static string version = "1.0.0";

        static void Main(string[] args)
        {
            Console.WriteLine("Cloakroom FS " + version);
            Guid[] guids = new Guid[5];
            string[] results = new string[5];

            string[] samples = new string[]
            {
                "This is test string 1",
                "This is test string 2",
                "This is test string 3",
                "This is test string 4",
                "This is test string 5"
            };


            Cloakroom<byte[]> cr = new Cloakroom<byte[]>();

            Console.WriteLine("Saving...");
            for (int i = 0; i < samples.Length; i++)
            {
                guids[i] = cr.Save(Encoding.ASCII.GetBytes(samples[i]));
            }

            Console.WriteLine("Reading...");
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = Encoding.ASCII.GetString(cr.Read(guids[i]));
                Console.WriteLine(results[i]);
            }

            Console.WriteLine("Saving backup...");
            File.WriteAllText("backup.cr", cr.Backup());

            Console.WriteLine("Opening backup...");
            string bk = File.ReadAllText("backup.cr");

            Console.WriteLine("Restoring backup...");
            cr.Clear();
            cr.Restore(bk);

            Console.WriteLine("Reading...");
            //Read
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = Encoding.ASCII.GetString(cr.Read(guids[i]));
                Console.WriteLine(results[i]);
            }

            Console.WriteLine("Finished.  Press ENTER to exit.");
            Console.ReadLine();

        }
    }
}
