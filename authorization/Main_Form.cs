using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace authorization
{
    public partial class Main_Form : Form
    {

        public Main_Form()
        {
            InitializeComponent();
        }

        public void ManagerRole(int role)
        {
            switch (role)
            {
                case 1:
                    label1.Text = "MAX";
                    label1.ForeColor = Color.Green;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    break;
                case 2:
                    label1.Text = "MID";
                    label1.ForeColor = Color.YellowGreen;
                    button1.Enabled = false;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    break;
                case 3:
                    label1.Text = "LOW";
                    label1.ForeColor = Color.Yellow;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = true;
                    break;
                default:
                    label1.Text = "UNDEFINED";
                    label1.ForeColor = Color.Red;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    break;
            }
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            this.Hide();
            Authorization_Form authorization_Form = new Authorization_Form();
            authorization_Form.ShowDialog();

            if (Auth_Class.auth)
            {
                this.Show();

                label2.Text = Auth_Class.auth_id;
                label3.Text = Auth_Class.auth_fullname;
                label4.Text = "CORRECT!";

                label4.ForeColor = Color.Green;

                ManagerRole(Auth_Class.auth_role);
            }
            else
            {
                this.Close();
            }
        }


    }
}