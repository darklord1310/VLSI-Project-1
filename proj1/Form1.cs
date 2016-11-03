using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace proj1
{
    public partial class Form1 : Form
    {
        string nodeFilePath, netFilePath;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Nodes file|*.nodes";
            dialog.Multiselect = false;
            dialog.Title = "Select file";

            DialogResult dr = dialog.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                nodeFilePath = dialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NodesOperation node = new NodesOperation(nodeFilePath);
            NetsOperation net = new NetsOperation(netFilePath);
            StringBuilder s = new StringBuilder();
            s.AppendLine("Number of non terminal node: " + node.getNoOfNonTerminalNode());
            s.AppendLine("Number of terminal node: " + node.getNoOfTerminalNode());
            s.AppendLine("Total Length Of Non Terminal Node: " + node.getTotalLengthNonTermNodes().ToString());
            s.AppendLine("Largest terminal node: " + node.getLargestTerminalNode());
            s.AppendLine("Smallest terminal node: " + node.getSmallestTerminalNode());
            s.AppendLine("Largest non terminal node: " + node.getLargestNonTerminalNode());
            s.AppendLine("Smallest non terminal node: " + node.getSmallestNonTerminalNode());
            s.AppendLine("Total number of nets: " + net.getTotalNets().ToString());
            
            List<Net> temp = net.getMaxDegreeNetPair();
            s.AppendLine("All Nets with highest degree: ");
            foreach (Net n in temp)
            {
                s.AppendLine(n.getNetDegree().ToString() + " " + n.getNetName());
            }

            s.AppendLine("Total number of input pins: " + net.getTotalInputPins().ToString());
            s.AppendLine("Total number of output pins: " + net.getTotalOutputPins().ToString());

            /*
            List<Net> temp2 = net.getAllNetsInfo();
            foreach (Net n in temp)
            {
                Console.WriteLine(n.getNetName() + " " + n.getNetName() );
                Console.WriteLine("Input Nodes");
                foreach(NetFanout f in n.getInputNodes())
                {
                    Console.WriteLine(f.getFanoutName() + " " + f.getCoordX() + " " + f.getCoordY());
                }
                Console.WriteLine("Output Node");
                Console.WriteLine(n.getOutputNodes().getFanoutName() + " " + n.getOutputNodes().getCoordX() + " " + n.getOutputNodes().getCoordY());
            }
            */

            string str = net.displayHistogramOfConnectivity();
            s.AppendLine(str);

            JR.Utils.GUI.Forms.FlexibleMessageBox.Show(s.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Nets file | *.nets";
            dialog.Multiselect = false;
            dialog.Title = "Select file";

            DialogResult dr = dialog.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                netFilePath = dialog.FileName;
            }
        }
    }
}
