/*
 * specifies the fields in the brew file that should be monitored
 */
using System;
using System.Collections.Generic;

namespace BrewDataProvider.BrewMonitor

{
    public class RequiredBrewParameters
    {
        private IDictionary<string, IDictionary<string, string>> _requiredParameters = new Dictionary<string, IDictionary<string, string>> ();
       //private IDictionary<string, IDictionary<string, string>> _startSectionAndFields = new Dictionary<string, IDictionary<string, string>>();
        private IDictionary<string, string> param = new Dictionary<string, string>();

        public IDictionary<string, IDictionary<string, string>> GetRequiredBrewParameters()
        {
            return _requiredParameters;
        }

        public RequiredBrewParameters()
        {
            //SetStartSectionAndFields();
            SetupRequiredParameters();
        }


        //Checks if the supplied section and field and in the list of required fields
        public Boolean FieldIsRequired(string processSection, string fieldName)
        {
            if (_requiredParameters.ContainsKey(processSection))
            {
                IDictionary<string, string> sectionFields = new Dictionary<string, string>();

                foreach (KeyValuePair<string, IDictionary<string, string>> requiredBrewParameter in _requiredParameters)
                {
                    sectionFields = requiredBrewParameter.Value;
                    if (sectionFields.ContainsKey(fieldName))
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        //May not be needed
        //Checks if the supplied field has a value
        public Boolean FieldIsAvailable(string processSection, string fieldName)
        {
           //Console.WriteLine("fieldName: " + fieldName);

            if (_requiredParameters.ContainsKey(processSection))
            {
                IDictionary<string, string> sectionFields = new Dictionary<string, string>();

                foreach (KeyValuePair<string, IDictionary<string, string>> requiredParameter in _requiredParameters)
                {
                    sectionFields = new Dictionary<string, string>();
                    sectionFields = requiredParameter.Value;
                    string sectionText = requiredParameter.Key;
                    if (sectionFields.ContainsKey(fieldName))
                    {
                        //Console.WriteLine("fieldName: " + fieldName);
                        string fieldValue = sectionFields[fieldName];
                        if (fieldValue.Length > 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        //Populate _requiredSectionParams with availableSectionParams values from file
        public void PopulateFields(IDictionary<string, IDictionary<string, string>> availableSectionFields)
        {
            IDictionary<string, string> storedSectionParams = new Dictionary<string, string>();
            IDictionary<string, string> newSectionParams = new Dictionary<string, string>();
            //Get each element of new availableSectionFields
            foreach (KeyValuePair<string, IDictionary<string, string>> availableSection in availableSectionFields)
            {
                if(_requiredParameters.ContainsKey(availableSection.Key))
                {
                    //Add section and fields to _requiredSectionParams
                    //a. Get stored section parameters
                    storedSectionParams = _requiredParameters[availableSection.Key];

                    //b. Get new section parameters 
                    newSectionParams = availableSection.Value;

                    //c. Get each field of the current section in the new fields in availableSectionFields
                    foreach(KeyValuePair<string, string> nParam in newSectionParams)
                    {
                        //d. Change the corresponding content of the stored parameters
                        storedSectionParams[nParam.Key] = nParam.Value;
                    }
                }
            }
        }

        public Boolean HasStartField(IDictionary<string, IDictionary<string, string>> availableFields)
        {
            RequiredFieldsChecker requiredFieldsChecker = new RequiredFieldsChecker();
            return requiredFieldsChecker.HasStartField(availableFields);
        }

        public IDictionary<string, string> GetAvailableStartField(IDictionary<string, IDictionary<string, string>> availableFields)
        {
            RequiredFieldsChecker requiredFieldsChecker = new RequiredFieldsChecker();
            return requiredFieldsChecker.GetStartField(availableFields);
        }

        //private void SetStartSectionAndFields()
        //{
        //    IDictionary<string, string> startField = new Dictionary<string, string>
        //    {
        //        { "FieldName", "Transport Time RAW Sorghum to WB MC - Finish" },
        //        { "FieldValue", "" }
        //    };

        //    IDictionary<string, string> section = new Dictionary<string, string>
        //    {
        //        { "SectionName", "Weigh bin Mash Copper" }
        //    };

        //    _startSectionAndFields.Add("Section", section);
        //    _startSectionAndFields.Add("StartField", startField);
        //}

        //Add all required fields for each equipment
        private void SetupRequiredParameters()
        {
            AddMashCopperFields();
            AddMashTunFields();
            AddMashFilterFields();
            AddHoldingVesselFields();
            AddWortCopperFields();
            AddWhirlpoolFields();
        }

        private void AddMashCopperFields()
        {

            //Weigh bin Mash Copper Section
            param = new Dictionary<string, string>
            {
                { "Start Transport RAW Sorguum to WB MC - Finish", "" },
                { "Transport Time RAW Sorguum to WB MC - Finish", "" }
            };

            this._requiredParameters.Add("Weigh bin Mash Copper", param);

            //Mash Copper Section
            param = new Dictionary<string, string>
            {
                { "Mash in Time - Finish", "" },
                { "Protein Rest Time - Finish", "" },
                { "Heating Time - Finish", "" },
                { "Rest time - Finish", "" },
                { "Heating Time - Finish 2", "" },
                { "Rest Time - Finish 2", "" },
                { "Mash Transfer to MT - Finish", "" },
                { "Mash Copper empty at - Finish", "" }
            };

            this._requiredParameters.Add("MASH COPPER", param);
        }

        private void AddMashTunFields()
        {
            //WEIGHBIN MASHTUN
            param = new Dictionary<string, string>
            {
                { "Start Grist Transort - Finish", "" },
                { "Tranpsort Time - Finish", "" }
            };

            this._requiredParameters.Add("WEIGHBIN MASHTUN", param);

            //MASH TUN Section
            param = new Dictionary<string, string>
            {
                { "Mash in Time - Finish", "" },
                { "Protein Rest Time - Finish", "" },
                { "Transfer Time from MC - Finish", "" },
                { "Sacha. Rest time - Finish", "" },
                { "Heating Time - Finish", "" },
                { "Main Mash Transfer to MF - Finish", "" },
                { "Mash Tun ready at - Finish", "" }
            };

            this._requiredParameters.Add("MASH TUN", param);

        }

        private void AddMashFilterFields()
        {
            //MASH FILTER Section
            param = new Dictionary<string, string>
            {
                { "Start Prefilling - Finish", "" },
                { "Prefilling Time - Finish", "" },
                { "WeakWort Transfer to WWT - Finish", "" },
                { "Main Mash Filtration Time - Finish", "" },
                { "Sparging Time - Finish", "" },
                { "Sparging to WWT Time - Finish", "" },
                { "Extra Sparging Time - Finish", "" },
                { "Dripping Time - Finish", ""},
                { "Spent Grain Discharge - Finish", "" },
                { "Cleaning MF - Finish", "" },
                { "Mash Filter Ready at - Finish", ""}
            };

            this._requiredParameters.Add("MASH FILTER", param);

        }

        private void AddHoldingVesselFields()
        {
            //HOLDING VESSEL Section
            param = new Dictionary<string, string>
            {
                { "Start Filling - Finish", "" }
            };

            this._requiredParameters.Add("HOLDING VESSEL", param);

            //HOLDING VESSEL TO WORT COPPER Section
            param = new Dictionary<string, string>
            {
                { "Transfer Time (WC) - Finish", "" },
                //{ "Rinsing - Finish", "" },
                { "Holding Vessle empty at - Finish", ""}
            };

            this._requiredParameters.Add("HOLDING VESSEL TO WORT COPPER", param);

        }

        private void AddWortCopperFields()
        {
            //WORT COPPER Section
            param = new Dictionary<string, string>
            {
                { "Start Heating(Boiling) - Finish", "" },
                { "Heating Time (Boiling) - Finish", "" },
                { "Wort Boiling - Finish", "" },
                { "Extra Boiling Time - Finish", "" },
                { "Casting to Whirpool - Finish", "" },
                { "Wort Copper Finish(No cip) - Finish", "" },
                { "Daily Cleaning - Finish", "" },
                { "Wort Copper Finish(with cip) - Finish", ""}
            };

            this._requiredParameters.Add("WORT COPPER", param);

        }

        private void AddWhirlpoolFields()
        {
            //WHIRLPOOL Section
            param = new Dictionary<string, string>
            {
                { "Start Casting - Finish", "" },
                { "Casting Time - Finish", "" },
                { "Rest Time - Finish", "" },
                { "Wort Cooling - Finish", "" },
                { "Trub Removal - Finish", "" },
                { "Whirlpool Ready at - Finish", "" }
            };

            this._requiredParameters.Add("WHIRLPOOL", param);

        }
    }
}
