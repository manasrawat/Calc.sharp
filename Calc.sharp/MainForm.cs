using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Calc.sharp {
	public partial class MainForm : Form {
		Font CenturyGothic;
		public MainForm() {
			InitializeComponent();
			
			//Form
			this.BackColor = Color.White;
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			
			//Label
			label1.BackColor= Color.White;
			CenturyGothic = new Font("Century Gothic", 20f, FontStyle.Bold);
			label1.Font = CenturyGothic;
			label1.AutoSize = false;
			label1.Height = 33;
			label1.TextAlign = ContentAlignment.MiddleLeft;
			label1.Text = "";
			label1.Anchor = AnchorStyles.Left;
			
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
				label1.Text = "";
				label1.Font = CenturyGothic;
			} else if (button.Text == "DEL") {
				if (label1.Text != "") {
					if (label1.Font.Size < 20f && label1.Text.Length > 11) {
    						label1.Font = new Font(label1.Font.FontFamily, label1.Font.Size + 0.5f, label1.Font.Style);
					}
										label1.Text = label1.Text.Substring(0, label1.Text.Length - 1);
				}
			} else {
				label1.Text += button.Text;
				while (label1.Width < System.Windows.Forms.TextRenderer.MeasureText(label1.Text, 
     				new Font(label1.Font.FontFamily, label1.Font.Size, label1.Font.Style)).Width) {
    				label1.Font = new Font(label1.Font.FontFamily, label1.Font.Size - 0.5f, label1.Font.Style);
				}
			}
		}
		
		void RichTextBox1TextChanged(object sender, EventArgs e)
		{
			
		}
	}
}
