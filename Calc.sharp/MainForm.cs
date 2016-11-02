using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Calc.sharp {
    public partial class MainForm : Form {
        Font CenturyGothic;
        string op;
        bool erase;
        double stored;
        string clickStatus;
        public MainForm() {
            InitializeComponent();
            clickStatus = "unclicked";
            stored = 0;
            panel1.BorderStyle = BorderStyle.None;
            
            //Form
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            //RTB
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.ReadOnly = true;
            richTextBox1.BackColor= Color.White;
            CenturyGothic = new Font("Century Gothic", 20f, FontStyle.Bold);
            richTextBox1.Font = CenturyGothic;
            richTextBox1.AutoSize = false;
            richTextBox1.Height = 33;
            richTextBox1.Text = "";
            richTextBox1.Anchor = AnchorStyles.Left;
            
            /*Buttons*/
            
            //Numbers
            List<Button> buttons = new List<Button>();
            button1.Text = "1"; buttons.Add(button1); button6.Text = "6"; buttons.Add(button6);
            button2.Text = "2"; buttons.Add(button2); button7.Text = "7"; buttons.Add(button7);
            button3.Text = "3"; buttons.Add(button3); button8.Text = "8"; buttons.Add(button8);
            button4.Text = "4"; buttons.Add(button4); button9.Text = "9"; buttons.Add(button9);
            button5.Text = "5"; buttons.Add(button5); button10.Text = "0"; buttons.Add(button10);
            
            //Operators & more
            button11.Text = "+"; buttons.Add(button11); button15.Text = "x"; buttons.Add(button15);
            button12.Text = "-"; buttons.Add(button12); button16.Text = "."; buttons.Add(button16);
            button13.Text = "÷"; buttons.Add(button13); button17.Text = "√"; buttons.Add(button17);
            button14.Text = "="; buttons.Add(button14); button18.Text = "DEL"; buttons.Add(button18);
            button19.Text = "AC"; buttons.Add(button19);

            foreach (Button button in buttons) {
                button.TabStop = false;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Font = new Font("Century Gothic", 10f, FontStyle.Bold);
                button.BackColor = Color.WhiteSmoke;
                button.Click += new EventHandler(onClick);
            }
        }
        
        void onClick(object sender, EventArgs e) {
            Button button = (Button) sender;
            if (button.Text == "AC") {
                stored = 0;
                richTextBox1.Text = "";
                op = "";
            } else if (button.Text == "DEL") {
                if (erase) richTextBox1.Text = ""; erase = false;
                if (richTextBox1.Text != "") {
                    if (richTextBox1.Font.Size < 20f && richTextBox1.Text.Length > 11) {
                        richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size + 0.5f, richTextBox1.Font.Style);
                    }
                    richTextBox1.Text = richTextBox1.Text.Substring(0, richTextBox1.Text.Length - 1);
                }
            } else if (button.Text == "+"||button.Text == "-"||button.Text == "x"||button.Text == "÷") {
                if (erase) richTextBox1.Text = ""; erase = false;
                if (clickStatus == "equalled") clickStatus = "unclicked";
                string[] ops = new string[] {"+", "-", "x", "÷"};
                for (int i = 0; i < 4; i ++) {
                    if (button.Text == ops[i]) {
                        op = ops[i];
                    }
                }
                richTextBox1.Text += op;
            } else if (button.Text == "=") {  
                string[] split;
                double res;
                bool valid = false;
                double uno;
                switch (op) {
                    case "+":
                        split = richTextBox1.Text.Split('+');
                        if (new Regex(@"\d+\+\d+").Match(richTextBox1.Text).Success) {
                            uno = double.Parse(split[0]);
                            erase = true;
                        } else {
                            uno = stored;
                            erase = false;
                        }
                        res = uno + double.Parse(split[1]);
                        valid = true;
                        break;
                    case "-":
                        split = richTextBox1.Text.Split('-');
                        if (new Regex(@"\d+\-\d+").Match(richTextBox1.Text).Success) {
                            uno = double.Parse(split[0]);
                            erase = true;
                        } else {
                            uno = stored;
                            erase = false;
                        }
                        res = uno - double.Parse(split[1]);
                        valid = true;
                        break;
                    case "x":
                        split = richTextBox1.Text.Split('x');
                        if (new Regex(@"\d+x\d+").Match(richTextBox1.Text).Success) {
                            uno = double.Parse(split[0]);
                            erase = true;
                        } else {
                            uno = stored;
                            erase = false;
                        }
                        res = uno * double.Parse(split[1]);
                        valid = true;
                        break;
                    case "÷":
                        split = richTextBox1.Text.Split('÷');
                        if (double.Parse(split[1]) == 0) {
                            res = 0;
                            valid = false;
                        } else {
                            if (new Regex(@"\d+÷\d+").Match(richTextBox1.Text).Success) {
                                uno = double.Parse(split[0]);
                                erase = true;
                            } else {
                                uno = stored;
                                erase = false;
                            }
                            res = uno / double.Parse(split[1]);
                            valid = true;
                        }
                        break;
                    default:
                        res = 0;
                        break;
                }
                if (valid) { richTextBox1.Text = res.ToString(); stored = res; erase = false; clickStatus = "equalled";
                } else { richTextBox1.Text = "N/A"; }
            } else {
                if (erase) richTextBox1.Text = ""; erase = false;
                if (clickStatus == "equalled") richTextBox1.Text = ""; clickStatus = "notequal";
                richTextBox1.Text += button.Text;
            }
        }
    }
}
