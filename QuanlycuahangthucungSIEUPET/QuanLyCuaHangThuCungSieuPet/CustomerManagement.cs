using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyCuaHangThuCungSieuPet
{
    public partial class CustomerManagement : Form
    {
        public static string quyen;
        public static string makh;
        public CustomerManagement()
        {
            InitializeComponent();
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            ds = conn.select("select * from khachhang");
            combomakh.DataSource = ds.Tables[0];
            combomakh.ValueMember = "makh";
            KetQua();
        }
        
        public void KetQua()
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            ds = conn.select("SELECT makh as 'Mã khách hàng', tenkh as 'Tên khách hàng', diachi as 'Địa chỉ', sodienthoai as 'Số điện thoại', ngaysinh as 'Ngày sinh' from khachhang");
            dgvkhachhang.DataSource = ds.Tables[0];
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            long n;
            if (txtmakh.Text == "")
            {
                MessageBox.Show("Hãy nhập mã khách hàng", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtenkh.Text == "")
            {
                MessageBox.Show("Hãy nhập tên khách hàng", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtsdt.Text == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtdiachi.Text == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ của khách hàng", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtsdt.Text.Length > 11 || txtsdt.Text.Length < 10)
            {
                MessageBox.Show("Số điện thoại phải nhập từ 10 đến 11 số", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (long.TryParse(txtsdt.Text, out n))
                {
                    Connect conn = new Connect();
                    makh = "SELECT makh from khachhang where makh = '" + txtmakh.Text.Trim() + "'";
                    if (conn.CheckKey(makh))
                    {
                        MessageBox.Show("Mã khách hàng này đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        bool kiemtra = conn.update("insert into khachhang values(N'" + txtmakh.Text + "',N'" + txtenkh.Text + "',N'" + txtdiachi.Text + "',N'" + txtsdt.Text + "',N'" + timengaysinh.Value + "') ");
                        if (kiemtra == true)
                        {
                            btnreset_Click(sender, e);
                            DataSet ds = new DataSet();
                            ds = conn.select("select * from khachhang");
                            combomakh.DataSource = ds.Tables[0];
                            combomakh.ValueMember = "makh";
                            MessageBox.Show("Bạn đã thêm thành công");
                            KetQua();
                        }
                        else
                        {
                            MessageBox.Show("Bạn thêm thất bại");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Hãy nhập chính xác số điện thoại là số", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }       
            }
            
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtmakh.Text = "";
            txtenkh.Text = "";
            txtdiachi.Text = "";
            txtsdt.Text = "";
            timengaysinh.ResetText();
        }

        private void btnhienthi_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            ds = conn.select("SELECT makh as 'Mã khách hàng', tenkh as 'Tên khách hàng', diachi as 'Địa chỉ', sodienthoai as 'Số điện thoại', ngaysinh as 'Ngày sinh' from khachhang");
            dgvkhachhang.DataSource = ds.Tables[0];
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            long n;
            if (txtmakh.Text == "")
            {
                MessageBox.Show("Hãy nhập mã khách hàng", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtenkh.Text == "")
            {
                MessageBox.Show("Hãy nhập tên khách hàng", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtsdt.Text == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtdiachi.Text == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ của khách hàng", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtsdt.Text.Length > 11 || txtsdt.Text.Length < 10)
            {
                MessageBox.Show("Số điện thoại phải nhập từ 10 đến 11 số", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                if (long.TryParse(txtsdt.Text, out n))
                {
                    Connect conn = new Connect();
                    bool kiemtra = conn.update("UPDATE khachhang set tenkh =N'" + txtenkh.Text + "',diachi =N'" + txtdiachi.Text + "',sodienthoai =N'" + txtsdt.Text + "',ngaysinh =N'" + timengaysinh.Value + "' where makh = '" + txtmakh.Text + "' ");
                    if (kiemtra == true)
                    {
                        btnreset_Click(sender, e);
                        MessageBox.Show("Bạn đã sửa thành công");
                        KetQua();
                    }
                    else
                    {
                        MessageBox.Show("Bạn sửa thất bại");
                    }
                }
                else
                {
                    MessageBox.Show("Hãy nhập chính xác số điện thoại là số", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            makh = "SELECT makh from khachhang where makh = '" + txtmakh.Text.Trim() + "'";
            if (conn.CheckKey(makh))
            {
                bool kiemtra = conn.update("delete khachhang where makh = '" + txtmakh.Text + "' ");
                if (kiemtra == true)
                {
                    btnreset_Click(sender, e);
                    MessageBox.Show("Bạn đã xoá thành công");
                    KetQua();
                }
                else
                {
                    MessageBox.Show("Bạn xoá thất bại");
                }
            }
            else
            {
                MessageBox.Show("Mã khách hàng này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            makh = "SELECT makh from khachhang where makh = '" +combomakh.SelectedValue+ "'";
            if (conn.CheckKey(makh))
            {
                MessageBox.Show("Bạn đã tìm kiếm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ds = conn.select("SELECT makh as 'Mã khách hàng', tenkh as 'Tên khách hàng', diachi as 'Địa chỉ', sodienthoai as 'Số điện thoại', ngaysinh as 'Ngày sinh' from khachhang where makh like N'%" + combomakh.SelectedValue + "%'");
                dgvkhachhang.DataSource = ds.Tables[0];
                btnreset_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Mã khách hàng này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void dgvkhachhang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmakh.Text = dgvkhachhang.CurrentRow.Cells[0].Value.ToString();
            txtenkh.Text = dgvkhachhang.CurrentRow.Cells[1].Value.ToString();
            txtdiachi.Text = dgvkhachhang.CurrentRow.Cells[2].Value.ToString();
            txtsdt.Text = dgvkhachhang.CurrentRow.Cells[3].Value.ToString();
            timengaysinh.Text = dgvkhachhang.CurrentRow.Cells[4].Value.ToString();
        }

        private void CustomerManagement_Load(object sender, EventArgs e)
        {
            if (quyen == "User")
            {
                quảnLýNhânViênToolStripMenuItem.Enabled = false;
                danhSáchHoáĐơnToolStripMenuItem.Visible = false;
            }
            danhSáchHoáĐơnToolStripMenuItem.Visible = false;
        }

        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomePage home = new HomePage();
            this.Hide();
            home.Show();
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerManagement customer = new CustomerManagement();
            this.Hide();
            customer.Show();
        }

        private void danhSáchNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff();
            this.Hide();
            staff.Show();
        }

       

        private void danhSáchSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListOfProducts dssp = new ListOfProducts();
            this.Hide();
            dssp.Show();
        }

        private void danhMụcSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductPortfolio dmsp = new ProductPortfolio();
            this.Hide();
            dmsp.Show();
        }

        private void danhSáchTinTứcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewsList news = new NewsList();
            this.Hide();
            news.Show();
        }

        private void danhMụcTinTứcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewsCategory newsCategory = new NewsCategory();
            this.Hide();
            newsCategory.Show();
        }

        private void lậpHoáĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceList dshd = new InvoiceList();
            this.Hide();
            dshd.Show();
        }

        private void danhSáchHoáĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invoice laphoadon = new Invoice();
            this.Hide();
            laphoadon.Show();
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier();
            this.Hide();
            supplier.Show();
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login dangnhap = new Login();
            this.Hide();
            dangnhap.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login dangnhap = new Login();
            this.Hide();
            dangnhap.Show();
        }

        private void đăngKýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registration dangxuat = new Registration();
            this.Hide();
            dangxuat.Show();
        }

        private void thôngBáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Introduct introduct = new Introduct();
            this.Hide();
            introduct.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
