using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AstrologyCalculator.TimespanUnits
{
    public class TimespanUnitManager : IXmlSerializable
    {
        // Constants used within class
        #region Constants

        private const string timeUnitsLabel = "timeUnits";
        private const string unitLabel = "unit";
        private const string nameLabel = "name";
        private const string lengthLabel = "length";
        private const string rankLabel = "rank";
        private const string second = "second";
        private const string minute = "minute";
        private const string hour = "hour";
        private const string day = "day";
        private const string week = "week";
        private const string month = "month";
        private const string sydonicMonth = "sydonic_month";
        private const string year = "year";
        private const string realYear = "real_year";
        private List<string> preexistingUnitNames = new List<string> { second, minute, hour, day, week, month, sydonicMonth, year, realYear };

        #endregion Constants

        public string BaseUnit => second;

        private IDictionary<string, double> UnitLength { get; }

        private IDictionary<string, int> UnitRank { get; }

        public TimespanUnitManager()
        {
            UnitLength = new Dictionary<string, double>();
            UnitRank = new Dictionary<string, int>();
        }

        public double Convert(string origin, string target, double value)
        {
            return value / UnitLength[origin] * UnitLength[target];
        }

        /// <summary>
        /// Adds or overrides the information with the data given.
        /// </summary>
        /// <param name="unit">Name of the unit.</param>
        /// <param name="length">How many seconds long is the unit.</param>
        /// <param name="rank">The Rank of the unit.</param>
        public void AddOrOverride(string unit, double length, int rank)
        {
            UnitLength[unit] = length;
            UnitRank[unit] = rank;
        }

        public void LoadDefault()
        {
            UnitLength.Clear();
            UnitRank.Clear();

            SetDefaultValues();
        }

        public IReadOnlyCollection<string> AvailableUnits()
        {
            return (IReadOnlyCollection<string>)UnitLength.Keys;
        }

        public double GetLength(string unit)
        {
            return UnitLength[unit];
        }

        public void SetLength(string unit, double length)
        {
            if (!UnitLength.Keys.Contains(unit))
                throw new KeyNotFoundException();
            UnitLength[unit] = length;
        }

        public int GetRank(string unit)
        {
            return UnitRank[unit];
        }

        public void SetRank(string unit, int rank)
        {
            if (!UnitRank.Keys.Contains(unit))
                throw new KeyNotFoundException();
            UnitRank[unit] = rank;
        }
        
        public void SetDefaultValues()
        { 
            AddOrOverride(second, 1, 0);
            AddOrOverride(minute, UnitLength[second] * 60, 1);
            AddOrOverride(hour, UnitLength[minute] * 60, 2);
            AddOrOverride(day, UnitLength[hour] * 24, 3);
            AddOrOverride(week, UnitLength[day] * 7, 4);
            AddOrOverride(month, UnitLength[day] * 30, 5);
            AddOrOverride(sydonicMonth, UnitLength[day] * 29.53, 5);
            AddOrOverride(year, UnitLength[month] * 12, 6);
            AddOrOverride(realYear, UnitLength[day] * 365.25, 6);
        }

        public void LoadFrom(string filename)
        {
            var reader = new XmlTextReader(filename);
            var serializer = new XmlSerializer(typeof(TimespanUnitManager));
            TimespanUnitManager result = (TimespanUnitManager)serializer.Deserialize(reader, null);
            var keys = result.UnitLength.Keys;

            foreach(var key in keys)
            {
                AddOrOverride(key, result.UnitLength[key], result.UnitRank[key]);
            }

            SetDefaultValues();
        }

        public void SaveTo(string fileName)
        {
            var writer = new XmlTextWriter(fileName, null);
            var serializer = new XmlSerializer(typeof(TimespanUnitManager));
            serializer.Serialize(writer, this);
        }

        #region ISerializable

        public void ReadXml(XmlReader reader)
        {
            reader.Read();

            reader.ReadStartElement(timeUnitsLabel);

            while (reader.Name.Equals(unitLabel) && reader.NodeType == XmlNodeType.Element)
            {
                string name = reader.GetAttribute(nameLabel);
                double length = double.Parse(reader.GetAttribute(lengthLabel));
                int rank = int.Parse(reader.GetAttribute(rankLabel));

                AddOrOverride(name, length, rank);

                reader.Read();
            }

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(timeUnitsLabel);

            foreach(var entry in UnitLength.Keys)
            {
                writer.WriteStartElement(unitLabel);
                writer.WriteAttributeString(nameLabel, entry);
                writer.WriteAttributeString(lengthLabel, UnitLength[entry].ToString());
                writer.WriteAttributeString(rankLabel, UnitRank[entry].ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        #endregion ISerializable
    }
}
