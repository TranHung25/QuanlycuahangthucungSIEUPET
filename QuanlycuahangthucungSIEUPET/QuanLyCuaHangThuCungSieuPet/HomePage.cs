using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyCuaHangThuCungSieuPet
{
    public partial class HomePage : Form
    {
        public static string quyen;
        public static string soluongnhanvien;
        public static string slkh;
        public static string sltt;
        public static string slsp;
        public static string tongtien;
        public static string username;
        public HomePage()
        {
            InitializeComponent();
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            soluongnhanvien = conn.XemDL("SELECT count(*) as nhanvien from Staff").Rows[0][0].ToString().Trim();
            lbnhanvien.Text = "Có " + soluongnhanvien + " nhân viên";
            slsp = conn.XemDL("SELECT count(*) as sanpham from sanpham").Rows[0][0].ToString().Trim();
            lbsanpham.Text = "Có " + slsp + " sản phẩm";
            slkh = conn.XemDL("SELECT count(*) as khachhang from khachhang").Rows[0][0].ToString().Trim();
            lbkhachhang.Text = "Có " + slkh + " khách hàng";
            sltt = conn.XemDL("SELECT count(*) as tintuc from Tintuc").Rows[0][0].ToString().Trim();
            lbtintuc.Text = "Có " + sltt + " tin tức";
            username = conn.XemDL("select UserName from UserAccount").Rows[0][0].ToString().Trim();
            lbUserName.Text = "Xin chào " + username + " đã đến với cửa hàng LapTop HKN";
            ds = conn.select("select hoadon.mahoadon as 'Mã hoá đơn',tongtien as 'Tổng tiền' from hoadon");
            dgvthongketongtien.DataSource = ds.Tables[0];
            tongtien = conn.XemDL("select sum(tongtien) as 'Tổng tiền' from hoadon").Rows[0][0].ToString().Trim();
            txtTongtien.Text = tongtien;
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            if(quyen == "User")
            {
                quảnLýNhânViênToolStripMenuItem.Enabled = false;
            }
            btnNext.Visible = false;
            btnstart.Visible = false;
            btPrevious.Visible = false;
            danhSáchHoáĐơnToolStripMenuItem.Visible = false;
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CustomerManagement customer = new CustomerManagement();
            this.Hide();
            customer.Show();
        }

        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomePage home = new HomePage();
            this.Hide();
            home.Show();
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

        private string[] FolderFile = null;
        private int Selected = 0;
        private int Begin = 0;
        private int End = 0;
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if(folder.ShowDialog() == DialogResult.OK)
            {
                string[] path1 = null, path2 = null, path3 = null;
                path1 = Directory.GetFiles(folder.SelectedPath, "*.jpg");
                path2 = Directory.GetFiles(folder.SelectedPath, "*.jpeg");
                path3 = Directory.GetFiles(folder.SelectedPath, "*.bmp");
                FolderFile = new string[path1.Length + path2.Length + path3.Length];
                Array.Copy(path1, 0, FolderFile, 0, path1.Length);
                Array.Copy(path2, 0, FolderFile, path1.Length, path2.Length);
                Array.Copy(path3, 0, FolderFile, path1.Length + path2.Length, path3.Length);
                Selected = 0;
                Begin = 0;
                End = FolderFile.Length;
                showImage(FolderFile[Selected]);
                btPrevious.Visible = true;
                btnNext.Visible = true;
                btnstart.Visible = true;
            }

        }
        private void showImage(string path)
        {
            Image imgtemp = Image.FromFile(path);
            picImageSlideShow.Image = imgtemp;
        }

        private void btPrevious_Click(object sender, EventArgs e)
        {
            if (Selected == 0)
            {
                Selected = FolderFile.Length - 1;
                showImage(FolderFile[Selected]);
            }
            else
            {
                Selected = Selected - 1;
                showImage(FolderFile[Selected]);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Selected == FolderFile.Length - 1)
            {
                Selected = 0;
                showImage(FolderFile[Selected]);
            }
            else
            {
                Selected = Selected + 1;
                showImage(FolderFile[Selected]);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnNext_Click(sender, e);
        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                timer1.Enabled = false;
                btnstart.Text = "Start Slide Show";
            }
            else
            {
                timer1.Enabled = true;
                btnstart.Text = "Stop Slide Show";
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Staff nv = new Staff();
            this.Hide();
            nv.Show();
        }

        private void lbsoluongsanpham_Click(object sender, EventArgs e)
        {
            ListOfProducts dssp = new ListOfProducts();
            this.Hide();
            dssp.Show();
        }

        private void lbsoluongkhachhang_Click(object sender, EventArgs e)
        {
            CustomerManagement dssp = new CustomerManagement();
            this.Hide();
            dssp.Show();
        }

        private void lbsoluongtintuc_Click(object sender, EventArgs e)
        {
            NewsList dssp = new NewsList();
            this.Hide();
            dssp.Show();
        }
    }
}
