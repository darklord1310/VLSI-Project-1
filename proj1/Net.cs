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
    }
}
