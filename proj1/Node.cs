using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj1
{
    class Node
    {
        private string nodeName = "";
        private string nodeType = "";
        private int nodeWidth = 0;
        private int nodeHeight = 0;
        

        public Node (string n, string t, int w, int h)
        {
            nodeHeight = h;
            nodeName = n;
            nodeType = t;
            nodeWidth = w;
        }

        public string getNodeName() { return nodeName; }
        public string getNodeType() { return nodeType; }
        public int getNodeWidth() { return nodeWidth; }
        public int getNodeHeight() { return nodeHeight; }
        public void setNodeHeight(int h) { nodeHeight = h; }
        public void setNodeWidth(int w) { nodeWidth = w; }
        public void setNodeName(string name) { nodeName = name; }
        public void setNodeType(string type) { nodeType = type; }

    }
}
