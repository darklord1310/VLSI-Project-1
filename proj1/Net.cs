using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj1
{
    class Net
    {
        private string netName = "";
        private int netDegree;
        private List<NetFanout> inputNodes;
        private NetFanout outputNodes;

        public Net (string n, int d)
        {
            netName = n;
            netDegree = d;
            inputNodes = new List<NetFanout>();
            outputNodes = new NetFanout(); 
        }

        public Net(string n, int d, List<NetFanout> i, NetFanout o)
        {
            netName = n;
            netDegree = d;
            inputNodes = i;
            outputNodes = o;
        }

        public string getNetName() { return netName; }
        public int getNetDegree() { return netDegree; }
        public List<NetFanout> getInputNodes() { return inputNodes; }
        public NetFanout getOutputNodes() { return outputNodes; }

        public void setNetName(string n) { netName = n; }

        public void setNetDegree(int degree) { netDegree = degree; }

        public void addInputNodes(NetFanout fanout)
        {
            inputNodes.Add(fanout);
        }

        public void setOutputNodes(NetFanout outputNode)
        {
            outputNodes = outputNode;
        }
    }
}
