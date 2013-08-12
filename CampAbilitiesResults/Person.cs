using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace CampAbilitiesResults
{
    public enum Arms
    {
        Right,
        Left
    }

    public class Person
    {
        public enum Genders
        {
            Male,
            Female
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<FileInfo> Files { get; set; }

        public double ArmLength { get; set; }

        public double Height { get; set; }

        public Genders Gender { get; set; }

        public Arms DominantArm { get; set; }

        public List<Trial> Trials { get; set; }

        public void GatherInfo()
        {
            //Load the trial info for this person
            Trials = new List<Trial>();

            for (int i = 0; i < Files.Count; i++)
            {
                Trial temp = new Trial();

                temp.TrialInfo = XDocument.Load(Files[i].FullName);
                temp.ProcessInfo();

                Trials.Add(temp);
            }

            //Gather info from the trial info list
            if (Trials.Count() > 0)
            {
                var data = Trials[0].TrialInfo.Descendants().Elements("User").Select(user => new
                {
                    length = Double.Parse(user.Element("ArmLength").Value),
                    height = Double.Parse(user.Element("Height").Value),
                    gender = (user.Element("Gender").Value == "M") ? Genders.Male : Genders.Female,
                    arm = (user.Element("DominantArm").Value == "Right") ? Arms.Right : Arms.Left
                }).FirstOrDefault();

                if (data != null)
                { 
                    ArmLength = data.length;
                    Height = data.height;
                    Gender = data.gender;
                    DominantArm = data.arm;
                }
            }
        }
    }
}
