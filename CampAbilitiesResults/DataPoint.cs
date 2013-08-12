using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace CampAbilitiesResults
{
    public enum TargetStates
    {
        Active,
        Found,
        Missed,
        Collecting
    }

    public enum TriggerStates
    {
        NotPressed,
        JustPressed,
        StillPressed
    }

    public class DataPoint
    {
        public int ID { get; set; }

        public Point Position { get; set; }

        public TargetStates TargetState { get; set; }

        public TriggerStates TriggerState { get; set; }

        public double ElapsedTime { get; set; }

        public DataPoint(XElement node)
        {
            ID = Int32.Parse(node.Attribute("ID").Value);
            Position = new Point(Int32.Parse(node.Attribute("X").Value), Int32.Parse(node.Attribute("Y").Value));
            
            switch(node.Attribute("TargetState").Value)
            {
                case "Active": TargetState = TargetStates.Active; break;
                case "Found": TargetState = TargetStates.Found; break;
                case "Missed": TargetState = TargetStates.Missed; break;
                case "Collecting": TargetState = TargetStates.Collecting; break;
            }

            switch (node.Attribute("TriggerState").Value)
            {
                case "NotPressed": TriggerState = TriggerStates.NotPressed; break;
                case "JustPressed": TriggerState = TriggerStates.JustPressed; break;
                case "StillPressed": TriggerState = TriggerStates.StillPressed; break;
            }
        }
    }
}
