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
    public partial class ProductPortfolio : Form
    {
        public static string quyen;
        public static string madanhmucsanpham;
        public void load()
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            ds = conn.select("select * from danhmucsanpham");
            cbDanhmucsanpham.DataSource = ds.Tables[0];
            cbDanhmucsanpham.ValueMember = "madanhmucsanpham";

        }
        public ProductPortfolio()
        {
            InitializeComponent();
            load();
            KetQua();
        }
        public void KetQua()
        {
            DataSet ds = new DataSet();
            Connect conn = new Connect();
            ds = conn.select("select madanhmucsanpham as 'Mã danh mục sản phẩm', tendanhmucsanpham as 'Tên danh mục sản phẩm' from danhmucsanpham");
            dgvdanhmucsanpham.DataSource = ds.Tables[0];
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if(txtmadanhmuc.Text=="")
            {
                MessageBox.Show("Hãy nhập mã danh mục sản phẩm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (txttendanhmuc.Text == "")
            {
                MessageBox.Show("Hãy nhập tên danh mục sản phẩm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Connect conn = new Connect();
                madanhmucsanpham = "SELECT madanhmucsanpham from danhmucsanpham where madanhmucsanpham = '" + txtmadanhmuc.Text.Trim() + "'";
                if (conn.CheckKey(madanhmucsanpham))
                {
                    MessageBox.Show("Mã danh mục sản phẩm đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    bool kiemtra = conn.update("insert into danhmucsanpham values(N'" + txtmadanhmuc.Text + "',N'" + txttendanhmuc.Text + "') ");
                    if (kiemtra == true)
                    {
                        btnreset_Click(sender, e);
                        MessageBox.Show("Bạn đã thêm thành công");
                        load();
                        KetQua();
                    }
                    else
                    {
                        MessageBox.Show("Bạn thêm thất bại");
                    }
                }
               
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtmadanhmuc.Text == "")
            {
                MessageBox.Show("Hãy nhập mã danh mục sản phẩm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (txttendanhmuc.Text == "")
            {
                MessageBox.Show("Hãy nhập tên danh mục sản phẩm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Connect conn = new Connect();
                madanhmucsanpham = "SELECT madanhmucsanpham from danhmucsanpham where madanhmucsanpham = '" + txtmadanhmuc.Text.Trim() + "'";
                if (conn.CheckKey(madanhmucsanpham))
                {
                    bool kiemtra = conn.update("UPDATE danhmucsanpham set tendanhmucsanpham = N'" + txttendanhmuc.Text + "' where madanhmucsanpham = N'" + txtmadanhmuc.Text + "'");
                    if (kiemtra == true)
                    {
                        btnreset_Click(sender, e);
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
                    MessageBox.Show("Mã danh mục sản phẩm không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(txtmadanhmuc.Text == "")
            {
                MessageBox.Show("Hãy nhập mã danh mục", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Connect conn = new Connect();
                madanhmucsanpham = "SELECT madanhmucsanpham from danhmucsanpham where madanhmucsanpham = '" + txtmadanhmuc.Text.Trim() + "'";
                if (conn.CheckKey(madanhmucsanpham))
                {
                    bool kiemtra = conn.update("delete danhmucsanpham where madanhmucsanpham = N'" + txtmadanhmuc.Text + "'");
                    if (kiemtra == true)
                    {
                        btnreset_Click(sender, e);
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
                    MessageBox.Show("Mã danh mục tin tức này không tồn tại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtmadanhmuc.Text = "";
            txttendanhmuc.Text = "";
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            madanhmucsanpham = "SELECT madanhmucsanpham from danhmucsanpham where madanhmucsanpham = '" + cbDanhmucsanpham.SelectedValue + "'";
            if (conn.CheckKey(madanhmucsanpham))
            {
                MessageBox.Show("Bạn đã tìm kiếm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ds = conn.select("select madanhmucsanpham as 'Mã danh mục sản phẩm',tendanhmucsanpham as 'Tên danh mục sản phẩm' from danhmucsanpham where madanhmucsanpham like N'%" + cbDanhmucsanpham.SelectedValue.ToString() + "%' ");
                dgvdanhmucsanpham.DataSource = ds.Tables[0];
                btnreset_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Mã khách hàng này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            KetQua();
        }

        private void dgvdanhmucsanpham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int _row = e.RowIndex;
            txtmadanhmuc.Text = dgvdanhmucsanpham.Rows[_row].Cells[0].Value.ToString();
            txttendanhmuc.Text = dgvdanhmucsanpham.Rows[_row].Cells[1].Value.ToString();
        }

        private void ProductPortfolio_Load(object sender, EventArgs e)
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
