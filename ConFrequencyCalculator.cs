using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace HW2{

    class ConFrequencyCalculator {
        public float[] arg_array;
        public string col_name;
        public int intervalNo;
        private float offset;
        public Dictionary<Tuple<float,float>, int> absolute_frequency; 
        public Dictionary<Tuple<float,float>, double> relative_frequency; 
        public Dictionary<Tuple<float,float>, double> percentage_frequency;

        public ConFrequencyCalculator(float[] arg_array, string col_name, int intervalNo){
            this.arg_array = arg_array;
            this.col_name = col_name;
            this.intervalNo = intervalNo;

            compute_af();
            compute_rf();
            compute_pf();
        }

        public void compute_af(){
            // this is the empty dictionary
            absolute_frequency = new Dictionary<Tuple<float,float>, int>();
            // we take this time the maximum and minimum values from arg_array
            // Then, we take the distance and we divide it by the number of intervals
            // Finally, in offset we have the (fixed) range for each interval
            
            // offset = (arg_array.Max() - arg_array.Min())/intervalNo [EX1]
            offset = 1.0F/ intervalNo; // [EX2]

            //float x = arg_array.Min(); [EX1]
            //float y = arg_array.Min() + offset; [EX1]
            float x = 0; //[EX2]
            float y = offset; //[EX2]

            for (int i = 1; i <= intervalNo; i++){
                absolute_frequency[new Tuple <float,float> (x,y)] = 0;
                x = x + offset; 
                y = y + offset;
            }

            foreach (float item in arg_array){
                foreach (KeyValuePair<Tuple<float,float>, int> entry in absolute_frequency){
                    if (item >= entry.Key.Item1 && item <= entry.Key.Item2) {absolute_frequency[entry.Key]++; }
                }
            }
        }

        public void compute_rf(){
            relative_frequency = new Dictionary<Tuple<float, float>, double>();
            int total = arg_array.Length;

            foreach (var kvp in absolute_frequency){
                relative_frequency[kvp.Key] = Math.Round((double)kvp.Value / total, 4);
            }
        }

        public void compute_pf(){
            percentage_frequency = new Dictionary<Tuple<float, float>, double>();
            int total = arg_array.Length;

            foreach (var kvp in relative_frequency){
                percentage_frequency[kvp.Key] = Math.Round(kvp.Value * 100, 2);
            }
        }

        public void show(string type){
            Dictionary<Tuple<float, float>, double> frequencyDict = null;

            if (type == "af")
            {
                Console.WriteLine($"{col_name} interval  absolute frequency");
                frequencyDict = absolute_frequency.ToDictionary(kvp => kvp.Key, kvp => (double)kvp.Value);
            }
            else if (type == "rf")
            {
                Console.WriteLine($"{col_name} interval relative frequency");
                frequencyDict = relative_frequency;
            }
            else if (type == "pf")
            {
                Console.WriteLine($"{col_name} interval percentage frequency");
                frequencyDict = percentage_frequency;
            }

            if (frequencyDict != null)
            {
                Console.WriteLine("+----------------------+");
                Console.WriteLine("| Interval value        Frequency|");
                Console.WriteLine("+----------------------+");

                foreach (var kvp in frequencyDict)
                {
                    Console.WriteLine($"| [{Math.Round(kvp.Key.Item1,2),-3}-{Math.Round(kvp.Key.Item2,2),-3}] {kvp.Value,9}{(type == "pf" ? "% " : "  ")}|");
                }

                Console.WriteLine("+----------------------+");
                Console.WriteLine(" "); // To leave some space from one table and the next one
                Console.WriteLine(" ");
                Console.WriteLine(" ");
            }
        }
    }
}