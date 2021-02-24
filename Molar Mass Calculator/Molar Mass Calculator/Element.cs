///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///
/// Element Support class 
///    
/// Periodic Table from : https://ptable.com/?lang=en#Properties
///
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using System.Linq;

namespace Molar_Mass_Calculator
{
    /// <summary>
    /// Class containing the elements of the periodic table
    /// </summary>
    public class Element{
        // Properties
        public int    AtomNum { get; private set; } // Atomic Number     - eg : 2
        public string Name    { get; private set; } // Name of Element   - eg : Helium
        public string Symbol  { get; private set; } // Symbol of Element - eg : He
        public double Mass    { get; private set; } // Mass of Element   - eg : 4.0026

        /// <summary>
        /// Constructor for Elements
        /// </summary>
        /// <param name="number">Element Number</param>
        /// <param name="name">Element Name</param>
        /// <param name="symbol">Element Symbol</param>
        /// <param name="mass">Element mass</param>
        private Element(int number, string name, string symbol, double mass){
            AtomNum = number;
            Name = name;
            Symbol = symbol;
            Mass = mass;
        }

        /// <summary>
        /// Generates Periodic Table of Elements from textfile resource
        /// </summary>
        /// <returns>Dictionary - Key = Element Symbol, Value = Element</returns>
        public static Dictionary<string, Element> CreateTableOfElements(){
            // Retrieve entire periodic table textfile as a single string
            string textfile = Properties.Resources.Periodic_Table_Text;

            // Splitting textfile into respective lines and generating a list from them
            var textfileLineList = from str in textfile.Split('\r', '\n') where !string.IsNullOrWhiteSpace(str) select str.Trim();

            // Spitting up each line on commas and assigning names
            var tempPeriodicTable = from i in textfileLineList
                                    select new{
                                        AtomNum = int.Parse(i.Split(',')[0]),
                                        Name = i.Split(',')[1],
                                        Symbol = i.Split(',')[2],
                                        AtomicMass = double.Parse(i.Split(',')[3])
                                    };

            // Declaring final dictionary that will be returned
            Dictionary<string, Element> periodicTable = new Dictionary<string, Element>();

            // Transferring data from temp table to final periodic table 
            foreach (var element in tempPeriodicTable){
                Element temp = new Element(element.AtomNum, element.Name, element.Symbol, element.AtomicMass);
                periodicTable.Add(element.Symbol, temp);
            }

            // Periodic table of elements is now complete - Good to return
            return periodicTable;
        }
    }
}
