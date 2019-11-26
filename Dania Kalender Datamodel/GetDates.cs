using System;
using System.Collections.Generic;
using System.IO;

namespace Dania_Kalender_Datamodel
{
    public class GetDates
    {
        public string FullPath { get; set; }
        private string tempPath;
        public Dictionary<DateTime, string> OrderedResults = new Dictionary<DateTime, string>();
        public List<(DateTime, string)> OrderedResultsTuple = new List<(DateTime, string)>();

        public string[] allLines { get; set; }

        public GetDates(string fullPath)
        {
            FullPath = fullPath;
            tempPath = fullPath;

            allLines = ReadFile(tempPath);

            FindDates(allLines);
        }
        private bool isItADate(ref string text)
        {
            //text = string.Join('/', text.Split('.', '-'));

            bool isDateTime = false;

            isDateTime = DateTime.TryParse(text, out _);
            return isDateTime;
        }
        private void FindDates(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                string[] words = lines[i].Trim().Split(' ');
                if (!isItADate(ref words[0]))
                {
                    continue;
                }
                DateTime date = Convert.ToDateTime(words[0]);
                string info = "";

                // add rest of line
                for (int j = 0; j < words.Length; j++)
                {
                    if (isItADate(ref words[j])) continue;
                    info += words[j] + "_";
                }
                // if to little info, add more from next line
                // TODO - this is shit, needs more IQ
                if (info.Length < 1 && lines.Length > i + 1)
                {
                    int addIndex = lines[i + 1].Trim().Length == 0 ? 2 : 1;

                    string extraLine = lines[i + addIndex].Replace('\t', ' ');
                    extraLine.Trim();
                    info += extraLine.Substring(0, Math.Min(20, extraLine.Length));
                }
                OrderedResultsTuple.Add((date, info));
            }
        }

        private string[] ReadFile(string path)
        {
            string[] allLines = File.ReadAllLines(path);

            return allLines;
        }
    }
}
