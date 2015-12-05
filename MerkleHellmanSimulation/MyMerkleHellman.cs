using System;
using System.Collections.Generic;
using System.Linq;

namespace MerkleHellmanSimulation
{
    public class MyMerkleHellman
    {

        public static List<int> CalculateHardknapsacks(List<int> simlpeknapsacks, int w, int n)
        {
            return simlpeknapsacks.Select(h => (h*w) % n).ToList();
        }

        public static List<int> EncruptMessage(List<int> hard, string message)
        {
            var m = hard.Count;
            var list = Split(message.ToList(), m);
            var result=new List<int>();
            foreach (var ele in list)
            {
                result.Add(hard.Select((t, i) => ele[i]*t).Sum());
            }
            return result;
            
        }

        public static List<List<int>> Split(List<char> source,int sub)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / sub)
                .Select(x => x.Select(v => Convert.ToInt32(v.Value.ToString())).ToList())
                .ToList();
        }

    }
}
