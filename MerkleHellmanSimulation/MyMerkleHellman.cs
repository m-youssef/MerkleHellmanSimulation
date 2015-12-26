using System;
using System.Collections.Generic;
using System.Linq;

namespace MerkleHellmanSimulation
{
    public class MyMerkleHellman
    {
        private readonly Form1 _myFormControl;

        public MyMerkleHellman(Form1 myForm)
        {
            _myFormControl = myForm;

        }

        public List<int> CalculateHardknapsacks(List<int> simlpeknapsacks, int w, int n)
        {
            PrintToScreen("**** Calculating Hard knapsacks ****");
            var res=new List<int>();
            for (int i = 0; i < simlpeknapsacks.Count; i++)
            {
                var h = (simlpeknapsacks[i]*w)%n;
                PrintToScreen(simlpeknapsacks[i] + " * " + w + " mod " + n + " = " + h);
                res.Add(h);
            }
            PrintToScreen("H = ["+string.Join(",", res)+"]");
            PrintToScreen("**************************************");
            return res;
           // return simlpeknapsacks.Select(h => (h * w) % n).ToList();
        }

        public List<int> EncryptMessage(List<int> hard, string message)
        {
            var m = hard.Count;
            var list = Split(message.ToList(), m);
            var list1 = CheckMessageLength(list, hard.Count);
            var result = new List<int>();
            foreach (var ele in list)
            {
                result.Add(hard.Select((t, i) => ele[i] * t).Sum());
            }
            return result;

        }

        // W-1 = (w^n-2)%n
        public int CalculateWinvers(int w, int n)
        {
            PrintToScreen("**** Calculating W Inverse ****");
            PrintToScreen("**** W-1 = (w^n-2)%n ****");
            var wi= (int)(Math.Pow(w, n - 2) % n);
            PrintToScreen(" W-1 = " + wi);
            PrintToScreen("**************************************");
            return wi;
        }

        private void PrintToScreen(string text)
        {
            _myFormControl.Invoke(_myFormControl.UpdateTextBox, text); // updates textbox1 on winForm

        }

        private List<List<int>> CheckMessageLength(List<List<int>> messge, int size)
        {
            foreach (var m in messge.Where(m => size != m.Count))
            {
                for (var i = size - m.Count; i < size; i++)
                {
                    m.Insert(i, 0);
                }
            }
            return messge;
        }

        public List<List<int>> DecryptMessage(List<int> message, int wi, int n, List<int> s)
        {
            var res = new List<List<int>>();
            foreach (var num in message)
            {
                var temp = (num * wi) % 17;
                var sumList = FindSum(s, temp);
                res.Add(MapMessageList(s, sumList));
            }

            return res;
        }

        private List<List<int>> Split(List<char> source, int sub)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / sub)
                .Select(x => x.Select(v => Convert.ToInt32(v.Value.ToString())).ToList())
                .ToList();
        }

        private List<int> MapMessageList(List<int> simple, List<int> sumList)
        {
            var res = new List<int>();
            foreach (var num in simple)
            {
                //var temp = sumList.Any(s => s == num);
                res.Add(sumList.Any(s => s == num) ? 1 : 0);
            }
            return res;
        }
        private List<int> FindSum(List<int> simple, int targetSum, int index = 0)
        {

            for (var i = index; i < simple.Count; i++)
            {
                int remainder = targetSum - simple[i];
                // if the current number is too big for the target, skip
                if (remainder < 0)
                    continue;
                // if the current number is a solution, return a list with it
                if (remainder == 0)
                    return new List<int>() { simple[i] };

                // otherwise try to find a sum for the remainder later in the list
                var s = FindSum(simple, remainder, i + 1);

                // if no number was returned, we couldn't find a solution, so skip
                if (s.Count == 0)
                    continue;

                // otherwise we found a solution, so add our current number to it
                // and return the result
                s.Insert(0, simple[i]);
                return s;
            }

            // if we end up here, we checked all the numbers in the list and
            // found no solution
            return new List<int>();
        }

    }


}
