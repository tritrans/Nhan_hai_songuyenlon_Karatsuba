using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BaiToanNhanSoNguyenLon
{
    public partial class Nhan2SoNguyenLon : Form
    {
        public Nhan2SoNguyenLon()
        {
            InitializeComponent();
           
        }

        private void txtA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;

        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            if (txtA.Text != "" && txtB.Text != "")
            {
                SoNguyenLon a = new SoNguyenLon(txtA.Text, 1);
                SoNguyenLon b = new SoNguyenLon(txtB.Text, 1);

                // Đo thời gian cho thuật toán Karatsuba
                Stopwatch stopwatch = Stopwatch.StartNew();
                SoNguyenLon resultKaratsuba = SoNguyenLon.nhanSoNguyenLon(a, b); // Sử dụng hàm Karatsuba
                stopwatch.Stop();
                long timeKaratsuba = stopwatch.ElapsedMilliseconds;

                txtKetQua.Text = SoNguyenLon.nhanSoNguyenLon(a,b).xuat();

                // Hiển thị kết quả lên DataGridView
                dataGridView1.Rows.Add("Chia để trị", timeKaratsuba, txtKetQua.Text);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa không????", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
               
                txtKetQua.Text = "";
                txtB.Text = "";
                txtA.Text = "";

                
                txt_CSA.Text = ""; 
                txt_CSB.Text = ""; 
               
                checkBox1.Checked = false;     
                txtA.Focus();

                dataGridView1.Rows.Clear();
            }
                
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có thực sự muốn thoát không???", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question); ;
            if (r == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void txtA_Leave(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                
                return; 
            }

        
           else if (string.IsNullOrWhiteSpace(txtA.Text))
            {
                errorProvider1.SetError(txtA, "Bạn không được để trống!!");
                txtA.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }
           
        }

        private void BTN_RANDOM_Click(object sender, EventArgs e)
        {
           
            // Xóa các giá trị trong txtA và txtB khi nhấn nút random
            txtA.Text = "";
            txtB.Text = "";
            txtA.Focus();
            dataGridView1.Rows.Clear();

            // Kiểm tra số chữ số cho A và B
            if (int.TryParse(txt_CSA.Text, out int soChuSoA) && int.TryParse(txt_CSB.Text, out int soChuSoB))
            {
                // Kiểm tra nếu số chữ số hợp lệ (phải lớn hơn 0)
                if (soChuSoA > 0 && soChuSoB > 0)
                {
                    // Sinh số ngẫu nhiên cho A và B, đảm bảo khác nhau
                    var (a, b) = SoNguyenLon.RandomHaiSoNguyenLonKhacNhau(soChuSoA);

                    // Cập nhật vào TextBox
                    txtA.Text = a.xuat();
                    txtB.Text = b.xuat();
                }
                else
                {
                    MessageBox.Show("Số chữ số phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số chữ số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void Nhan2SoNguyenLon_Load(object sender, EventArgs e)
        {
            txt_CSA.Enabled = false;
            txt_CSB.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
     
            if (checkBox1.Checked)
            {
          
                txt_CSA.Enabled = true;
                txt_CSB.Enabled = true;

               
                txtA.Text = "";
                txtB.Text = "";
  
            }
            else
            {
                txt_CSA.Enabled = false;
                txt_CSB.Enabled = false;

              
                txtA.Text = "";
                txtB.Text = "";
            }
        }

        private void btn_FFT_Click(object sender, EventArgs e)
        {
            if (txtA.Text != "" && txtB.Text != "")
            {
                SoNguyenLon a = new SoNguyenLon(txtA.Text, 1);
                SoNguyenLon b = new SoNguyenLon(txtB.Text, 1);

                Stopwatch stopwatch = Stopwatch.StartNew();
                SoNguyenLon resultFFT = SoNguyenLon.nhanSoNguyenLonFFT(a, b); // Hàm nhân FFT (cần triển khai)
                stopwatch.Stop();

                long timeFFT = stopwatch.ElapsedMilliseconds;

                txtKetQua.Text = SoNguyenLon.nhanSoNguyenLonFFT(a, b).xuat();

                // Hiển thị kết quả lên DataGridView
                dataGridView1.Rows.Add("FFT", timeFFT, txtKetQua.Text);
            }
        }
    }
}
