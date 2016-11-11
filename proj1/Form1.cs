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
        string nodeFilePath = "", netFilePath = "" ,fileName;
        NodesOperation node = new NodesOperation();
        NetsOperation net = new NetsOperation();

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
                fileName = dialog.SafeFileName.Remove(dialog.SafeFileName.IndexOf('.'));
                nodeFilePath = dialog.FileName;
                node.readFileFromPath(nodeFilePath);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(nodeFilePath != "" && netFilePath != "")
            {
                StringBuilder s = new StringBuilder();
                button3.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                s.AppendLine("Number of Non Terminal Node : " + node.getNoOfNonTerminalNode());
                s.AppendLine("Number of Terminal Node : " + node.getNoOfTerminalNode());
                s.AppendLine("Total Height Of Non Terminal Node : " + node.getTotalHeightNonTermNodes().ToString());
                s.AppendLine("Total Width Of Non Terminal Node : " + node.getTotalWidthNonTermNodes().ToString());
                List<string> temp = node.getLargestTerminalNode();
                s.AppendLine("Largest terminal node : " + node.largestTermArea.ToString());
                foreach (string val in temp) { s.AppendLine("\t" + val); }
                temp.Clear();
                temp = node.getSmallestTerminalNode();
                s.AppendLine("Smallest terminal node : " + node.smallestTermArea.ToString());
                foreach (string val in temp) { s.AppendLine("\t" + val); }
                temp.Clear();
                temp = node.getLargestNonTerminalNode();
                s.AppendLine("Largest non terminal node : " + node.largestNonTermArea.ToString());
                foreach (string val in temp) { s.AppendLine("\t" + val); }
                temp.Clear();
                temp = node.getSmallestNonTerminalNode();
                s.AppendLine("Smallest non terminal node : " + node.smallestNonTermArea.ToString());
                foreach (string val in temp) { s.AppendLine("\t" + val); }
                temp.Clear();

                s.AppendLine("Total number of nets : " + net.getTotalNets().ToString());
                s.AppendLine("All Nets with highest degree : " + net.getMaxDegreeNetPair());
                s.AppendLine("Total number of input pins : " + net.getTotalInputPins().ToString());
                s.AppendLine("Total number of output pins : " + net.getTotalOutputPins().ToString());

                //List<Net> temp2 = net.getAllNets();
                //Console.WriteLine(temp2.Count.ToString());
                
                /*
                foreach (Net n in temp2)
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
                Cursor.Current = Cursors.Default;
                button3.Enabled = true;
                JR.Utils.GUI.Forms.FlexibleMessageBox.Show(s.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StringBuilder s = new StringBuilder();
            using (StreamWriter writer = new StreamWriter(fileName + ".rpt", false))
            {
                if (nodeFilePath != "" && netFilePath != "")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    button4.Enabled = true;
                    s.AppendLine("Number of Non Terminal Node : " + node.getNoOfNonTerminalNode());
                    s.AppendLine("Number of Terminal Node : " + node.getNoOfTerminalNode());
                    s.AppendLine("Total Height Of Non Terminal Node : " + node.getTotalHeightNonTermNodes().ToString());
                    s.AppendLine("Total Width Of Non Terminal Node : " + node.getTotalWidthNonTermNodes().ToString());
                    List<string> temp = node.getLargestTerminalNode();
                    s.AppendLine("Largest terminal node : " + node.largestTermArea.ToString());
                    foreach (string val in temp) { s.AppendLine("\t" + val); }
                    temp.Clear();
                    temp = node.getSmallestTerminalNode();
                    s.AppendLine("Smallest terminal node : " + node.smallestTermArea.ToString());
                    foreach (string val in temp) { s.AppendLine("\t" + val); }
                    temp.Clear();
                    temp = node.getLargestNonTerminalNode();
                    s.AppendLine("Largest non terminal node : " + node.largestNonTermArea.ToString());
                    foreach (string val in temp) { s.AppendLine("\t" + val); }
                    temp.Clear();
                    temp = node.getSmallestNonTerminalNode();
                    s.AppendLine("Smallest non terminal node : " + node.smallestNonTermArea.ToString());
                    foreach (string val in temp) { s.AppendLine("\t" + val); }
                    temp.Clear();

                    s.AppendLine("Total number of nets : " + net.getTotalNets().ToString());
                    s.AppendLine("All Nets with highest degree : " + net.getMaxDegreeNetPair());
                    s.AppendLine("Total number of input pins : " + net.getTotalInputPins().ToString());
                    s.AppendLine("Total number of output pins : " + net.getTotalOutputPins().ToString());

                    string str = net.displayHistogramOfConnectivity();
                    s.AppendLine(str);
                }

                writer.WriteLine(s.ToString());
            }
            Cursor.Current = Cursors.Default;
            button4.Enabled = true;
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
                net = new NetsOperation(netFilePath);
            }
        }
    }
}
