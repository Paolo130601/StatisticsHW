using System;
using System.Collections.Generic;
using System.Linq;

namespace HW4{
class JointDistribution{

    private List<float[]> DS; 
    private int[] I;
    public Dictionary<(float, float)[], int> distribution;

    public JointDistribution(List<float[]> Dataset,int[] Intervals_no){
        DS = Dataset;
        I = Intervals_no;

        //We first create the intervals
        List<(float, float)[]> L1 = CreateIntervals(DS,I);
        //We create the clusters
        List<(float, float)[]> L2 = GetCombinations(L1);
        //And we compute the empiritcal Joint distribution
        Dictionary<(float, float)[], int> distribution = CalculateJointDistribution(L2,DS);

        Console.WriteLine("{0,-30} {1,-30}", "Cluster", "Empirical Joint frequency");
        foreach (var kvp in distribution)
        {
            string cluster = string.Join(" x ", Math.Round(kvp.Key.Select(t => $"({t.Item1}, {t.Item2})"),2));
            Console.WriteLine("{0,-30} {1,-30}", cluster, kvp.Value);
        }

    }

    //The function below creates the intervals according to the numbers specified in I
    public List<(float, float)[]> CreateIntervals(List<float[]> dataset, int[] I)
    {
        List<(float, float)[]> result = new List<(float, float)[]>();

        int numColumns = dataset[0].Length;

        for (int j = 0; j < numColumns; j++)
        {
            float minValue = float.MaxValue;
            float maxValue = float.MinValue;

            // Find the min and max value in column j
            foreach (var sample in dataset)
            {
                minValue = Math.Min(minValue, sample[j]);
                maxValue = Math.Max(maxValue, sample[j]);
            }

            float intervalSize = (float) Math.Round((maxValue - minValue) / I[j],2);

            // Create intervals for column j
            (float, float)[] intervals = new (float, float)[I[j]];
            for (int k = 0; k < I[j]; k++)
            {
                intervals[k] = (minValue + k * intervalSize, minValue + (k + 1) * intervalSize);
            }

            result.Add(intervals);
        }

        return result;
    }

    //The function below builds the clusters instead
    static List<(float, float)[]> GetCombinations(List<(float, float)[]> L1)
    {
        if (L1.Count == 0)
        {
            return new List<(float, float)[]>() { new (float, float)[0] };
        }

        var restCombinations = GetCombinations(L1.Skip(1).ToList());
        var currentArray = L1.First();

        var result = new List<(float, float)[]>();

        foreach (var item in currentArray)
        {
            foreach (var combination in restCombinations)
            {
                var newArray = new List<(float, float)>() { item };
                newArray.AddRange(combination);

                result.Add(newArray.ToArray());
            }
        }

        return result;
    }

    //Finally, this last function computes the joint distribution
    static Dictionary<(float, float)[], int> CalculateJointDistribution(List<(float, float)[]> L2, List<float[]> dataset)
    {
        Dictionary<(float, float)[], int> jointDistribution = new Dictionary<(float, float)[], int>();

        foreach (var combination in L2)
        {
            jointDistribution[combination] = 0;
        }

        foreach (var sample in dataset)
        {
            foreach (var combination in L2)
            {
                bool isInsideAllIntervals = true;

                for (int i = 0; i < sample.Length; i++)
                {
                    if (sample[i] < combination[i].Item1 || sample[i] > combination[i].Item2)
                    {
                        isInsideAllIntervals = false;
                        break;
                    }
                }

                if (isInsideAllIntervals)
                {
                    jointDistribution[combination]++;
                }
            }
        }

        return jointDistribution;
    }

}
}