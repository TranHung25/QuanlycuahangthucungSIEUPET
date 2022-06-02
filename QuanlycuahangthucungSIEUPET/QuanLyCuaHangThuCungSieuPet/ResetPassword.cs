using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyCuaHangThuCungSieuPet
{
    public partial class ResetPassword : Form
    {
        public static string quyen;
        public static string matkhau;
        public static string taikhoan;
        public ResetPassword()
        {
            InitializeComponent();
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            taikhoan = conn.XemDL("select UserName from UserAccount").Rows[0][0].ToString().Trim();
            matkhau = conn.XemDL("select Password from UserAccount").Rows[0][0].ToString().Trim();
            txtUserName.Text = taikhoan;
            txtPasswordCu.Text = matkhau;
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            if (txtNewPassword.Text == "" || txtnhaplaiPass.Text == "")
            {
                MessageBox.Show("Tên tài khoản hoặc mật khẩu chưa được nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if(txtNewPassword.Text!=txtnhaplaiPass.Text)
                {
                    MessageBox.Show("Mật khẩu không trùng khớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                   
                    bool check = conn.update("UPDATE UserAccount SET Password = '" + txtnhaplaiPass.Text + "' where UserName = '" + txtUserName.Text + "'");
                    if (check == true)
                    {
                        MessageBox.Show("Reset successfully !");
                        Login form = new Login();
                        this.Hide();
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu không trùng khớp !");
                    }
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ResetPassword_Load(object sender, EventArgs e)
        {

        }
    }
}
