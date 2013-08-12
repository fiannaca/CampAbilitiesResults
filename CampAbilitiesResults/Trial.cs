using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CampAbilitiesResults
{
    public class Trial
    {
        public enum TrialPhases
        {
            Warmup,
            Single,
            Simultaneous
        }

        public TrialPhases Phase { get; set; }

        public int Score { get; set; }

        public XDocument TrialInfo { get; set; }

        public List<Target> Targets { get; set; }

        public void ProcessInfo()
        {
            Score = TrialInfo.Descendants().Elements("GameInfo").Select(gi => new {
                        score = Int32.Parse(gi.Element("Score").Value)
                    }).FirstOrDefault().score;

            Targets = new List<Target>();

            var targets = TrialInfo.Descendants().Elements("Targets").FirstOrDefault().Elements("TargetData");

            foreach (XElement t in targets)
            {
                Targets.Add(new Target(t));
            }
        }
    }
}
