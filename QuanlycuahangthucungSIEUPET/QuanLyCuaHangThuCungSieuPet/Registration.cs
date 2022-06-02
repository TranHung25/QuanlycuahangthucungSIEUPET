using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QuanLyCuaHangThuCungSieuPet
{
    public partial class Registration : Form
    {
        public static string quyen;
        public Registration()
        {
            Connect conn = new Connect();
            InitializeComponent();
            quyen = conn.XemDL("SELECT decentralization from UserAccount").Rows[0][0].ToString().Trim();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if(txtPhanQuyen.Text == quyen)
            {
                MessageBox.Show("Bạn không được đăng ký quyền Admin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Connect conn = new Connect();
                bool insert = conn.update("INSERT INTO UserAccount values(N'" + txtSetUserName.Text + "', N'" + txtSetPassword.Text + "', N'" + txtPhanQuyen.Text + "')");
                if (insert == true)
                {
                    MessageBox.Show("Sign Up Success");
                    Login form1 = new Login();
                    this.Hide();
                    form1.Show();

                }
                else
                {
                    MessageBox.Show("Registration failed");
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
