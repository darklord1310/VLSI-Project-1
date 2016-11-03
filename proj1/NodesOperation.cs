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
        private string nodesPath = "";
        private string[] lines;
        private string largestNonTermNode = "";
        private string smallestNonTermNode = "";
        private string largestTermNode = "";
        private string smallestTermNode = "";
        private int totalWidthOfNonTermNodes = 0;
        private List<Node> Nodes = new List<Node>();

        public NodesOperation(string path)
        {
            this.nodesPath = path;
            try
            {
                lines = File.ReadAllLines(@nodesPath, Encoding.UTF8);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // to be implement
        public void readFromPath(string path)
        {
        }

        private void findTerminalOrNonTerminalNodes()
        {
            int largestTermArea = 0;
            int smallestTermArea = 0;
            int largestNonTermArea = 0;
            int smallestNonTermArea = 0;
            foreach (string line in lines)
            {
                Regex termNodesPattern = new Regex(@"^NumTerminals :\s+(\d+)");
                Regex totalNodesPattern = new Regex(@"^NumNodes :\s+(\d+)");
                Regex terminalNodeInfo = new Regex(@"(o\d+)\s+(\d+)\s+(\d+)\s+(\w+)");
                Regex nonTerminalNodeInfo = new Regex(@"(o\d+)\s+(\d+)\s+(\d+)");
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
                    
                    if (area > largestNonTermArea)
                    {
                        largestNonTermNode = name;
                        largestNonTermArea = area;
                    }

                    if (smallestNonTermArea == 0)
                    {
                        smallestNonTermArea = area;
                        smallestNonTermNode = name;
                    }
                    else if(area < smallestNonTermArea)
                    {
                        smallestNonTermArea = area;
                        smallestNonTermNode = name;
                    }

                    Node newNode = new Node(name, type, w, h);
                    Nodes.Add(newNode);
                    totalWidthOfNonTermNodes += w;
                }

                if (terminalInfomatch.Success)
                {
                    string name = terminalInfomatch.Groups[1].Value;
                    int w = Convert.ToInt16(terminalInfomatch.Groups[2].Value);
                    int h = Convert.ToInt16(terminalInfomatch.Groups[3].Value);
                    string type = terminalInfomatch.Groups[4].Value;
                    int area = w * h;

                    if (area > largestTermArea)
                    {
                        largestTermNode = name;
                        largestTermArea = area;
                    }

                    if (smallestTermArea == 0)
                    {
                        smallestTermArea = area;
                        smallestTermNode = name;
                    }
                    else if (area < smallestTermArea)
                    {
                        smallestTermArea = area;
                        smallestTermNode = name;
                    }

                    Node newNode = new Node(name,type,w,h);
                    Nodes.Add(newNode);
                    totalWidthOfNonTermNodes += w;
                }
            }
            nonTerminalCount = totalNodeCount - terminalNodeCount;
        }

        public string getSmallestTerminalNode()
        {
            if (smallestTermNode == "")
                findTerminalOrNonTerminalNodes();

            return smallestTermNode;
        }

        public string getSmallestNonTerminalNode()
        {
            if (smallestNonTermNode == "")
                findTerminalOrNonTerminalNodes();

            return smallestNonTermNode;
        }


        public string getLargestTerminalNode()
        {
            if (largestTermNode == "")
                findTerminalOrNonTerminalNodes();

            return largestTermNode;
        }

        public string getLargestNonTerminalNode()
        {
            if (largestNonTermNode == "")
                findTerminalOrNonTerminalNodes();

            return largestNonTermNode;
            
        }

        public int getTotalLengthNonTermNodes()
        {
            if (totalWidthOfNonTermNodes == 0)
                findTerminalOrNonTerminalNodes();

            return totalWidthOfNonTermNodes;
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
    }
}
