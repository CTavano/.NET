///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///
/// Molecular Weight Calculator 
/// Curtis J Kordyban : https://github.com/kordytech/Molar-Mass-Calculator
/// Christofer Tavano
/// Created: October 2, 2020
/// 
/// Periodic Table from : https://ptable.com/?lang=en#Properties
/// 
/// About : -Calculates molar mass of inputed chemical formula
///         -Validates textbox input ensuring only elements are entered
///         -Uses regular expressions to evaluate formula for existing elements
///         -LINQ used for displaying periodic table in gridview
///         -Added text file resource for elements 
///         
///  *Support Class* - Element.cs 
///  
/// Updates: 
///         -Curtis  : Built user interface
///         -Chris   : Found and added periodic table as textfile into resources
///         -Mutually: Worked out getting data from textfile into something we can work with
///         -Chris   : Wrote sorting LINQ statements and wired to button events
///         -Chris   : Wrote Element Class
///         -Curtis  : Wrote regular expression textbox change event
///         -Curtis  : Moved sorting LINQ statements into single event and wrote switch statement 
///                    Added Gridview cell formatting event 
///         
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions; // Needed for REGEX

namespace Molar_Mass_Calculator
{
    public partial class Main : Form{
        // Global variables 
        private readonly Dictionary<string, Element> _PeriodicTable = Element.CreateTableOfElements();
        public BindingSource _bs = new BindingSource();

        public Main(){
            // Initialization 
            InitializeComponent();
            Shown += Form1_Shown;
            UI_gv_Periodic_Table.DataSource = _bs;

            // Event Handlers
            UI_btn_char_Symbols.Click   += Btn_Sort_Click_Event;
            UI_btn_sort_by_Atomic.Click += Btn_Sort_Click_Event;
            UI_btn_sort_by_Name.Click   += Btn_Sort_Click_Event;
            UI_gv_Periodic_Table.CellFormatting += UI_gv_Periodic_Table_CellFormatting;
            UI_tBox_Formula.TextChanged += UI_tBox_Formula_TextChanged;
        }

        private void Form1_Shown(object sender, EventArgs e){

            Location = new Point(150, 150);                     // Set forms location
            UI_tBox_Molar_Mass_Output.ForeColor = Color.Black;  // Set color to black on load

            // Displaying periodic table in gridview
            _bs.DataSource = from i in _PeriodicTable
                             orderby i.Value.AtomNum
                             select new { i.Value.AtomNum, i.Value.Name, i.Value.Symbol, i.Value.Mass };

        }

        // Event - ButtonClicked - Sorting periodic table based on which button pressed
        private void Btn_Sort_Click_Event(object sender, EventArgs e){
            // Clearing input textbox
            UI_tBox_Formula.Clear();

            Button b = (Button)sender;
            switch (b.Name){
                // Sorting by name of element
                case "UI_btn_sort_by_Name":
                    _bs.DataSource = from i in _PeriodicTable
                                     orderby i.Value.Name
                                     select new { i.Value.AtomNum, i.Value.Name, i.Value.Symbol, i.Value.Mass };
                    break;

                // Sorting by single character symbols
                case "UI_btn_char_Symbols":
                    _bs.DataSource = from i in _PeriodicTable
                                     where i.Key.Length == 1
                                     orderby i.Key
                                     select new { i.Value.AtomNum, i.Value.Name, i.Value.Symbol, i.Value.Mass };
                    break;

                // Sorting by Atomic number of element
                case "UI_btn_sort_by_Atomic":
                    _bs.DataSource = from i in _PeriodicTable
                                     orderby i.Value.AtomNum
                                     select new { i.Value.AtomNum, i.Value.Name, i.Value.Symbol, i.Value.Mass };
                    break;
            }
        }

