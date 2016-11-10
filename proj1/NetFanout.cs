using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj1
{
    class NetFanout
    {
        private string fanoutName;
        private double coordx;
        private double coordy;

        public NetFanout(string name, double x, double y)
        {
            fanoutName = name;
            coordy = y;
            coordx = x;
        }

        public NetFanout()
        {
            fanoutName = "";
            coordy = 0;
            coordx = 0;
        }

        public void setFanoutName(string n)
        {
            fanoutName = n;
        }

        public void setCoordX(double x)
        {
            coordx = x;
        }

        public void setCoordY(double y)
        {
            coordy = y;
        }

        public string getFanoutName() { return fanoutName; }
        public double getCoordX() { return coordx; }
        public double getCoordY() { return coordy; }
    }
}
