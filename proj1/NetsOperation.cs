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
        int netIndicator = 0;
        int totalInputPins = 0;
        int totalOutputPins = 0;
        List<Net> Nets = new List<Net>();
        NetFanout outputNodes;
        SortedDictionary<int, int> NetDegreeCountPair = new SortedDictionary<int, int>();
        public NetsOperation(string path)
        {
            this.NetsPath = path;
            try
            {
                lines = File.ReadAllLines(@NetsPath, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void NetOperations()
        {
            string netName = "";
            int netDegree = 0;
            List<NetFanout> inputNodes = new List<NetFanout>();

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

                if (netIndicator == 0)
                {
                    if (netDegreeMatch.Success)
                    {
                        netIndicator = 1;
                        netDegree = Convert.ToInt32(netDegreeMatch.Groups[1].Value);
                        netName = netDegreeMatch.Groups[2].Value;
                        if (NetDegreeCountPair.ContainsKey(netDegree))
                        {
                            NetDegreeCountPair[netDegree] = (int)NetDegreeCountPair[netDegree] + 1;
                        }
                        else
                            NetDegreeCountPair.Add(netDegree,1);
                    }
                }
                else if (netIndicator == 1)
                {
                    double coordx = 0;
                    double coordy = 0;
                    string fanoutName = "";
                    string fanoutType = "";

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
                            inputNodes.Add(fanout);
                            totalInputPins++;
                        }
                        else
                        {
                            outputNodes = fanout;
                            totalOutputPins++;
                        }    
                    }
                    else
                    {
                        Net net = new Net(netName, netDegree, inputNodes, outputNodes);
                        Nets.Add(net);
                        netIndicator = 0;
                    }
                }
            }
        }

        public List<Net> getMaxDegreeNetPair()
        {
            int largestDegree = 0;
            List<Net> maxNetDegree = new List<Net>();

            if (totalNumberOfNets == 0)
                NetOperations();

            foreach (Net n in Nets)
            {
                if (n.getNetDegree() > largestDegree)
                {
                    largestDegree = n.getNetDegree();
                    maxNetDegree.Clear();
                    maxNetDegree.Add(n);
                }
            }
            return maxNetDegree;
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

            output = "Net Degree\t|\tNumber of Nets\n";

            foreach (KeyValuePair<int, int> p in NetDegreeCountPair)
            {
                output += "\t" + p.Key.ToString() + "\t \t\t\t" + p.Value.ToString() + "\n";
            }
            return output;
        }
    }
}
