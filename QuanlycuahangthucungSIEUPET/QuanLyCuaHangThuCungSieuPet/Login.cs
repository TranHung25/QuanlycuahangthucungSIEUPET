using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangThuCungSieuPet
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            if(Properties.Settings.Default.RememberMe == "true")
            {
                txtName.Text = Properties.Settings.Default.User;
                txtPassword.Text = Properties.Settings.Default.Password;
                checkSave.Checked = true;
            }
            else
            {
                txtName.Text = "";
                txtPassword.Text = "";
                checkSave.Checked = false;
            }
        }
        // click để thoát khỏi đăng nhập.
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // click để đăng nhập.
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtName.Text =="" || txtPassword.Text== "")
            {
                MessageBox.Show("Tài Khoản hoặc mật khẩu không được trống !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Connect conn = new Connect();
                DataSet ds = new DataSet();
                ds = conn.select("select * from UserAccount where UserName = '" + txtName.Text + "' and Password = '" + txtPassword.Text + "'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    checkSave_CheckedChanged(sender,e);
                    HomePage.quyen= conn.XemDL("select decentralization from UserAccount where UserName = '" + txtName.Text + "' and Password = '" + txtPassword.Text + "'").Rows[0][0].ToString().Trim();
                    Staff.quyen = conn.XemDL("select decentralization from UserAccount where UserName = '" + txtName.Text + "' and Password = '" + txtPassword.Text + "'").Rows[0][0].ToString().Trim();
                    ProductPortfolio.quyen = conn.XemDL("select decentralization from UserAccount where UserName = '" + txtName.Text + "' and Password = '" + txtPassword.Text + "'").Rows[0][0].ToString().Trim();
                    NewsList.quyen = conn.XemDL("select decentralization from UserAccount where UserName = '" + txtName.Text + "' and Password = '" + txtPassword.Text + "'").Rows[0][0].ToString().Trim();
                    NewsCategory.quyen = conn.XemDL("select decentralization from UserAccount where UserName = '" + txtName.Text + "' and Password = '" + txtPassword.Text + "'").Rows[0][0].ToString().Trim();
                    ListOfProducts.quyen = conn.XemDL("select decentralization from UserAccount where UserName = '" + txtName.Text + "' and Password = '" + txtPassword.Text + "'").Rows[0][0].ToString().Trim();
                    InvoiceList.quyen = conn.XemDL("select decentralization from UserAccount where UserName = '" + txtName.Text + "' and Password = '" + txtPassword.Text + "'").Rows[0][0].ToString().Trim();
                    Invoice.quyen = conn.XemDL("select decentralization from UserAccount where UserName = '" + txtName.Text + "' and Password = '" + txtPassword.Text + "'").Rows[0][0].ToString().Trim();
                    CustomerManagement.quyen = conn.XemDL("select decentralization from UserAccount where UserName = '" + txtName.Text + "' and Password = '" + txtPassword.Text + "'").Rows[0][0].ToString().Trim();
                    Supplier.quyen = conn.XemDL("select decentralization from UserAccount where UserName = '" + txtName.Text + "' and Password = '" + txtPassword.Text + "'").Rows[0][0].ToString().Trim();
                    Introduct.quyen = conn.XemDL("select decentralization from UserAccount where UserName = '" + txtName.Text + "' and Password = '" + txtPassword.Text + "'").Rows[0][0].ToString().Trim();
                    MessageBox.Show("You login successfully ");
                    HomePage homePage = new HomePage();
                    homePage.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("You login unsuccessful");
                }
            }
        }
        // click để tạo tài khoản mới.
        private void label6_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            this.Hide();
            registration.Show();
        }
        // hàm lưu mật khẩu
        private void checkSave_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSave.Checked)
            {
                Properties.Settings.Default.User = txtName.Text;
                Properties.Settings.Default.Password = txtPassword.Text;
                Properties.Settings.Default.RememberMe = "true";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.User = txtName.Text;
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.RememberMe = "false";
                Properties.Settings.Default.Save();
            }
        }
    
        // đổi mật khẩu
        private void label5_Click(object sender, EventArgs e)
        {
            ResetPassword reset = new ResetPassword();
            this.Hide();
            reset.Show();
        }

        private void CheckMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckMatKhau.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
