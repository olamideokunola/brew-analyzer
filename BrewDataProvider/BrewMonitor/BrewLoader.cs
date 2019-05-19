/*
    Provides access to live brewsInProcess via brew files
    Keeps a dictionary of brewsInProcess with brewnumber as key
*/
using System;
using System.Collections.Generic;

namespace BrewDataProvider.BrewMonitor
{
    public class BrewLoader
    {

        private IFileParser _fileParser;
        private IDictionary<string, string> _processHeaderFields = new Dictionary<string, string> ();
        private IDictionary<string, IDictionary<string, string>> _processSectionFields = new Dictionary<string, IDictionary<string, string>>();
        private IDictionary<string, BrewInProcess> _brewsInProcess = new Dictionary<string, BrewInProcess>();

        public BrewLoader(IFileParser fileParser)
        {
            _fileParser = fileParser;
        }

        public IDictionary<string, BrewInProcess> BrewsInProcess { get => _brewsInProcess; }

        public BrewInProcess GetBrewInProcess(string filePath, string brewNumber)
        {
            UpdateProcessFields(filePath, brewNumber);
            BrewInProcess brewInProcess = new BrewInProcess(filePath, brewNumber, _processHeaderFields, _processSectionFields);         

            if (!_brewsInProcess.ContainsKey(brewNumber))
            {
                _brewsInProcess.Add(brewNumber, brewInProcess);
            }

            return brewInProcess;
        }

        public void CreateBrewInProcess(string filePath, string brewNumber)
        {
            UpdateProcessFields(filePath, brewNumber);
            BrewInProcess brewInProcess = new BrewInProcess(filePath, brewNumber, _processHeaderFields, _processSectionFields);

            if(!_brewsInProcess.ContainsKey(brewNumber))
            {
                _brewsInProcess.Add(brewNumber, brewInProcess);
            }
        }

        public BrewInProcess GetBrewInProcessOld(string filePath, string brewNumber)
        {
            Console.WriteLine("In GetBrewInProcess!");
            BrewInProcess brewInProcess = null;
            UpdateProcessFields(filePath, brewNumber);

            if (_brewsInProcess.ContainsKey(brewNumber))
            {
                Console.WriteLine("In _brewsInProcess.ContainsKey(brewNumber)!");
                brewInProcess = _brewsInProcess[brewNumber];
                Console.WriteLine("");
                brewInProcess.Update(this._processHeaderFields, this._processSectionFields);
            }

            return brewInProcess;
        }

        private void UpdateProcessFields(string filePath, string brewNumber)
        {
            _fileParser.Initialize(filePath, brewNumber);
            _processHeaderFields = new Dictionary<string, string>();
            _processHeaderFields = _fileParser.GetHeaderFields();
            _processSectionFields = new Dictionary<string, IDictionary<string, string>>();
            _processSectionFields = _fileParser.GetSectionFields();
        }
    }
}