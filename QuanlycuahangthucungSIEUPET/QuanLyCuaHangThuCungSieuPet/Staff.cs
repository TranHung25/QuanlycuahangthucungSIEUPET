using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyCuaHangThuCungSieuPet
{
    public partial class Staff : Form
    {
        public static string quyen;
        public static string manhanvien;
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
        public void show()
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            ds = conn.select("SELECT Staff.ID_Staff as 'ID Nhân Viên', Staff.StaffName as 'Tên Nhân Viên',Staff.Image as 'Ảnh', Staff.DateOfBrith as 'Ngày Sinh',  Staff.Address as 'Địa Chỉ', Staff.PhoneNumber as 'Số Điện Thoại', Staff.Gender as 'Giới Tính', Staff.DayToWork as 'Ngày vào làm', Staff.CMTND as 'CMTND' FROM Staff");
            dgvnhanvien.DataSource = ds.Tables[0];
            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dgvnhanvien.Columns[2];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dgvnhanvien.RowTemplate.Height = 80;
            dgvnhanvien.Columns[0].Width = 50;
            dgvnhanvien.Columns[1].Width = 130;



        }
        public void load()
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            ds = conn.select("select * from Staff");
            cbnhanvien.DataSource = ds.Tables[0];
            cbnhanvien.ValueMember = "ID_Staff";


        }
        public Staff()
        {
            InitializeComponent();
            load();
            show();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn hình ảnh cho nhân viên";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = new Bitmap(dlgOpen.FileName);
            }
        }
        private string getGioiTinh()
        {
            if (radioNam.Checked == true && radioNu.Checked == false)
                return "Nam";
            else if (radioNam.Checked == false && radioNu.Checked == true)
                return "Nữ";
            else
                return "";
        }
        private void SetGioitinh(string gioitinh)
        {
            if (gioitinh == "Nữ")
            {
                radioNam.Checked = false;
                radioNu.Checked = true;
            }
            else
            {
                radioNam.Checked = true;
                radioNu.Checked = false;
            }
        }
        private void btnHienthi_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            ds = conn.select("SELECT Staff.ID_Staff as 'ID Nhân Viên', Staff.StaffName as 'Tên Nhân Viên',Staff.Image as 'Ảnh', Staff.DateOfBrith as 'Ngày Sinh',  Staff.Address as 'Địa Chỉ', Staff.PhoneNumber as 'Số Điện Thoại', Staff.Gender as 'Giới Tính', Staff.DayToWork as 'Ngày vào làm', Staff.CMTND as 'CMTND' FROM Staff");
            dgvnhanvien.DataSource = ds.Tables[0];
        }

        private void dgvnhanvien_Click(object sender, EventArgs e)
        {
            txtMaNhanVien.Text = dgvnhanvien.CurrentRow.Cells[0].Value.ToString();
            txttennhanvien.Text = dgvnhanvien.CurrentRow.Cells[1].Value.ToString();
            byte[] image = (byte[])dgvnhanvien.CurrentRow.Cells[2].Value;
            MemoryStream ms = new MemoryStream(image);
            picAnh.Image = Image.FromStream(ms);
            timengaysinh.Text = dgvnhanvien.CurrentRow.Cells[3].Value.ToString();
            txtDiaChi.Text = dgvnhanvien.CurrentRow.Cells[4].Value.ToString();
            txtSDT.Text = dgvnhanvien.CurrentRow.Cells[5].Value.ToString();
            SetGioitinh(dgvnhanvien.CurrentRow.Cells[6].Value.ToString());
            timengaylam.Text = dgvnhanvien.CurrentRow.Cells[7].Value.ToString();
            txtcmtnd.Text = dgvnhanvien.CurrentRow.Cells[8].Value.ToString();
          
        }
    
        private void btnThem_Click(object sender, EventArgs e)
        {
            long n;
            if (txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Hãy nhập mã nhân viên", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txttennhanvien.Text == "")
            {
                MessageBox.Show("Hãy nhập tên nhân viên", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtcmtnd.Text == "")
            {
                MessageBox.Show("Hãy nhập chứng minh thư nhân dân", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (radioNam.Checked==false && radioNu.Checked==false)
            {
                MessageBox.Show("Hãy chọn giới tính của nhân viên", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtSDT.Text == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại của nhân viên", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtSDT.Text.Length > 11 || txtSDT.Text.Length < 10)
            {
                MessageBox.Show("Số điện thoại phải nhập từ 10 đến 11 số", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (long.TryParse(txtcmtnd.Text, out n) && long.TryParse(txtSDT.Text, out n))
                {
                    manhanvien = "SELECT ID_Staff from Staff where ID_Staff = '" + txtMaNhanVien.Text.Trim() + "'";
                    if (CheckKey(manhanvien))
                    {
                        MessageBox.Show("Mã nhân viên này đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        string GioiTinh = getGioiTinh();
                        conn.Open();
                        string sql = "insert into Staff(ID_Staff,StaffName,Image,DateOfBrith,Address,PhoneNumber,Gender,DayToWork,CMTND) values (@ID_Staff,@StaffName,@Image,@DateOfBrith,@Address,@PhoneNumber,@Gender,@DayToWork,@CMTND)";
                        command = new SqlCommand(sql, conn);
                        command.Parameters.AddWithValue("@ID_Staff", txtMaNhanVien.Text);
                        command.Parameters.AddWithValue("@StaffName", txttennhanvien.Text);
                        command.Parameters.AddWithValue("@Image", SavePhoto());
                        command.Parameters.AddWithValue("@DateOfBrith", timengaysinh.Value);
                        command.Parameters.AddWithValue("@Address", txtDiaChi.Text);
                        command.Parameters.AddWithValue("@PhoneNumber", txtSDT.Text);
                        command.Parameters.AddWithValue("@Gender", GioiTinh.Trim());
                        command.Parameters.AddWithValue("@DayToWork", timengaylam.Value);
                        command.Parameters.AddWithValue("@CMTND", txtcmtnd.Text);
                        command.ExecuteNonQuery();
                        conn.Close();
                        btnLamMoi_Click(sender, e);
                        load();
                        show();
                        MessageBox.Show("Save Successfully!");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Hãy nhập chính xác các thông tin", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            picAnh.Image.Save(ms, picAnh.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            long n;
            if (txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Hãy nhập mã nhân viên", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txttennhanvien.Text == "")
            {
                MessageBox.Show("Hãy nhập tên nhân viên", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtcmtnd.Text == "")
            {
                MessageBox.Show("Hãy nhập chứng minh thư nhân dân", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (radioNam.Checked == false && radioNu.Checked == false)
            {
                MessageBox.Show("Hãy chọn giới tính của nhân viên", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtSDT.Text == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại của nhân viên", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtSDT.Text.Length > 11 || txtSDT.Text.Length < 10)
            {
                MessageBox.Show("Số điện thoại phải nhập từ 10 đến 11 số", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (long.TryParse(txtcmtnd.Text, out n) && long.TryParse(txtSDT.Text, out n))
                {
                    manhanvien = "SELECT ID_Staff from Staff where ID_Staff = '" + txtMaNhanVien.Text.Trim() + "'";
                    if (CheckKey(manhanvien))
                    {
                        string GioiTinh = getGioiTinh();
                        conn.Open();
                        string sql = "update Staff set StaffName=@StaffName,Image=@Image,DateOfBrith=@DateOfBrith,Address=@Address,PhoneNumber=@PhoneNumber,Gender=@Gender,DayToWork=@DayToWork,CMTND=@CMTND where ID_Staff='" + txtMaNhanVien.Text + "'";
                        command = new SqlCommand(sql, conn);
                        command.Parameters.AddWithValue("@ID_Staff", txtMaNhanVien.Text);
                        command.Parameters.AddWithValue("@StaffName", txttennhanvien.Text);
                        command.Parameters.AddWithValue("@Image", SavePhoto());
                        command.Parameters.AddWithValue("@DateOfBrith", timengaysinh.Value);
                        command.Parameters.AddWithValue("@Address", txtDiaChi.Text);
                        command.Parameters.AddWithValue("@PhoneNumber", txtSDT.Text);
                        command.Parameters.AddWithValue("@Gender", GioiTinh.Trim());
                        command.Parameters.AddWithValue("@DayToWork", timengaylam.Value);
                        command.Parameters.AddWithValue("@CMTND", txtcmtnd.Text);
                        command.ExecuteNonQuery();
                        conn.Close();
                        btnLamMoi_Click(sender, e);
                        load();
                        show();
                        MessageBox.Show("Save Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Mã nhân viên này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("Hãy nhập chính xác các thông tin", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            manhanvien = "SELECT ID_Staff from Staff where ID_Staff = '" + txtMaNhanVien.Text.Trim() + "'";
            if (conn.CheckKey(manhanvien))
            {
                bool b = conn.update("delete Staff where ID_Staff = '" + txtMaNhanVien.Text + "'");
                if (b == true)
                {
                    btnLamMoi_Click(sender, e);
                    MessageBox.Show("Xoá thành công");
                    load();
                    show();
                }
                else
                {
                    MessageBox.Show("Xoá thất bại");
                }
            }
            else
            {
                MessageBox.Show("Mã nhân viên này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            DataSet ds = new DataSet();
            manhanvien = "SELECT ID_Staff from Staff where ID_Staff = '" + cbnhanvien.SelectedValue + "'";
            if (conn.CheckKey(manhanvien))
            {
                MessageBox.Show("Bạn đã tìm kiếm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ds = conn.select("SELECT Staff.ID_Staff as 'ID Nhân Viên', Staff.StaffName as 'Tên Nhân Viên',Staff.Image as 'Ảnh', Staff.DateOfBrith as 'Ngày Sinh',  Staff.Address as 'Địa Chỉ', Staff.PhoneNumber as 'Số Điện Thoại', Staff.Gender as 'Giới Tính', Staff.DayToWork as 'Ngày vào làm', Staff.CMTND as 'CMTND' FROM Staff where ID_Staff like N'%" + cbnhanvien.SelectedValue.ToString() + "%'");
                btnLamMoi_Click(sender, e);
                dgvnhanvien.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Mã sản phẩm này không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                show();
            }
          
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNhanVien.Text = "";
            txttennhanvien.Text = "";
            timengaysinh.ResetText();
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            radioNam.Checked = false;
            radioNu.Checked = false;
            txtcmtnd.Text = "";
            timengaylam.ResetText();
            show();
        }

        private void Staff_Load(object sender, EventArgs e)
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

        private void picAnh_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void export2Excel(DataGridView g, string duongDan, string tenTap)
        {

            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            for (int i = 1; i < g.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = g.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < g.Rows.Count - 1; i++)
            {
                for (int j = 0; j < g.Columns.Count; j++)
                {
                    if (g.Rows[i].Cells[j].Value != null)
                    {
                        worksheet.Cells[i + 2, j + 1] = g.Rows[i].Cells[j].Value.ToString();
                    }
                    else
                    {
                        worksheet.Cells[i + 2, j + 1] = "";
                    }
                }
            }
        }
        private void btnxuatds_Click(object sender, EventArgs e)
        {
            export2Excel(dgvnhanvien, @"D:\", "Webthucung");
        }
    }
}
