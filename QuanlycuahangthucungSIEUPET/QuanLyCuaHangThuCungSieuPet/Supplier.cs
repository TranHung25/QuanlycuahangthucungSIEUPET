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
    public partial class Supplier : Form
    {
        public static string quyen;
        public static string manhacungcap;
        public void load()
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            ds = conn.select("select * from nhacungcap");
            cbMaNCC.DataSource = ds.Tables[0];
            cbMaNCC.ValueMember = "manhacungcap";
        }
        public Supplier()
        {
            InitializeComponent();
            load();
            KetQua();
        }
        public void KetQua()
        {
            DataSet ds = new DataSet();
            Connect conn = new Connect();
            ds = conn.select("select manhacungcap as 'Mã', tennhacungcap as 'Nhà cung cấp',diachi as 'Địa chỉ', sodienthoai as 'Số điện thoại' from nhacungcap");
            dgvNCC.DataSource = ds.Tables[0];
          
        }
        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            long n;
            if (txtMaNhaCungCap.Text == "")
            {
                MessageBox.Show("Hãy nhập mã nhà cung cấp", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtTenNCC.Text == "")
            {
                MessageBox.Show("Hãy nhập tên nhà cung cấp", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtDiaChiNCC.Text == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ nhà cung cấp", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtSDT_NCC.Text == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtSDT_NCC.Text.Length > 11 || txtSDT_NCC.Text.Length < 10)
            {
                MessageBox.Show("Số điện thoại phải nhập từ 10 đến 11 số", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                manhacungcap = "SELECT manhacungcap from nhacungcap where manhacungcap = '" + txtMaNhaCungCap.Text.Trim() + "'";
                if (conn.CheckKey(manhacungcap))
                {
                    MessageBox.Show("Mã nhà cung cấp này đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (long.TryParse(txtSDT_NCC.Text, out n))
                    {
                        bool kiemtra = conn.update("insert into nhacungcap values(N'" + txtMaNhaCungCap.Text + "',N'" + txtTenNCC.Text + "',N'" + txtDiaChiNCC.Text + "',N'" + txtSDT_NCC.Text + "') ");
                        if (kiemtra == true)
                        {
                            btnLamMoiNCC_Click(sender, e);
                            MessageBox.Show("Bạn đã thêm thành công");
                            load();
                            KetQua();
                        }
                        else
                        {
                            MessageBox.Show("Bạn thêm thất bại");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hãy nhập số điện thoại với dữ liệu kiểu số", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnSuaNCC_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            long n;
            if (txtMaNhaCungCap.Text == "")
            {
                MessageBox.Show("Hãy nhập mã nhà cung cấp", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtTenNCC.Text == "")
            {
                MessageBox.Show("Hãy nhập tên nhà cung cấp", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtDiaChiNCC.Text == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ nhà cung cấp", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtSDT_NCC.Text == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtSDT_NCC.Text.Length > 11 || txtSDT_NCC.Text.Length < 10)
            {
                MessageBox.Show("Số điện thoại phải nhập từ 10 đến 11 số", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                manhacungcap = "SELECT manhacungcap from nhacungcap where manhacungcap = '" + txtMaNhaCungCap.Text.Trim() + "'";
                if (conn.CheckKey(manhacungcap))
                {
                    if (long.TryParse(txtSDT_NCC.Text, out n))
                    {
                        bool kiemtra = conn.update("UPDATE nhacungcap set manhacungcap = N'" + txtMaNhaCungCap.Text + "',tennhacungcap =N'" + txtTenNCC.Text + "',diachi =N'" + txtDiaChiNCC.Text + "',sodienthoai =N'" + txtSDT_NCC.Text + "' where manhacungcap = '" + txtMaNhaCungCap.Text + "' ");
                        if (kiemtra == true)
                        {
                            btnLamMoiNCC_Click(sender, e);
                            MessageBox.Show("Bạn đã sửa thành công");
                            load();
                            KetQua();
                        }
                        else
                        {
                            MessageBox.Show("Bạn sửa thất bại");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hãy nhập số điện thoại với dữ liệu kiểu số", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Mã nhà cung cấp này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } 
        }

        private void btnXoaNCC_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            if(txtMaNhaCungCap.Text == "")
            {
                MessageBox.Show("Hãy nhập mã nhà cung cấp", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                manhacungcap = "SELECT manhacungcap from nhacungcap where manhacungcap = '" + txtMaNhaCungCap.Text.Trim() + "'";
                if (conn.CheckKey(manhacungcap))
                {
                    bool kiemtra = conn.update("delete nhacungcap where manhacungcap = '" + txtMaNhaCungCap.Text + "' ");
                    if (kiemtra == true)
                    {
                        btnLamMoiNCC_Click(sender, e);
                        MessageBox.Show("Bạn đã xoá thành công");
                        load();
                        KetQua();
                    }
                    else
                    {
                        MessageBox.Show("Bạn xoá thất bại");
                    }
                }
                else
                {
                    MessageBox.Show("Mã nhà cung cấp này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            
        }

        private void btnTimKiemNCC_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            manhacungcap = "SELECT manhacungcap from nhacungcap where manhacungcap = '" + cbMaNCC.SelectedValue + "'";
            if (conn.CheckKey(manhacungcap))
            {
                MessageBox.Show("Bạn đã tìm kiếm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ds = conn.select("select manhacungcap as 'Mã', tennhacungcap as 'Nhà cung cấp',diachi as 'Địa chỉ', sodienthoai as 'Số điện thoại' from nhacungcap where manhacungcap like N'%" + cbMaNCC.SelectedValue.ToString() + "%' ");
                dgvNCC.DataSource = ds.Tables[0];
                btnLamMoiNCC_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Mã khách hàng này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void btnHienThiNCC_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            Connect conn = new Connect();
            ds = conn.select("select manhacungcap as 'Mã', tennhacungcap as 'Nhà cung cấp',diachi as 'Địa chỉ', sodienthoai as 'Số điện thoại' from nhacungcap");
            dgvNCC.DataSource = ds.Tables[0];
        }

        private void btnLamMoiNCC_Click(object sender, EventArgs e)
        {
            txtMaNhaCungCap.Text = "";
            txtTenNCC.Text = "";
            txtSDT_NCC.Text = "";
            txtDiaChiNCC.Text = "";
        }

        private void dgvNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int _row = e.RowIndex;
            txtMaNhaCungCap.Text = dgvNCC.Rows[_row].Cells[0].Value.ToString();
            txtTenNCC.Text = dgvNCC.Rows[_row].Cells[1].Value.ToString();
            txtDiaChiNCC.Text = dgvNCC.Rows[_row].Cells[2].Value.ToString();
            txtSDT_NCC.Text = dgvNCC.Rows[_row].Cells[3].Value.ToString();
        }

        private void Supplier_Load(object sender, EventArgs e)
        {
            danhSáchHoáĐơnToolStripMenuItem.Visible = false;
            if (quyen == "User")
            {
                quảnLýNhânViênToolStripMenuItem.Enabled = false;
            }
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
