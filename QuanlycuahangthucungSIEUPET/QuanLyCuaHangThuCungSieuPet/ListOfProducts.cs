using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
    public partial class ListOfProducts : Form
    {
        public static string quyen;
        public static string masanpham;
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

        public ListOfProducts()
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            Connect conn = new Connect();
            ds = conn.select("select * from danhmucsanpham");
            cbDanhMuc.DataSource = ds.Tables[0];
            cbDanhMuc.ValueMember = "madanhmucsanpham";
            cbDanhMuc.DisplayMember = "tendanhmucsanpham";

            ds = conn.select("select * from nhacungcap");
            cbncc.DataSource = ds.Tables[0];
            cbncc.ValueMember = "manhacungcap";
            cbncc.DisplayMember = "tennhacungcap";

            ds = conn.select("select * from sanpham");
            cbmasanpham.DataSource = ds.Tables[0];
            cbmasanpham.ValueMember = "masanpham";

            KetQua();
        }

        public void KetQua()
        {
            DataSet ds = new DataSet();
            Connect conn = new Connect();
            ds = conn.select("select sanpham.masanpham as 'Mã sản phẩm', tensanpham as 'Tên nhà sản phẩm', danhmucsanpham.tendanhmucsanpham as 'Danh muc sản phẩm',giasanpham as 'Giá sản phẩm', mota as 'Mô tả', nhacungcap.tennhacungcap as 'Tên nhà cung cấp', sanpham.anhsanpham as 'Ảnh sản phẩm',sanpham.soluong from nhacungcap, sanpham, danhmucsanpham where nhacungcap.manhacungcap= sanpham.manhacungcap and sanpham.madanhmucsanpham=danhmucsanpham.madanhmucsanpham");
            dvgSanPham.DataSource = ds.Tables[0];
            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dvgSanPham.Columns[6];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dvgSanPham.RowTemplate.Height = 80;

        }

        private void ListOfProducts_Load(object sender, EventArgs e)
        {
            danhSáchHoáĐơnToolStripMenuItem.Visible = false;
            if (quyen == "User")
            {
                quảnLýNhânViênToolStripMenuItem.Visible = false;
            }
        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            ds = conn.select("select sanpham.masanpham as 'Mã sản phẩm', tensanpham as 'Tên nhà sản phẩm', danhmucsanpham.tendanhmucsanpham as 'Danh muc sản phẩm',giasanpham as 'Giá sản phẩm', mota as 'Mô tả', nhacungcap.tennhacungcap as 'Tên nhà cung cấp', sanpham.anhsanpham as 'Ảnh sản phẩm',soluong as 'Số lượng' from nhacungcap, sanpham, danhmucsanpham where nhacungcap.manhacungcap= sanpham.manhacungcap and sanpham.madanhmucsanpham=danhmucsanpham.madanhmucsanpham");
            dvgSanPham.DataSource = ds.Tables[0];
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            long n;
            if (txtMaSanPham.Text == "")
            {
                MessageBox.Show("Hãy nhập mã sản phẩm", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtTenSanPham.Text == "")
            {
                MessageBox.Show("Hãy nhập tên tên sản phẩm", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtSoluong.Text == "")
            {
                MessageBox.Show("Hãy nhập số lượng sản phẩm", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtMoTa.Text == "")
            {
                MessageBox.Show("Hãy nhập mô tả sản phẩm", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtDonGia.Text == "")
            {
                MessageBox.Show("Hãy nhập đơn giá cho sản phẩm", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (long.TryParse(txtDonGia.Text, out n) && long.TryParse(txtSoluong.Text, out n))
                {

                    masanpham = "SELECT masanpham from sanpham where masanpham = '" + txtMaSanPham.Text.Trim() + "'";

                    if (CheckKey(masanpham))
                    {
                        MessageBox.Show("Mã sản phẩm này đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        conn.Open();
                        string sql = "insert into sanpham(masanpham,tensanpham,madanhmucsanpham,giasanpham,mota,manhacungcap,anhsanpham,soluong) values (@masanpham,@tensanpham,@madanhmucsanpham,@giasanpham,@mota,@manhacungcap,@anhsanpham,@soluong)";
                        command = new SqlCommand(sql, conn);
                        command.Parameters.AddWithValue("@masanpham", txtMaSanPham.Text);
                        command.Parameters.AddWithValue("@tensanpham", txtTenSanPham.Text);
                        command.Parameters.AddWithValue("@madanhmucsanpham", cbDanhMuc.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@giasanpham", txtDonGia.Text);
                        command.Parameters.AddWithValue("@mota", txtMoTa.Text);
                        command.Parameters.AddWithValue("@manhacungcap", cbncc.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@anhsanpham", SavePhoto());
                        command.Parameters.AddWithValue("@soluong", txtSoluong.Text);
                        command.ExecuteNonQuery();
                        conn.Close();
                        btnreset_Click(sender, e);
                        KetQua();
                        MessageBox.Show("Save Successfully!");
                    }
                }
                else
                {
                    MessageBox.Show("Hãy nhập chính xác số điện thoại là số hoặc số lượng là số", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }


            
        }
        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            picSanpham.Image.Save(ms, picSanpham.Image.RawFormat);
            return ms.GetBuffer();
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            long n;
            if (txtMaSanPham.Text == "")
            {
                MessageBox.Show("Hãy nhập mã sản phẩm", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtTenSanPham.Text == "")
            {
                MessageBox.Show("Hãy nhập tên tên sản phẩm", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtSoluong.Text == "")
            {
                MessageBox.Show("Hãy nhập số lượng sản phẩm", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtMoTa.Text == "")
            {
                MessageBox.Show("Hãy nhập mô tả sản phẩm", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtDonGia.Text == "")
            {
                MessageBox.Show("Hãy nhập đơn giá cho sản phẩm", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (long.TryParse(txtDonGia.Text, out n) && long.TryParse(txtSoluong.Text, out n))
                {

                    masanpham = "SELECT masanpham from sanpham where masanpham = '" + txtMaSanPham.Text.Trim() + "'";

                    if (CheckKey(masanpham))
                    {
                        conn.Open();
                        string sql = "update sanpham set tensanpham=@tensanpham,madanhmucsanpham=@madanhmucsanpham,giasanpham=@giasanpham,mota=@mota,manhacungcap=@manhacungcap,anhsanpham=@anhsanpham,soluong=@soluong where masanpham='" + txtMaSanPham.Text + "'";
                        command = new SqlCommand(sql, conn);
                        command.Parameters.AddWithValue("@tensanpham", txtTenSanPham.Text);
                        command.Parameters.AddWithValue("@madanhmucsanpham", cbDanhMuc.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@giasanpham", txtDonGia.Text);
                        command.Parameters.AddWithValue("@mota", txtMoTa.Text);
                        command.Parameters.AddWithValue("@manhacungcap", cbncc.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@anhsanpham", SavePhoto());
                        command.Parameters.AddWithValue("@soluong", txtSoluong.Text);
                        command.ExecuteNonQuery();
                        conn.Close();
                        btnreset_Click(sender, e);
                        KetQua();
                        MessageBox.Show("Save Successfully!");
    
                    }
                    else
                    {
                        MessageBox.Show("Mã sản phẩm này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Hãy nhập chính xác số điện thoại là số hoặc số lượng là số", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            masanpham = "SELECT masanpham from sanpham where masanpham = '" + txtMaSanPham.Text.Trim() + "'";
            if (conn.CheckKey(masanpham))
            {
                bool b = conn.update("delete sanpham where masanpham = '" + txtMaSanPham.Text + "'");
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
                MessageBox.Show("Mã sản phẩm này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }  
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtMaSanPham.Text = "";
            txtTenSanPham.Text = "";
            picSanpham.Image = Properties.Resources.thucung1;
            txtDonGia.Text = "";
            txtMoTa.Text = "";
            txtSoluong.Text = "";
            DataSet ds = new DataSet();
            Connect conn = new Connect();
            ds = conn.select("select * from danhmucsanpham");
            cbDanhMuc.DataSource = ds.Tables[0];
            cbDanhMuc.ValueMember = "madanhmucsanpham";
            cbDanhMuc.DisplayMember = "tendanhmucsanpham";

            ds = conn.select("select * from nhacungcap");
            cbncc.DataSource = ds.Tables[0];
            cbncc.ValueMember = "manhacungcap";
            cbncc.DisplayMember = "tennhacungcap";
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            masanpham = "SELECT masanpham from sanpham where masanpham = '" + cbmasanpham.SelectedValue + "'";
            if (conn.CheckKey(masanpham))
            {
                MessageBox.Show("Bạn đã tìm kiếm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ds = conn.select("select sanpham.masanpham as 'Mã sản phẩm', tensanpham as 'Tên nhà sản phẩm', danhmucsanpham.tendanhmucsanpham as 'Danh muc sản phẩm',giasanpham as 'Giá sản phẩm', mota as 'Mô tả', nhacungcap.tennhacungcap as 'Tên nhà cung cấp', sanpham.anhsanpham as 'Ảnh sản phẩm',soluong as 'Số lượng' from nhacungcap, sanpham, danhmucsanpham where nhacungcap.manhacungcap= sanpham.manhacungcap and sanpham.madanhmucsanpham=danhmucsanpham.madanhmucsanpham and masanpham like N'%" + cbmasanpham.SelectedValue.ToString() + "%'");
                dvgSanPham.DataSource = ds.Tables[0];
                btnreset_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Mã sản phẩm này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                KetQua();
            }
        }

        private void dvgSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSanPham.Text = dvgSanPham.CurrentRow.Cells[0].Value.ToString();
            txtTenSanPham.Text = dvgSanPham.CurrentRow.Cells[1].Value.ToString();
            cbDanhMuc.Text = dvgSanPham.CurrentRow.Cells[2].Value.ToString();
            txtDonGia.Text = dvgSanPham.CurrentRow.Cells[3].Value.ToString();
            txtMoTa.Text = dvgSanPham.CurrentRow.Cells[4].Value.ToString();
            cbncc.Text = dvgSanPham.CurrentRow.Cells[5].Value.ToString();
            byte[] image = (byte[])dvgSanPham.CurrentRow.Cells[6].Value;
            MemoryStream ms = new MemoryStream(image);
            picSanpham.Image = Image.FromStream(ms);
            txtSoluong.Text = dvgSanPham.CurrentRow.Cells[7].Value.ToString();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn hình ảnh cho nhân viên";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picSanpham.Image = new Bitmap(dlgOpen.FileName);
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

        private void cbDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void picSanpham_Click(object sender, EventArgs e)
        {

        }
    }
}
