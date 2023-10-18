using System;
using System.Collections.Generic;

namespace HW2
{
    class IntFrequencyCalculator
    {
        public int[] arg_array;
        public string col_name;
        public Dictionary<int, int> absolute_frequency; 
        public Dictionary<int, double> relative_frequency; 
        public Dictionary<int, double> percentage_frequency; 

        public IntFrequencyCalculator(int[] arg_array, string col_name){
            this.arg_array = arg_array;
            this.col_name = col_name;

            compute_af();
            compute_rf();
            compute_pf();
        }

        public void compute_af(){
            absolute_frequency = new Dictionary<int, int>();
            foreach (int item in arg_array) {
                if (absolute_frequency.ContainsKey(item)) { absolute_frequency[item]++; }
                
                else { absolute_frequency[item] = 1; }
            }
        }

        public void compute_rf()
        {
            relative_frequency = new Dictionary<int, double>();
            int total = arg_array.Length;
            foreach (var kvp in absolute_frequency) {
                relative_frequency[kvp.Key] = Math.Round((double)kvp.Value / total, 4);
            }
        }

        public void compute_pf()
        {
            percentage_frequency = new Dictionary<int, double>();
            int total = arg_array.Length;
            foreach (var kvp in relative_frequency)
            {
                percentage_frequency[kvp.Key] = Math.Round(kvp.Value * 100, 2);
            }
        }

        public void show(string type)
        {
            Dictionary<int, double> frequencyDict = null;

            if (type == "af")
            {
                Console.WriteLine($"{col_name} absolute frequency");
                frequencyDict = absolute_frequency.ToDictionary(kvp => kvp.Key, kvp => (double)kvp.Value);
            }
            else if (type == "rf")
            {
                Console.WriteLine($"{col_name} relative frequency");
                frequencyDict = relative_frequency;
            }
            else if (type == "pf")
            {
                Console.WriteLine($"{col_name} percentage frequency");
                frequencyDict = percentage_frequency;
            }

            if (frequencyDict != null)
            {
                Console.WriteLine("+----------------------+");
                Console.WriteLine("| Value        Frequency|");
                Console.WriteLine("+----------------------+");

                foreach (var kvp in frequencyDict)
                {
                    Console.WriteLine($"| {kvp.Key,-12} {kvp.Value,9}{(type == "pf" ? "% " : "  ")}|");
                }

                Console.WriteLine("+----------------------+");
                Console.WriteLine(" "); // To leave some space from one table and the next one
                Console.WriteLine(" ");
                Console.WriteLine(" ");
            }
        }
    }
}
