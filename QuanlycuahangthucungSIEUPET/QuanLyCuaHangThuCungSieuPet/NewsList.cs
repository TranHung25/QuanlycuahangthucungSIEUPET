using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyCuaHangThuCungSieuPet
{
    public partial class NewsList : Form
    {
        public static string quyen;
        public static string matintuc;
        SqlConnection conn = new SqlConnection(@"Data Source=TRANHUNG;Initial Catalog=QuanLyCuaHangThuCungSieuPet;Integrated Security=True");
        SqlCommand command;
        public bool CheckKey(string sql)
        {
            SqlDataAdapter MyData = new SqlDataAdapter(sql, conn);
            DataTable table = new DataTable();
            MyData.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }
        public NewsList()
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            Connect conn = new Connect();
            ds = conn.select("select * from Danhmuctintuc");
            cbDanhmuctintuc.DataSource = ds.Tables[0];
            cbDanhmuctintuc.ValueMember = "madanhmuctintuc";
            cbDanhmuctintuc.DisplayMember = "tendanhmuctintuc";

            ds = conn.select("select * from Tintuc");
            cbTimkiem.DataSource = ds.Tables[0];
            cbTimkiem.ValueMember = "matintuc";
            
            KetQua();
        }
        public void KetQua()
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            ds = conn.select("select matintuc as 'Mã Tin Tức', tentintuc as 'Tên Tin Tức', Image as 'Ảnh Tin Tức', Danhmuctintuc.tendanhmuctintuc as 'Danh Mục Tin Tức', chitiet as 'Chi Tiết' from Tintuc,Danhmuctintuc where Danhmuctintuc.madanhmuctintuc=Tintuc.madanhmuctintuc");
            dgvtintuc.DataSource = ds.Tables[0];
            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dgvtintuc.Columns[2];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dgvtintuc.RowTemplate.Height = 80;
            
        }


        private void btnHienthi_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            ds = conn.select("select matintuc as 'Mã Tin Tức', tentintuc as 'Tên Tin Tức', Image as 'Ảnh Tin Tức', Danhmuctintuc.tendanhmuctintuc as 'Danh Mục Tin Tức', chitiet as 'Chi Tiết' from Tintuc,Danhmuctintuc where Danhmuctintuc.madanhmuctintuc=Tintuc.madanhmuctintuc");
            dgvtintuc.DataSource = ds.Tables[0];
            btnreset_Click(sender, e);

        }

        private void NewsList_Load(object sender, EventArgs e)
        {
            danhSáchHoáĐơnToolStripMenuItem.Visible = false;
            if (quyen == "User")
            {
                quảnLýNhânViênToolStripMenuItem.Visible = false;
            }
        }

        private void btnupload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn hình ảnh tin tức";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                pictureImage.Image = new Bitmap(dlgOpen.FileName);
            }
            else
            {
                MessageBox.Show("Hãy chọn file ảnh cho tin tức", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            if (txtMaTinTuc.Text == "")
            {
                MessageBox.Show("Hãy nhập mã tin tức", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtTenTinTuc.Text == "")
            {
                MessageBox.Show("Hãy nhập tên tin tức", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtChiTiet.Text == "")
            {
                MessageBox.Show("Hãy nhập chi tiết cho tin tức", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                
                matintuc = "SELECT matintuc from Tintuc where matintuc = '" + txtMaTinTuc.Text.Trim() + "'";
                if (CheckKey(matintuc))
                {
                    MessageBox.Show("Mã sản phẩm này đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    conn.Open();
                    string sql = "insert into Tintuc(matintuc,tentintuc,Image,madanhmuctintuc,chitiet) values (@matintuc,@tentintuc,@Image,@madanhmuctintuc,@chitiet)";
                    command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue("@matintuc", txtMaTinTuc.Text);
                    command.Parameters.AddWithValue("@tentintuc", txtTenTinTuc.Text);
                    command.Parameters.AddWithValue("@Image", SavePhoto());
                    command.Parameters.AddWithValue("@madanhmuctintuc", cbDanhmuctintuc.SelectedValue.ToString());
                    command.Parameters.AddWithValue("@chitiet", txtChiTiet.Text);
                    command.ExecuteNonQuery();
                    conn.Close();
                    btnreset_Click(sender, e);
                    KetQua();
                    MessageBox.Show("Save Successfully!");
                }
            }
        }
        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureImage.Image.Save(ms, pictureImage.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtMaTinTuc.Text = "";
            txtTenTinTuc.Text = "";
            pictureImage.Image = Properties.Resources.image02;
            DataSet ds = new DataSet();
            Connect conn = new Connect();
            ds = conn.select("select * from Danhmuctintuc");
            cbDanhmuctintuc.DataSource = ds.Tables[0];
            cbDanhmuctintuc.ValueMember = "madanhmuctintuc";
            cbDanhmuctintuc.DisplayMember = "tendanhmuctintuc";
            txtChiTiet.Text = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            matintuc = "SELECT matintuc from Tintuc where matintuc = '" + txtMaTinTuc.Text.Trim() + "'";

            if (conn.CheckKey(matintuc))
            {
                bool b = conn.update("delete Tintuc where matintuc = '" + txtMaTinTuc.Text + "'");
                if (b == true)
                {
                    btnreset_Click(sender, e);
                    MessageBox.Show("Xoá thành công");
                    KetQua();
                }
                else
                {
                    MessageBox.Show("Xoá thất bại");
                }
            }
            else
            {
                MessageBox.Show("Mã tin tức này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaTinTuc.Text == "")
            {
                MessageBox.Show("Hãy nhập mã tin tức", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtTenTinTuc.Text == "")
            {
                MessageBox.Show("Hãy nhập tên tin tức", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtChiTiet.Text == "")
            {
                MessageBox.Show("Hãy nhập chi tiết cho tin tức", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                matintuc = "SELECT matintuc from Tintuc where matintuc = '" + txtMaTinTuc.Text.Trim() + "'";
                if (CheckKey(matintuc))
                {
                    conn.Open();
                    string sql = "update Tintuc set tentintuc=@tentintuc,Image=@Image,madanhmuctintuc=@madanhmuctintuc,chitiet=@chitiet where matintuc='" + txtMaTinTuc.Text + "'";
                    command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue("@tentintuc", txtTenTinTuc.Text);
                    command.Parameters.AddWithValue("@Image", SavePhoto());
                    command.Parameters.AddWithValue("@madanhmuctintuc", cbDanhmuctintuc.SelectedValue.ToString());
                    command.Parameters.AddWithValue("@chitiet", txtChiTiet.Text);
                    command.ExecuteNonQuery();
                    conn.Close();
                    btnreset_Click(sender, e);
                    KetQua();
                    MessageBox.Show("Save Successfully!");
                }
                else
                {
                    MessageBox.Show("Mã tin tức này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
           
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            matintuc = "SELECT matintuc from Tintuc where matintuc = '" + cbTimkiem.SelectedValue + "'";
            if (conn.CheckKey(matintuc))
            {
                MessageBox.Show("Bạn đã tìm kiếm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ds = conn.select(" select matintuc as 'Mã Tin Tức', tentintuc as 'Tên Tin Tức', Image as 'Ảnh Tin Tức', Danhmuctintuc.tendanhmuctintuc as 'Danh Mục Tin Tức', chitiet as 'Chi Tiết' from Tintuc,Danhmuctintuc where Danhmuctintuc.madanhmuctintuc=Tintuc.madanhmuctintuc and matintuc like N'%" + cbTimkiem.SelectedValue.ToString()+ "%'");
                dgvtintuc.DataSource = ds.Tables[0];
                btnreset_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Mã tin tức này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                KetQua();
            }
          
        }

        private void dgvtintuc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaTinTuc.Text = dgvtintuc.CurrentRow.Cells[0].Value.ToString();
            txtTenTinTuc.Text = dgvtintuc.CurrentRow.Cells[1].Value.ToString();
            byte[] image = (byte[])dgvtintuc.CurrentRow.Cells[2].Value;
            MemoryStream ms = new MemoryStream(image);
            pictureImage.Image = Image.FromStream(ms);
            cbDanhmuctintuc.Text = dgvtintuc.CurrentRow.Cells[3].Value.ToString();
            txtChiTiet.Text = dgvtintuc.CurrentRow.Cells[4].Value.ToString();
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
