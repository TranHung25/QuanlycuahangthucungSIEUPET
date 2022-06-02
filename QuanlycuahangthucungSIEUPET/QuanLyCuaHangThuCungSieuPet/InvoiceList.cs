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
    public partial class InvoiceList : Form
    {
        public static string quyen;
        public static string mahoadon;
        public InvoiceList()
        {
            InitializeComponent();
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            ds = conn.select("select * from Staff");
            cbNhanvien.DataSource = ds.Tables[0];
            cbNhanvien.ValueMember = "ID_Staff";
            cbNhanvien.DisplayMember = "StaffName";

            ds = conn.select("select * from khachhang");
            cbkhanhhang.DataSource = ds.Tables[0];
            cbkhanhhang.ValueMember = "makh";
            cbkhanhhang.DisplayMember = "tenkh";
        }

        private void btnDathang_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            long n;
            if (txtMaHoaDon.Text == "")
            {
                MessageBox.Show("Hãy nhập mã hoá đơn", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtPhone.Text == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (lstchitiet.Items.Count <= 0)
            {
                MessageBox.Show("Hãy chọn sản phẩm", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (long.TryParse(txtPhone.Text, out n))
                {
                    mahoadon = "SELECT mahoadon from hoadon where mahoadon = '" + txtMaHoaDon.Text + "'";
                    if (conn.CheckKey(mahoadon))
                    {
                        MessageBox.Show("Mã hoá đơn này đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        conn.ThucThiDl("insert into hoadon values ('" + txtMaHoaDon.Text + "','" + cbNhanvien.SelectedValue + "','" + cbkhanhhang.SelectedValue + "','" + datetimeLHD.Value + "','" + 0 + "' )");
                        for (int i = 0; i < lstchitiet.Items.Count; i++)
                        {
                            string masp = conn.XemDL("select masanpham from sanpham where tensanpham = N'" + lstchitiet.Items[i].ToString().Trim() + "' ").Rows[0][0].ToString().Trim();
                            string tien = conn.XemDL("select giasanpham from sanpham where masanpham='" + masp + "'").Rows[0][0].ToString().Trim();
                            conn.ThucThiDl("insert into CTHD values ('" + txtMaHoaDon.Text.ToString().Trim() + "','" + masp + "','" + 1 + "','" + Convert.ToInt32(tien) + "')");
                            MessageBox.Show("Thêm thành công");
                        }
                        Invoice.MaHD = txtMaHoaDon.Text.Trim();
                        Invoice frm = new Invoice();
                        this.Hide();
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Hãy nhập chính xác số điện thoại là số", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
            
        }

        private void InvoiceList_Load(object sender, EventArgs e)
        {
            danhSáchHoáĐơnToolStripMenuItem.Visible = false;
            if (quyen == "User")
            {
                quảnLýNhânViênToolStripMenuItem.Visible = false;
            }
            
            Connect connect = new Connect();
            DataSet ds = new DataSet();
            ds = connect.select("Select * from sanpham");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < connect.XemDL("Select * from sanpham").Rows.Count; i++)
                {
                    lstSanPham.Items.Add(connect.XemDL("Select * from sanpham").Rows[i][1].ToString());
                }
            }
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            while (lstSanPham.SelectedItems.Count > 0)
            {
                lstchitiet.Items.Add(lstSanPham.SelectedItem);
                lstSanPham.Items.Remove(lstSanPham.SelectedItem);
            }
        }

        private void btnBoChon_Click(object sender, EventArgs e)
        {
            while (lstchitiet.SelectedItems.Count > 0)
            {
                lstSanPham.Items.Add(lstchitiet.SelectedItem);
                lstchitiet.Items.Remove(lstchitiet.SelectedItem);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lstchitiet.Text = "";
            txtMaHoaDon.Text = "";
            txtPhone.Text = "";
            txtDiaChi.Text = "";
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

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
