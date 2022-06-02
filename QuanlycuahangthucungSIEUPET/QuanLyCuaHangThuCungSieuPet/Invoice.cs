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
    public partial class Invoice : Form
    {
        public static string quyen;
        public static string MaHD;
        public string macthd;
        public static string soluongsanpham;
        Connect conn = new Connect();
        DataSet ds = new DataSet();
        public Invoice()
        {
            InitializeComponent();
        }

        private void CustomerManagement_Load(object sender, EventArgs e)
        {
            if (quyen == "User")
            {
                quảnLýNhânViênToolStripMenuItem.Enabled = false;
            }
        }
        private void Invoice_Load(object sender, EventArgs e)
        {
            danhSáchHoáĐơnToolStripMenuItem.Visible = false;
            if (quyen == "User")
            {
                quảnLýNhânViênToolStripMenuItem.Visible = false;
            }
            dgvSanPham.DataSource = conn.XemDL("select MaCTHD, tensanpham, CTHD.soluong, thanhtien from hoadon, CTHD, sanpham where hoadon.mahoadon = CTHD.mahoadon and CTHD.masanpham=sanpham.masanpham and hoadon.mahoadon='" + MaHD + "'");
            txtTongTien.Text = conn.XemDL("select sum(thanhtien) as tongtien from CTHD where mahoadon='" + MaHD + "'").Rows[0][0].ToString();
            conn.ThucThiDl("update hoadon set tongtien='" + Convert.ToInt32(txtTongTien.Text.Trim()) + "' where mahoadon='" + MaHD + "'");
            btnLuuHoaDon.Enabled = false;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string masp = conn.XemDL("select masanpham from sanpham where tensanpham =N'" + txtTenSP.Text.Trim() + "'").Rows[0][0].ToString().Trim();

            float tien = Convert.ToInt32(conn.XemDL("select giasanpham from sanpham where masanpham='" + masp + "'").Rows[0][0].ToString().Trim()) * Convert.ToInt32(txtSoLuong.Text.Trim());
            
            conn.ThucThiDl("update CTHD set soluong ='" + Convert.ToInt32(txtSoLuong.Text.ToString()) + "', thanhtien='" + tien + "' where MaCTHD='" + txtCTHD.Text.ToString() + "' and  mahoadon ='" + MaHD + "' and  masanpham ='" + masp + "'");
           
            dgvSanPham.DataSource = conn.XemDL("select MaCTHD, tensanpham, CTHD.soluong, thanhtien from hoadon, CTHD, sanpham where hoadon.mahoadon=CTHD.mahoadon and CTHD.masanpham=sanpham.masanpham and hoadon.mahoadon='" + MaHD + "'");
           
            txtTongTien.Text = conn.XemDL("select sum(thanhtien) as tongtien from CTHD where mahoadon='" + MaHD + "'").Rows[0][0].ToString();

            conn.ThucThiDl("update hoadon set tongtien='" + Convert.ToInt32(txtTongTien.Text.Trim()) + "' where mahoadon='" + MaHD + "'");

            MessageBox.Show("Cập nhật thành công");
            btnLuuHoaDon.Enabled = true;
        }

        private void btnLuuHoaDon_Click(object sender, EventArgs e)
        {
            
            float SoTienKhachHangTra = Convert.ToInt32(txtsotienkhachhangtra.Text.Trim());
            float TongTienPhaiTra = Convert.ToInt32(txtTongTien.Text.Trim());
            float SoTienTraLaiKhachHang =  SoTienKhachHangTra - TongTienPhaiTra;
            if(SoTienKhachHangTra < TongTienPhaiTra)
            {
                MessageBox.Show("Bạn cần trả với số tiền lớn hơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                txtsotientralaikhachhang.Text = SoTienTraLaiKhachHang.ToString();
                MessageBox.Show("Đã Thanh Toán Thành Công");
            }
            
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int _row = e.RowIndex;
            txtCTHD.Text = dgvSanPham.Rows[_row].Cells[0].Value.ToString();
            txtTenSP.Text = dgvSanPham.Rows[_row].Cells[1].Value.ToString();
            txtSoLuong.Text = dgvSanPham.Rows[_row].Cells[2].Value.ToString();
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

        private void txtsotientralaikhachhang_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenSP_TextChanged(object sender, EventArgs e)
        {

        }
      
        private void btnxuathoadon_Click(object sender, EventArgs e)
        {
           
        }
    }
}
