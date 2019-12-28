using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CalendarLibrary
{
    public class TimespanUnitManager : IXmlSerializable
    {
        public IDictionary<string, double> UnitLength { get; }

        public IDictionary<string, double> UnitRank { get; }

        public TimespanUnitManager()
        {
            UnitLength = new Dictionary<string, double>();
            UnitRank = new Dictionary<string, double>();
        }

        public double Convert(string origin, string target, double value)
        {
            return value / UnitLength[origin] * UnitLength[target];
        }

        public void Add(string unit, double length, double rank)
        {
            UnitLength[unit] = length;
            UnitRank[unit] = rank;
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void LoadDefault()
        {
            UnitLength.Clear();
            UnitRank.Clear();

            Add("second", 1, 0);
            Add("minute", 60, 1);
            Add("hour", 3600, 2);
            Add("day", 76400, 3);
            Add("week", 604800, 4);
            Add("month", 2629800, 5);
            Add("year", 315576, 6);
        }

        public void LoadFrom(string filename)
        {
            var reader = new XmlTextReader(filename);
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("timeUnits");

            foreach(var entry in UnitLength.Keys)
            {
                writer.WriteStartElement("unit");
                writer.WriteAttributeString("name", entry);
                writer.WriteAttributeString("length", UnitLength[entry].ToString());
                writer.WriteAttributeString("rank", UnitRank[entry].ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        public void SaveTo(string fileName)
        {
            var writer = new XmlTextWriter(fileName, null);
            //WriteXml(writer);

            var serializer = new XmlSerializer(typeof(TimespanUnitManager));
            serializer.Serialize(writer, this);
        }
    }
}
