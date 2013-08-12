using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace CampAbilitiesResults
{
    public class Target
    {
        public int ID { get; set; }

        public Arms Controller { get; set; }

        public Point Position { get; set; }

        public DateTime StartTime { get; set; }

        public double ScanningTime { get; set; }

        public double RecognitionTime { get; set; }

        public double AliveTime { get; set; }

        public List<DataPoint> DataPoints { get; set; }

        public Target(XElement TargetNode)
        {
            //Set the main attributes
            ID = Int32.Parse(TargetNode.Attribute("ID").Value);
            Controller = TargetNode.Attribute("Controller").Value == "RightHand" ? Arms.Right : Arms.Left;
            Position = new Point(Int32.Parse(TargetNode.Attribute("X").Value), Int32.Parse(TargetNode.Attribute("Y").Value));
            StartTime = DateTime.Parse(TargetNode.Attribute("Time").Value);
            ScanningTime = Double.Parse(TargetNode.Element("ScanningTime").Value);
            RecognitionTime = Double.Parse(TargetNode.Element("RecognitionTime").Value);
            AliveTime = Double.Parse(TargetNode.Element("TotalAliveTime").Value);

            //Gather the data points for the target
            DataPoints = new List<DataPoint>();

            var points = TargetNode.Descendants().Elements("Position");

            foreach (XElement p in points)
            {
                DataPoints.Add(new DataPoint(p));
            }
        }
    }
}
