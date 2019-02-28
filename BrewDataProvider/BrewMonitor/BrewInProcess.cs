/*
 * Provides access to brew in process
 */

using System;
//using System.Collections.Concurrent;
using System.Collections.Generic;
namespace BrewDataProvider.BrewMonitor
{
    public class BrewInProcess
    {
        private string _filePath;
        private string _brewNumber;

        private IDictionary<string, IDictionary<string, string>> _availableFields = new Dictionary<string, IDictionary<string, string>>();
        private IDictionary<string, IDictionary<string, string>> _existingFields = new Dictionary<string, IDictionary<string, string>>();
        private IDictionary<string, IDictionary<string, string>> _newField = new Dictionary<string, IDictionary<string, string>>();
        //private ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _newField = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();

        private IDictionary<string, string> _headerFields = new Dictionary<string, string>();
        private IDictionary<string, IDictionary<string, string>> _sectionFields = new Dictionary<string, IDictionary<string, string>>();

        public BrewInProcess(string filePath, string brewNumber, IDictionary<string, string> headerFields, IDictionary<string, IDictionary<string, string>> sectionFields)
        {
            this._filePath = filePath;
            this._brewNumber = brewNumber;
            this._headerFields = headerFields;
            SetAvailableFields(sectionFields);
        }

        public BrewInProcess()
        {
            _availableFields = new Dictionary<string, IDictionary<string, string>>();
            _existingFields = new Dictionary<string, IDictionary<string, string>>();
            //_newField = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();
            _newField = new Dictionary<string, IDictionary<string, string>>();
            _filePath = "";
            _brewNumber = "";
            _headerFields = new Dictionary<string, string>();
            _sectionFields = new Dictionary<string, IDictionary<string, string>>();
        }

        internal void ResetNewField()
        {
            _newField.Clear();
        }

        //Getters
        public string FilePath
        {
            get
            {
                return _filePath;
            }
        }

        public string BrewNumber
        {
            get
            {
                return _brewNumber;
            }
        }

        public IDictionary<string, IDictionary<string, string>> NewField
        {
            get
            {
                return _newField;
            }
        }

        public IDictionary<string, IDictionary<string, string>> AvailableFields
        {
            get
            {
                return _availableFields;
            }
        }

        //Other methods
        public Boolean HasNewField()
        {
            return _newField.Count > 0 ? true : false;
        }

        public void Update(IDictionary<string, string> headerFields, IDictionary<string, IDictionary<string, string>> sectionFields)
        {
            //ResetNewField();
            _headerFields = headerFields;
            SetAvailableFields(sectionFields);
        }

        private void SetAvailableFields(IDictionary<string, IDictionary<string, string>> sectionFields)
        {
            Console.WriteLine("In SetAvailableFields!");

            _existingFields = new Dictionary<string, IDictionary<string, string>>();
            _existingFields = _availableFields;

            Console.WriteLine("_existingFields: " + _existingFields.GetHashCode());
            Console.WriteLine("_availableFields: " + _availableFields.GetHashCode());
                
            RequiredBrewParameters requiredBrewParameters = new RequiredBrewParameters();
            requiredBrewParameters.PopulateFields(sectionFields);

            _availableFields = new Dictionary<string, IDictionary<string, string>>();
            _availableFields = requiredBrewParameters.GetRequiredBrewParameters();
            SetNewField(_existingFields, _availableFields);

            Console.WriteLine("_existingFields: " + _existingFields.GetHashCode());
            Console.WriteLine("_availableFields: " + _availableFields.GetHashCode());
        }

        private void SetNewField(IDictionary<string, IDictionary<string, string>> existingFields, IDictionary<string, IDictionary<string, string>> availableFields)
        {
            Console.WriteLine("In SetNewField!");
            IDictionary<string, IDictionary<string, string>> newField = new Dictionary<string, IDictionary<string, string>>();

            foreach (KeyValuePair<string, IDictionary<string, string>> processSection in existingFields)
            {
                IDictionary<string, string> existingParams = processSection.Value;
                IDictionary<string, string> availableParams = availableFields[processSection.Key];

                foreach(KeyValuePair<string, string> param in existingParams)
                {
                    if (availableParams.ContainsKey(param.Key))
                    {
                        string existingParam = param.Value.Trim();
                        string availableParam = availableParams[param.Key];

                        if (existingParam.Length > 0)
                        {
                            //Console.WriteLine("existingParam: " + existingParam);
                        }

                        Console.WriteLine("");
                        Console.WriteLine("param: " + param.Key);
                        Console.WriteLine("existingParam.Length: " + existingParam.Length);
                        Console.WriteLine("availableParam.Length: " + availableParam.Length);
                        Console.WriteLine("");

                        if (existingParam.Length == 0 && availableParam.Length > 0)
                        {
                            Console.WriteLine("In if(existingParam.Length == 0 && availableParam.Length > 0)!");
                            //ConcurrentDictionary<string, string> newParam = new ConcurrentDictionary<string, string>();
                            IDictionary<string, string> newParam = new Dictionary<string, string>();
                            //newParam.TryAdd(param.Key, availableParams[param.Key]);
                            newParam.Add(param.Key, availableParams[param.Key]);
                            //{
                            //    { param.Key, availableParams[param.Key] }
                            //};
                            //_newField.TryAdd(processSection.Key, newParam);
                            _newField.Add(processSection.Key, newParam);
                        }
                    }
                }
            }
        }
    }
}
