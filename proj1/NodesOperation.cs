using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace proj1
{
    class NodesOperation
    {
        private int terminalNodeCount = 0;      // terminal node count
        private int totalNodeCount = 0;     // total node count
        private int nonTerminalCount = 0;   // non terminal node count
        public int largestTermArea = 0;
        public int smallestTermArea = 0;
        public int largestNonTermArea = 0;
        public int smallestNonTermArea = 0;
        private string nodesPath = "";
        private string[] lines;
        private List<string> largestNonTermNode = new List<string>();
        private List<string> smallestNonTermNode = new List<string>();
        private List<string> largestTermNode = new List<string>();
        private List<string> smallestTermNode = new List<string>();
        private int totalWidthOfNonTermNodes = 0;
        private int totalHeightOfNonTermNodes = 0;
        private List<Node> Nodes = new List<Node>();

        public NodesOperation()
        {
        }

        public NodesOperation(string path)
        {
            readFileFromPath(path);
        }

        private void resetAllParams()
        {
            terminalNodeCount = 0;     
            totalNodeCount = 0;     
            nonTerminalCount = 0;   
            largestTermArea = 0;
            smallestTermArea = 0;
            largestNonTermArea = 0;
            smallestNonTermArea = 0;

            largestNonTermNode.Clear();
            smallestNonTermNode.Clear();
            largestTermNode.Clear();
            smallestTermNode.Clear();
            totalWidthOfNonTermNodes = 0;
            totalHeightOfNonTermNodes = 0;
            Nodes.Clear();
        }

        public void readFileFromPath(string path)
        {
            if (nodesPath != path)
            {
                this.nodesPath = path;
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

        private void findTerminalOrNonTerminalNodes()
        {           
            foreach (string line in lines)
            {
                Regex termNodesPattern = new Regex(@"^NumTerminals :\s+(\d+)");
                Regex totalNodesPattern = new Regex(@"^NumNodes :\s+(\d+)");
                Regex terminalNodeInfo = new Regex(@"(o\d+)\s+(\d+)\s+(\d+)\s+(\w+)");
                Regex nonTerminalNodeInfo = new Regex(@"(o\d+)\s+(\d+)\s+(\d+)$");
                Match numNodesMatch = termNodesPattern.Match(line);
                Match totalNodesMatch = totalNodesPattern.Match(line);
                Match terminalInfomatch = terminalNodeInfo.Match(line);
                Match nonTerminalInfomatch = nonTerminalNodeInfo.Match(line);

                if (numNodesMatch.Success)
                    terminalNodeCount = Convert.ToInt32(numNodesMatch.Groups[1].Value);

                if (totalNodesMatch.Success)
                    totalNodeCount = Convert.ToInt32(totalNodesMatch.Groups[1].Value);
                
                if (nonTerminalInfomatch.Success)
                {
                    string name = nonTerminalInfomatch.Groups[1].Value;
                    int w = Convert.ToInt16(nonTerminalInfomatch.Groups[2].Value);
                    int h = Convert.ToInt16(nonTerminalInfomatch.Groups[3].Value);
                    string type = "Non Terminal";
                    int area = w * h;

                    if (area == largestNonTermArea)
                        largestNonTermNode.Add(name);

                    if (area > largestNonTermArea)
                    {
                        largestNonTermNode.Clear();
                        largestNonTermNode.Add(name);
                        largestNonTermArea = area;
                    }

                    if (smallestNonTermArea == 0)
                        smallestNonTermArea = area;

                    if (smallestNonTermArea == area)
                        smallestNonTermNode.Add(name);

                    if (area < smallestNonTermArea)
                    {
                        smallestNonTermNode.Clear();
                        smallestNonTermNode.Add(name);
                        smallestNonTermArea = area;
                    }

                    Node newNode = new Node(name, type, w, h);
                    Nodes.Add(newNode);
                    totalWidthOfNonTermNodes += w;
                    totalHeightOfNonTermNodes += h;
                }

                if (terminalInfomatch.Success)
                {
                    string name = terminalInfomatch.Groups[1].Value;
                    int w = Convert.ToInt16(terminalInfomatch.Groups[2].Value);
                    int h = Convert.ToInt16(terminalInfomatch.Groups[3].Value);
                    string type = terminalInfomatch.Groups[4].Value;
                    int area = w * h;

                    if (area == largestTermArea)
                        largestTermNode.Add(name);

                    if (area > largestTermArea)
                    {
                        largestTermNode.Clear();
                        largestTermNode.Add(name);
                        largestTermArea = area;
                    }

                    if (smallestTermArea == 0)
                        smallestTermArea = area;

                    if (smallestTermArea == area)
                        smallestTermNode.Add(name);

                    if (area < smallestTermArea)
                    {
                        smallestTermNode.Clear();
                        smallestTermNode.Add(name);
                        smallestTermArea = area;
                    }

                    Node newNode = new Node(name,type,w,h);
                    Nodes.Add(newNode);
                }
            }
            nonTerminalCount = totalNodeCount - terminalNodeCount;
        }

        public List<string> getSmallestTerminalNode()
        {
            if (smallestTermNode.Count == 0)
                findTerminalOrNonTerminalNodes();

            return smallestTermNode;
        }

        public List<string> getSmallestNonTerminalNode()
        {
            if (smallestNonTermNode.Count == 0)
                findTerminalOrNonTerminalNodes();

            return smallestNonTermNode;
        }


        public List<string> getLargestTerminalNode()
        {
            if (largestTermNode.Count == 0)
                findTerminalOrNonTerminalNodes();

            return largestTermNode;
        }

        public List<string> getLargestNonTerminalNode()
        {
            if (largestNonTermNode.Count == 0)
                findTerminalOrNonTerminalNodes();

            return largestNonTermNode;            
        }

        public int getTotalWidthNonTermNodes()
        {
            if (totalWidthOfNonTermNodes == 0)
                findTerminalOrNonTerminalNodes();

            return totalWidthOfNonTermNodes;
        }

        public int getTotalHeightNonTermNodes()
        {
            if (totalHeightOfNonTermNodes == 0)
                findTerminalOrNonTerminalNodes();

            return totalHeightOfNonTermNodes;
        }

        public int getNoOfTerminalNode()
        {
            if (terminalNodeCount == 0)
                findTerminalOrNonTerminalNodes();

            return terminalNodeCount;
        }

        public int getNoOfNonTerminalNode()
        {
            if (nonTerminalCount == 0)
                findTerminalOrNonTerminalNodes();

            return nonTerminalCount;
        }

        public List<Node> getAllNodes()
        {
            return Nodes;
        }
    }
}
