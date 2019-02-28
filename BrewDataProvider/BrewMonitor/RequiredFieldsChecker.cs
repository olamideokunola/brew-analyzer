/*
 * Checks if the passed in fields include the field that signifies the start of reading process fields
 */
using System;
using System.Collections.Generic;

namespace BrewDataProvider.BrewMonitor
{
    public class RequiredFieldsChecker
    {
        private IDictionary<string, IDictionary<string, string>> _startSectionAndFields = new Dictionary<string, IDictionary<string, string>>();

        //public IDictionary<string, IDictionary<string, string>> StartSectionAndFields
        //{
        //    get
        //    {
        //        return _startSectionAndFields;
        //    }
        //}

        public RequiredFieldsChecker()
        {
            SetStartSectionAndFields();
        }

        //Checks for the presence of the startField in the passed in availableFields
        public Boolean HasStartField(IDictionary<string, IDictionary<string, string>> availableFields)
        {
            IDictionary<string, string> section = _startSectionAndFields["Section"];
            IDictionary<string, string> startField = _startSectionAndFields["StartField"];

            string sectionName = "";

            if (section.ContainsKey("SectionName"))
            {
                sectionName = section["SectionName"];
            }

            if (availableFields.ContainsKey(sectionName)){
                IDictionary<string, string> sectionFields = availableFields[sectionName];
                if(sectionFields.ContainsKey(startField["FieldName"]))
                {
                    string fieldKey = startField["FieldName"];

                    if(fieldKey.Length > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // This method may not be needed eventually
        public IDictionary<string, string> GetStartField(IDictionary<string, IDictionary<string, string>> availableFields)
        {
            IDictionary<string, string> section = _startSectionAndFields["Section"];
            IDictionary<string, string> startField = _startSectionAndFields["StartField"];

            string sectionName = "";

            if (section.ContainsKey("SectionName"))
            {
                sectionName = section["SectionName"];
            }

            if (availableFields.ContainsKey(sectionName))
            {
                IDictionary<string, string> sectionFields = availableFields[sectionName];
                if (sectionFields.ContainsKey(startField["FieldName"]))
                {
                    string fieldKey = startField["FieldName"];

                    if (fieldKey.Length > 0)
                    {
                           
                        return startField;
                    }
                }
            }
            return null;
        }

        private void SetStartSectionAndFields()
        {
            IDictionary<string, string> startField = new Dictionary<string, string>
            {
                { "FieldName", "Transport Time RAW Sorguum to WB MC - Finish" }
            };

            IDictionary<string, string> section = new Dictionary<string, string>
            {
                { "SectionName", "Weigh bin Mash Copper" }
            };

            _startSectionAndFields.Add("Section", section);
            _startSectionAndFields.Add("StartField", startField);
        }
    }
}