        // Event - CellFormatting - Formatting gridview cells to ensure no null space 
        private void UI_gv_Periodic_Table_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e){
            // Taking the last column of the binding source and expanding it to fill the remaining null space in the gv
            UI_gv_Periodic_Table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            UI_gv_Periodic_Table.Columns[UI_gv_Periodic_Table.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        // Event - TextChanged - Using regex to determine elements entered in textbox
        private void UI_tBox_Formula_TextChanged(object sender, EventArgs e){
            // Clearing gridview and MolarMass output
            _bs.DataSource = null;
            UI_tBox_Molar_Mass_Output.Text = $"0";
            UI_tBox_Molar_Mass_Output.ForeColor = Color.Black;

            // Get the textbox input
            string textboxInput = $"{ UI_tBox_Formula.Text}";

            // Defining the regex:
            //    What we are looking for is input that could be: N, Na, Na3 (Nitrogen, Sodium or Sodium x 3)
            //    -First char must be capitalized -> [A-Z]. Second char must be lowercase -> [a-z], followed by ? making it optional input 
            //    -For the count [0-9]*, * indicates any length of digit and sticking a ? on the outside will again make it an optional input 
            //    -To add a prefix to our grouping criteria (?'NameOfPrefix'[criteria]) otherwise you can do new Regex(@"[Criteria]") if you don't need one            
            Regex reg = new Regex(@"(?'element'[A-Z][a-z]?)(?'count'[0-9]*)?");

            // Using dictionary to store regex results
            //  -(Key = regex groupings / elements, Value = # of occurrences)
            Dictionary<string, int> results = new Dictionary<string, int>();

            int elementsAdded = 0; // count of elements / regex groupings in textbox

            // If we have a grouping in the text box that satisfies regex expression
            if (reg.IsMatch(textboxInput)){
                // Grabbing copy of regex match collection from user text input
                MatchCollection matchCollection = reg.Matches(textboxInput);

                // Itterate through collection and evaluate
                foreach (Match grouping in matchCollection){
                    // Setting count = 1 since we will always be adding 1 to our value regardless
                    int count = 1;

                    // Check if a count was applied by user and use that value instead
                    if (grouping.Groups["count"].Length >= 1)
                        int.TryParse(grouping.Groups["count"].ToString(), out count); // converting string -> int 

                    // Can now proceed to add groupings into the dictionary 
                    if (!results.ContainsKey(grouping.Groups["element"].ToString()))
                        results.Add(grouping.Groups["element"].ToString(), count);
                    else
                        results[grouping.Groups["element"].ToString()] += count;

                    // Check if the 'element' parsed out exists in periodic table
                    //   -If the element exists -> add the length of that string to the count with the groupings count 
                    if (_PeriodicTable.ContainsKey(grouping.Groups["element"].ToString()))
                        elementsAdded += grouping.Groups["element"].Length + grouping.Groups["count"].Length;

                }

                // Now we have a dictionary of possible elements and a count of how many times they appear in the textbox...perfect
                // Can now do a join on results and the _PeriodicTable on the element symbol to pull elements mass 
                var joinedQuery = from z in results
                                  join i in _PeriodicTable on
                                  z.Key equals i.Value.Symbol
                                  orderby z.Value
                                  orderby i.Value.Mass
                                  select new { Element = i.Value.Name, Count = z.Value, i.Value.Mass, TotalMass = i.Value.Mass * z.Value };
                _bs.DataSource = joinedQuery;

                // Calculate molar mass of formula by rolling up sums
                double sum = 0;
                (from i in joinedQuery select i.TotalMass).ToList().ForEach(x => sum += x);
                if (sum > 0)
                    UI_tBox_Molar_Mass_Output.Text = $"{sum} g/mol";

                // Indicate to user valid formula by green text, red for error
                UI_tBox_Molar_Mass_Output.ForeColor = elementsAdded.Equals(UI_tBox_Formula.Text.Length) ? Color.Green : Color.Red;

            }

            // Repopulate gridview with elements if textbox is empty
            if (UI_tBox_Formula.Text.Length.Equals(0)){
                _bs.DataSource = from i in _PeriodicTable
                                 orderby i.Value.AtomNum
                                 select new { i.Value.AtomNum, i.Value.Name, i.Value.Symbol, i.Value.Mass };
            }
        }
    }
}
