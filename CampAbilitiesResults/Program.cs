using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CampAbilitiesResults
{
    class Program
    {

        /// <summary>
        /// This program accepts the path to the results directory as an argument, processes the results data, and outputs results files.
        /// The results directory must be of the structure:
        /// 
        /// ./Results_Directory
        ///     /Firstname_Lastname
        ///         /Date_Name_StudyPhase.xml
        ///         /Date_Name_StudyPhase.xml
        ///     /FirstName_Lastname
        ///         /Date_Name_StudyPhase.xml
        ///         /etc...
        /// 
        /// The results from this analysis will be output into the /Analysis directory which is created in the directory from which this
        /// program is called.
        /// </summary>
        /// <param name="args">Program arguments</param>
        static void Main(string[] args)
        {
            //Check for correct arguments
            if (args.Length != 1)
            {
                Error("Error: Incorrect number of arguments passed to vires.exe!", false);

                Usage();

                return;
            }

            //Check that the directory exists
            DirectoryInfo di = null;

            try
            {
                di = new DirectoryInfo(args[0]);
            }
            catch (Exception e)
            {
                Error("Invalid path argument! Verify that the path is correct and that you have read access to this directory.");

                return;
            }

            //Gather the names and files for each of the participants
            DirectoryInfo[] participants = di.GetDirectories("*", SearchOption.TopDirectoryOnly);
                
            List<Person> People = new List<Person>();

            try
            {
                People = participants.Select(p =>
                {
                    string[] parts = p.Name.Split(new char[] { '_' });

                    return new Person { FirstName = parts[0], LastName = parts[1], Files = p.GetFiles().ToList() };
                }).ToList();
            }
            catch (Exception e)
            {
                Error("The results directory could not be properly traversed! Check that the directory structure is correct and the files are named appropriately");
            }

            //Gather the data for each of the people
            if (People.Count() > 0)
            {
                foreach (Person p in People)
                {
                    p.GatherInfo();
                }
            }
            else
            {
                Error("There are no participant results folders in the main results folder!");
            }
        }

        /// <summary>
        /// This function outputs the usage information for the program
        /// </summary>
        private static void Usage()
        {
            Console.WriteLine();
            Console.WriteLine(@"Usage: vires.exe ./PathToResultsDirectory/");
            Console.WriteLine();
        }

        /// <summary>
        /// This function prints an error message to the screen and then exits the program
        /// </summary>
        /// <param name="e">The error message</param>
        /// <param name="exit">Closes the program if set to true</param>
        private static void Error(string e, bool exit = true)
        {
            Console.WriteLine();
            Console.WriteLine("Error: " + e);
            Console.WriteLine();
            Console.WriteLine("Exiting vires.exe...");
            Console.WriteLine();

            if(exit)
                Environment.Exit(-1);
        }
    }
}
