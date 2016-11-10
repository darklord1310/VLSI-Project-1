using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace proj1
{
    class NetsOperation
    {
        private string NetsPath = "";
        string[] lines;
        private int totalNumberOfNets = 0;
        int totalInputPins = 0;
        int totalOutputPins = 0;
        List<string> maxNetDegree = new List<string>();
        List<Net> Nets = new List<Net>();
        SortedDictionary<int, int> NetDegreeCountPair = new SortedDictionary<int, int>();

        public NetsOperation()
        {

        }

        private void resetAllParams()
        {
            totalNumberOfNets = 0;
            totalInputPins = 0;
            totalOutputPins = 0;
            maxNetDegree.Clear();
            NetDegreeCountPair.Clear();
            Nets.Clear();
        }

        public void readFileFromPath(string path)
        {
            if (NetsPath != path)
            {
                this.NetsPath = path;
                resetAllParams();

                try
                {
                    lines = File.ReadAllLines(@path, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public NetsOperation(string path)
        {
            readFileFromPath(path);
        }

        private void NetOperations()
        {
            string prevNetName = "empty net";
            string netName = "";
            int netDegree = 0;
            int largestNetDegree = 0;
            double coordx = 0;
            double coordy = 0;
            string fanoutName = "";
            string fanoutType = "";
            Net net = new Net(netName,netDegree);

            foreach (string line in lines)
            {
                Regex noOfNetsPattern = new Regex(@"^NumNets :\s+(\d+)");
                Match noOfNetsMatch = noOfNetsPattern.Match(line);

                Regex netDegreePattern = new Regex(@"^NetDegree :\s+(\d+)\s+(\w+)");
                Match netDegreeMatch = netDegreePattern.Match(line);

                Regex netInfo = new Regex(@"\s*(o\d+)\s+(\w)\s+:\s+(\W*\d+.\d+)\s+(\W*\d+.\d+)");
                Match netInfoMatch = netInfo.Match(line);

                if (noOfNetsMatch.Success)
                    totalNumberOfNets = Convert.ToInt32(noOfNetsMatch.Groups[1].Value);

                if (netDegreeMatch.Success)
                {
                    netDegree = Convert.ToInt32(netDegreeMatch.Groups[1].Value);
                    netName = netDegreeMatch.Groups[2].Value;
                    if (NetDegreeCountPair.ContainsKey(netDegree))
                        NetDegreeCountPair[netDegree] = (int)NetDegreeCountPair[netDegree] + 1;
                    else
                        NetDegreeCountPair.Add(netDegree, 1);

                    if (netDegree == largestNetDegree)
                        maxNetDegree.Add(netDegree + netName);

                    if (netDegree > largestNetDegree)
                    {
                        largestNetDegree = netDegree;
                        maxNetDegree.Clear();
                        maxNetDegree.Add(netDegree + " " + netName);
                    }

                    net = new Net(netName, netDegree);
                }

                if (netInfoMatch.Success)
                {
                    fanoutName = netInfoMatch.Groups[1].Value;
                    fanoutType = netInfoMatch.Groups[2].Value;
                    if (netInfoMatch.Groups[3].Value.StartsWith("-") == true)
                        coordx = Convert.ToDouble(netInfoMatch.Groups[3].Value.Substring(1).TrimEnd('0')) * (-1);
                    else
                        coordx = Convert.ToDouble(netInfoMatch.Groups[3].Value.TrimEnd('0'));

                    if (netInfoMatch.Groups[4].Value.StartsWith("-") == true)
                        coordy = Convert.ToDouble(netInfoMatch.Groups[4].Value.Substring(1).TrimEnd('0')) * (-1);
                    else
                        coordy = Convert.ToDouble(netInfoMatch.Groups[4].Value.TrimEnd('0'));

                    NetFanout fanout = new NetFanout(fanoutName, coordx, coordy);
                    if (fanoutType.Equals("I"))
                    {
                        net.addInputNodes(fanout);
                        totalInputPins++;
                    }
                    else
                    {
                        net.setOutputNodes(fanout);
                        totalOutputPins++;
                    }
                }
            }
        }

        public string getMaxDegreeNetPair()
        {
            StringBuilder str = new StringBuilder();
            if (maxNetDegree.Count == 0)
                NetOperations();
            
            foreach (string val in maxNetDegree)
            {
                str.AppendLine(val);
            }

            return str.ToString();
        }

        public int getTotalInputPins()
        {
            if (totalInputPins == 0)
                NetOperations();
            return totalInputPins;
        }

        public int getTotalOutputPins()
        {
            if (totalOutputPins == 0)
                NetOperations();
            return totalOutputPins;
        }

        public int getTotalNets()
        {
            if (totalNumberOfNets == 0)
                NetOperations();
            return totalNumberOfNets;
        }

        public List<Net> getAllNetsInfo()
        {
            return Nets;
        }
        public string displayHistogramOfConnectivity()
        {
            string output = "";
            if (NetDegreeCountPair.Count != 0)
            {
                output = "Net Degree\t|\tNumber of Nets\n";

                foreach (KeyValuePair<int, int> p in NetDegreeCountPair)
                {
                    output += "\t" + p.Key.ToString() + "\t \t" + p.Value.ToString() + "\n";
                } 
            }
            return output;
        }
    }
}
