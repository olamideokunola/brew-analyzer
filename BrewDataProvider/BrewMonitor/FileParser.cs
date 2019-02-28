/*
 * Parsers the file content to extract process parameters
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace BrewDataProvider.BrewMonitor
{
    public class FileParser : IFileParser
    {
        private StreamReader reader;
        private string fullPath = "";
        private IDictionary<string, string> _headerFields = new Dictionary<string, string>();
        private IDictionary<string, IDictionary<string, string>> _sectionFields = new Dictionary<string, IDictionary<string, string>>();
        private IDictionary<int, string> _fileLines = new Dictionary<int, string>();

        public FileParser()
        {

        }

        public IDictionary<string, string> GetHeaderFields()
        {
            //1. Get all header lines into a dictionary
            IDictionary<int, string> rawHeaderLines = GetRawHeaderLines();

            //2. Break each line to fields using ; delimiter

            foreach(KeyValuePair<int, string> rawHeaderLine in rawHeaderLines)
            {
                string line = rawHeaderLine.Value;
                string[] headerFieldArray = line.Split(';', (char)StringSplitOptions.RemoveEmptyEntries);
                //3. Put Field and value into Header Fields Dictionary
                _headerFields = new Dictionary<string, string>
                {
                    { headerFieldArray[0], headerFieldArray[1] }
                };
            }
            return _headerFields;
        }

        public IDictionary<string, IDictionary<string, string>> GetSectionFields()
        {
            _sectionFields = new Dictionary<string, IDictionary<string, string>>();
            //1. Scan the file, get the sections, 
            //   the line number of the first field,
            //   and the line number of the last fild,
            //   place them in a Dictionary sectionLines
            IDictionary<int, IDictionary<string, string>> sectionLines = new Dictionary<int, IDictionary<string, string>>();
            sectionLines = GetSectionLines();

            //2. Put each line of the text file into a dictionary 
            //   with line number as key in fileLines
            _fileLines = new Dictionary<int, string>();
            _fileLines = GetFileLines();

            //3. Go through the dictionary of section lines (sectionLines), 
            //   for each:
            //      - get all line fields for each section into a dictionary
            //      - add the new dictionary as value with the sectionText as key in 
            //        the sectionFields Dictionary: IDictionary<string, IDictionary<string, string>>
            //      - Where the section name exists already, count the number of occurences and add 1 to the number, the result should 
            //        be a suffix of the name which will be added to the sectionLines dictionary

            int index = 0;
            string nSectionText = "";
            foreach (KeyValuePair<int, IDictionary<string, string>> sectionLine in sectionLines)
            {
                IDictionary<string, string> sectionAttributes = new Dictionary<string, string>();
                sectionAttributes = sectionLine.Value;

                string sectionText = sectionAttributes["sectionText"];
                int firstFieldLineNumber = int.Parse(sectionAttributes["firstFieldLineNumber"]);
                int lastFieldLineNumber = int.Parse(sectionAttributes["lastFieldLineNumber"]);

                index = NumberOfTimesInSectionFields(sectionText, _sectionFields);

                //Add sectionText as key
                if (index == 0)
                {
                    nSectionText = sectionText;
                    _sectionFields.Add(nSectionText, new Dictionary<string, string>());
                }
                else if (index > 0)
                {
                    nSectionText = sectionText + " " + index.ToString();
                    _sectionFields.Add(nSectionText, new Dictionary<string, string>());
                }

                //Get line fields for _sectionFields
                IDictionary<string, string> sectionParams = new Dictionary<string, string>();

                if (_sectionFields.ContainsKey(nSectionText))
                {
                    sectionParams = _sectionFields[nSectionText];
                }
                //Get line fields for section
                IDictionary<string, string> lineFields = GetLineFields(firstFieldLineNumber, lastFieldLineNumber);

                //Add field keys and values
                foreach (KeyValuePair<string, string> lineField in lineFields)
                {
                    sectionParams.Add(lineField.Key, lineField.Value);
                }
            }

            return _sectionFields;
        }

        private IDictionary<string, string> GetLineFields(int firstFieldLineNumber, int lastFieldLineNumber)
        {
            //3. Go through the dictionary of section lines (sectionLines), 
            //   for each:
            //      - get each line
            //      - split each line into parts using delimiter ';'
            //      - where there are 3 parts, the first part is the field name prefix, the second adds " - DURATION" to the prefix
            //        the third adds " - FINISH" to the prefix
            //      - where there are 2 parts, the first part is the field name prefix, the second adds " - VALUE" to the prefix
            //      - add each part and value to a dictionary of fields using the names derived above and the corresponding value 
            //        of the second and third part

            IDictionary<string, string> lineFields = new Dictionary<string, string>();

            string fieldLine = "";
            int index = 0;
            for (int i = firstFieldLineNumber; i <= lastFieldLineNumber; i++ )
            {
                if(_fileLines.ContainsKey(i)){
                    fieldLine = _fileLines[i];
                    string[] lineParts = fieldLine.Split(';');

                    if (lineParts.Length == 3)
                    {
                        index = NumberOfTimesInLineFields(lineParts[0].Trim(), lineFields);
                        if (index == 0){
                            lineFields.Add(lineParts[0].Trim() + " - Duration", lineParts[1].Trim());
                            lineFields.Add(lineParts[0].Trim() + " - Finish", lineParts[2].Trim());
                        } else if(index > 0){
                            lineFields.Add(lineParts[0].Trim() + " - Duration " + index.ToString(), lineParts[1].Trim());
                            lineFields.Add(lineParts[0].Trim() + " - Finish " + index.ToString(), lineParts[2].Trim());
                        }
                    } else if(lineParts.Length == 2)
                    {
                        index = NumberOfTimesInLineFields(lineParts[0].Trim(), lineFields);
                        if (index == 0){
                            lineFields.Add(lineParts[0].Trim() + " - Value", lineParts[1].Trim());
                        }
                        else if (index > 0)
                        {
                            lineFields.Add(lineParts[0].Trim() + " - Value " + index.ToString(), lineParts[1].Trim());
                        }
                    }
                }

            }

            return lineFields;
        }

        private int NumberOfTimesInLineFields(string searchText, IDictionary<string, string> lineFields)
        {
            int num = 0;

            foreach(KeyValuePair<string, string> lineField in lineFields)
            {
                if(lineField.Key.Contains(searchText))
                {
                    num++;
                }
            }

            return num;
        }

        private int NumberOfTimesInSectionFields(string searchText, IDictionary<string, IDictionary<string, string>> sectionFields)
        {
            int num = 0;
            if (sectionFields.ContainsKey(searchText))
            {
                foreach (KeyValuePair<string, IDictionary<string, string>> sectionField in sectionFields)
                {
                    string sectionFieldString = sectionField.Key;
                    if (sectionFieldString.Contains(searchText))
                    {
                        num++;
                    }
                }   
            }
            return num;
        }


        private IDictionary<int, IDictionary<string, string>> GetSectionLines(){
            //1. Scan the file, get the sections (sectionLines), store the line number key and the value another dictionary 
            //   of line text (sectionText), 
            //2. line number of first field (firstFieldLineNumber) and line number of the last field (lastFieldLineNumber)

            int lineNumber = 0;
            reader = new StreamReader(fullPath);
            //Read first line from text file
            string line = reader.ReadLine().Trim();

            //read other lines from the text file
            IDictionary<int, IDictionary<string, string>> sectionLines = new Dictionary<int, IDictionary<string, string>>();

            string sectionText = "";
            int previousSectionLineNumber = 0;
            int lastFieldLineNumber = 0;

            while (line != null)
            {
                lineNumber++;

                //if (Regex.IsMatch(line,"([)")){

                if (line.Contains("[ ")){
                    //Record lastFieldLineNumber for previous section if previousSectionLineNumber > 0 (not first section)
                    if (previousSectionLineNumber > 0)
                    {
                        IDictionary<string, string> previousSectionAttributes = sectionLines[previousSectionLineNumber];
                        lastFieldLineNumber = lineNumber - 1;
                        previousSectionAttributes.Add("lastFieldLineNumber", lastFieldLineNumber.ToString());
                    }

                    //Remove square brackets
                    sectionText = line.Replace("[", "");
                    sectionText = sectionText.Replace("]", "");
                    sectionText = sectionText.Trim();
                    int firstFieldLineNumber = lineNumber + 1;
                    previousSectionLineNumber = lineNumber;

                    IDictionary<string, string> sectionAttributes = new Dictionary<string, string>
                    {
                        { "sectionText", sectionText },
                        { "firstFieldLineNumber", firstFieldLineNumber.ToString()}
                    };


                    sectionLines.Add(lineNumber, sectionAttributes);
                }

                line = reader.ReadLine();

            }
            //Set lastFieldLineNumber for last section
            if (previousSectionLineNumber > 0)
            {
                IDictionary<string, string> previousSectionAttributes = sectionLines[previousSectionLineNumber];
                lastFieldLineNumber = lineNumber - 1;
                previousSectionAttributes.Add("lastFieldLineNumber", lastFieldLineNumber.ToString());
            }

            return sectionLines;
        }

        private IDictionary<int, string> GetFileLines(){
            IDictionary<int, string> fileLines = new Dictionary<int, string>();

            //1st section line contains [ Weigh bin Mash Copper ]
            int lineNumber = 0;
            reader = new StreamReader(fullPath);

            Console.WriteLine("New Reader created! " + reader.GetHashCode());
            //Read first line from text file
            string line = reader.ReadLine().Trim();

            //read other lines from the text file
            while (line != null)
            {
                lineNumber++;
                if (line.Contains(";"))
                {
                    fileLines.Add(lineNumber, line);
                }
                line = reader.ReadLine();
            }
            return fileLines;
        }

        public void Initialize(string filePath, string brewNumber)
        {
            fullPath = "";
            fullPath = filePath + brewNumber + ".txt";
            reader = new StreamReader(fullPath);
            Console.WriteLine("In FileParser Initialized!");
        }

        private IDictionary<int, string> GetRawHeaderLines(){
            //1. Read file line by line until 1st section of Process Parameter is encountered
            //1st section line contains [ Weigh bin Mash Copper ]
            int lineNumber = 0;
            reader = new StreamReader(fullPath);
            //Read first line from text file
            string line = reader.ReadLine();

            //read other lines from the text file
            IDictionary<int, string> rawHeaderLines = new Dictionary<int, string>();
            while (!line.Contains("[ Weigh bin Mash Copper ]"))
            {

                if(line.Contains(";"))
                {
                    lineNumber++;
                    rawHeaderLines.Add(lineNumber, line);
                }
                line = reader.ReadLine();
            }

            return rawHeaderLines;
        }
    }
}
