using System;
using System.Collections.Generic;

namespace HW2
{
    class StrFrequencyCalculator
    {
        public string[] arg_array;
        public string col_name;
        public Dictionary<string, int> absolute_frequency;
        public Dictionary<string, double> relative_frequency;
        public Dictionary<string, double> percentage_frequency;

        public StrFrequencyCalculator(string[] arg_array, string col_name) {
            this.arg_array = arg_array;
            this.col_name = col_name;

            // The frequencies are computed when constructor is invoked
            compute_af();
            compute_rf();
            compute_pf();
        }

        public void compute_af(){
            absolute_frequency = new Dictionary<string, int>();
            foreach (string item in arg_array) {

                if (absolute_frequency.ContainsKey(item)) { absolute_frequency[item]++; }

                else { absolute_frequency[item] = 1; }
            }
        }

        public void compute_rf(){
            relative_frequency = new Dictionary<string, double>();
            int total = arg_array.Length;
            foreach (var kvp in absolute_frequency) {
                // Relative frequency is rounded up to the forth digit after decimal
                relative_frequency[kvp.Key] = Math.Round((double)kvp.Value / total, 4);
            }
        }

        public void compute_pf() {
            percentage_frequency = new Dictionary<string, double>();
            int total = arg_array.Length;
            foreach (var kvp in relative_frequency) {
                // Percentage frequency is instead rounded up to the second, as it must be first multiplied by 100
                percentage_frequency[kvp.Key] = Math.Round(kvp.Value * 100, 2);
            }
        }

        public void show(string type) {
            Dictionary<string, double> frequencyDict = null;

            if (type == "af") {
                Console.WriteLine($"{col_name} absolute frequency");
                frequencyDict = absolute_frequency.ToDictionary(kvp => kvp.Key, kvp => (double)kvp.Value);
            }
            else if (type == "rf") {
                Console.WriteLine($"{col_name} relative frequency");
                frequencyDict = relative_frequency;
            }
            else if (type == "pf") {
                Console.WriteLine($"{col_name} percentage frequency");
                frequencyDict = percentage_frequency;
            }

            else {Console.WriteLine("Insert valid frequency argument");}

            if (frequencyDict != null)
            {
                Console.WriteLine("+----------------------+");
                Console.WriteLine("| Value   |    Frequency|");
                Console.WriteLine("+----------------------+");

                foreach (var kvp in frequencyDict) {
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
