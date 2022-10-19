using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace authorization
{
    public partial class Authorization_Form : Form
    {
        string connStr = "chuc.caseum.ru; port=33333; user=is_3_20_12; database=is_3_20_st12_KURS; password=33251525";
        MySqlConnection conn;

        static string sha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        public void GetUserInfo(string login_user)
        {
            string selected_id_stud = textBox1.Text;
            conn.Open();
            string sql = $"SELECT * FROM employees_T WHERE login_employees='{login_user}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Auth_Class.auth_id = reader[0].ToString();
                Auth_Class.auth_fullname = reader[1].ToString();
                Auth_Class.auth_role = Convert.ToInt32(reader[2].ToString());
            }
            reader.Close();
            conn.Close();
        }

        public Authorization_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM t_user WHERE loginUser = @un and  passUser= @up";
            conn.Open();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@un", MySqlDbType.VarChar, 25);
            cmd.Parameters.Add("@up", MySqlDbType.VarChar, 25);

            cmd.Parameters["@un"].Value = textBox1.Text;
            cmd.Parameters["@up"].Value = sha256(textBox2.Text);

            adapter.SelectCommand = cmd;
            adapter.Fill(table);

            conn.Close();

            if (table.Rows.Count > 0)
            {
                Auth_Class.auth = true;
                GetUserInfo(textBox1.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("WRONG!");
            }
        }

        private void Authorization_Form_Load (object sender, EventArgs e)
        {
            conn = new MySqlConnection(connStr);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
