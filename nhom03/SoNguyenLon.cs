using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Numerics;

namespace BaiToanNhanSoNguyenLon
{
    class SoNguyenLon
    {

        public string soNguyen;
        public int n;
        public int dau; 

        public SoNguyenLon()
        {
            this.soNguyen = "";
            this.dau = 1; // mặc định là số dương
        }

        public SoNguyenLon(string soNguyen, int dau)
        {
            this.soNguyen = soNguyen.TrimStart('-'); // Bỏ dấu âm nếu có
            this.n = this.soNguyen.Length;
            this.dau = soNguyen.StartsWith("-") ? -1 : dau; // Xác định dấu từ chuỗi nếu có dấu âm
        }

        public static SoNguyenLon congHaiSoNguyenLon(SoNguyenLon A, SoNguyenLon B)
        {
            int nho = 0;

            int n = A.soNguyen.Length - B.soNguyen.Length;
            if (n > 0)
            {
                for (int i = 0; i < n; i++)
                    B.soNguyen = "0" + B.soNguyen;
            }
            else
            {
                for (int i = 0; i < Math.Abs(n); i++)
                    A.soNguyen = "0" + A.soNguyen;
            }

            string a = A.soNguyen, b = B.soNguyen, kq = "";

            int x, y;
            for (int i = a.Length - 1; i >= 0; i--)
            {
                x = int.Parse(a[i] + "");
                y = int.Parse(b[i] + "");
                int tong = x + y + nho;
                if (tong >= 10)
                {
                    kq = (tong % 10) + kq;
                    nho = tong / 10;
                }
                else
                {
                    kq = tong + kq;
                    nho = 0;
                }
            }
            if (nho == 1)
                kq = nho + kq;
            SoNguyenLon dapAn = new SoNguyenLon(kq, 1);

            return dapAn;
        }


        public static SoNguyenLon nhanSoNguyenLon(SoNguyenLon a, SoNguyenLon b)
        {
            string soNguyenA = a.soNguyen;
            string soNguyenB = b.soNguyen;

            // Thêm các chữ số '0' để đảm bảo hai số có độ dài bằng nhau
            while (soNguyenA.Length < soNguyenB.Length)
            {
                soNguyenA = "0" + soNguyenA;
            }

            while (soNguyenB.Length < soNguyenA.Length)
            {
                soNguyenB = "0" + soNguyenB;
            }

            int n = soNguyenA.Length;

            // Trường hợp cơ sở khi độ dài là 1
            if (n == 1)
            {
                int x = int.Parse(soNguyenA);
                int y = int.Parse(soNguyenB);
                int tich = x * y;
                int dau = (a.dau == b.dau) ? 1 : -1;
                return new SoNguyenLon(Math.Abs(tich).ToString(), dau);
            }

            // Chia số thành hai phần
            int m = n / 2;
            SoNguyenLon A = new SoNguyenLon(soNguyenA.Substring(0, n - m), 1);
            SoNguyenLon B = new SoNguyenLon(soNguyenA.Substring(n - m), 1);
            SoNguyenLon C = new SoNguyenLon(soNguyenB.Substring(0, n - m), 1);
            SoNguyenLon D = new SoNguyenLon(soNguyenB.Substring(n - m), 1);

            // Tính các giá trị trung gian
            SoNguyenLon AC = nhanSoNguyenLon(A, C);
            SoNguyenLon AD = nhanSoNguyenLon(A, D);
            SoNguyenLon BC = nhanSoNguyenLon(B, C);
            SoNguyenLon BD = nhanSoNguyenLon(B, D);

            // Kết hợp các kết quả với vị trí chữ số đúng
            for (int i = 0; i < 2 * m; i++) AC.soNguyen += "0"; // AC * 10^(2*m)
            for (int i = 0; i < m; i++) AD.soNguyen += "0";     // AD * 10^m
            for (int i = 0; i < m; i++) BC.soNguyen += "0";     // BC * 10^m

            // Cộng các kết quả lại
            SoNguyenLon sum1 = congHaiSoNguyenLon(AC, AD);
            SoNguyenLon sum2 = congHaiSoNguyenLon(sum1, BC);
            SoNguyenLon result = congHaiSoNguyenLon(sum2, BD);

            // Loại bỏ các số 0 đứng đầu và trả về kết quả
            result.soNguyen = result.soNguyen.TrimStart('0');
            result.dau = (a.dau == b.dau) ? 1 : -1; // Đặt dấu cho kết quả
            return result;
        }

        public static string TongHopKQ(SoNguyenLon m1, SoNguyenLon m2, SoNguyenLon m3, int n)
        {
            // Thêm "0" vào m1 để tương ứng với phép nhân 10^(2*m)
            for (int i = 0; i < 2 * (n / 2); i++)
                m1.soNguyen += "0";

            // Thêm "0" vào m2 để tương ứng với phép nhân 10^(m)
            for (int i = 0; i < n / 2; i++)
                m2.soNguyen += "0";

            // Cộng các kết quả trung gian m1, m2, m3
            SoNguyenLon m1_m2 = congHaiSoNguyenLon(m1, m2);
            SoNguyenLon dapAn = congHaiSoNguyenLon(m1_m2, m3);

            // Loại bỏ số 0 đứng đầu và trả về chuỗi kết quả
            return dapAn.soNguyen.TrimStart('0');
        }

        public string xuat()
        {
                return (dau == -1 ? "-" : "") + this.soNguyen;
         }


        

        public static SoNguyenLon RandomSoNguyenLon(int soChuSo)
        {
            Random rand = new Random();
            StringBuilder soNguyen = new StringBuilder();

            soNguyen.Append(rand.Next(1, 10));

            for (int i = 1; i < soChuSo; i++)
            {
                soNguyen.Append(rand.Next(0, 10));
            }

            int dau = rand.Next(2) == 0 ? -1 : 1;

            return new SoNguyenLon(soNguyen.ToString(), dau);
        }

        public static (SoNguyenLon, SoNguyenLon) RandomHaiSoNguyenLonKhacNhau(int soChuSo)
        {
            SoNguyenLon soA = RandomSoNguyenLon(soChuSo);
            SoNguyenLon soB;

            do
            {
                soB = RandomSoNguyenLon(soChuSo);
            }
            while (soB.soNguyen == soA.soNguyen && soB.dau == soA.dau);

            return (soA, soB);
        }




        ///////////////////////////////////////////////////////////////////////


        public static SoNguyenLon nhanSoNguyenLonFFT(SoNguyenLon a, SoNguyenLon b)
        {
            int lenA = a.soNguyen.Length;
            int lenB = b.soNguyen.Length;
            int n = 1;
            while (n < lenA + lenB) n <<= 1; // Tìm lũy thừa tiếp theo của 2

            Complex[] A = new Complex[n];
            Complex[] B = new Complex[n];

            for (int i = 0; i < lenA; i++)
                A[i] = new Complex(a.soNguyen[lenA - 1 - i] - '0', 0); // Đảo ngược thứ tự cho FFT
            for (int i = 0; i < lenB; i++)
                B[i] = new Complex(b.soNguyen[lenB - 1 - i] - '0', 0);

            A = FFT(A);
            B = FFT(B);

            for (int i = 0; i < n; i++)
                A[i] *= B[i];

            A = IFFT(A);

            long carry = 0;
            string result = "";
            for (int i = 0; i < n; i++)
            {
                long value = (long)(A[i].Real + 0.5) + carry; // Làm tròn và chuyển sang long
                result += value % 10;
                carry = value / 10;
            }

            while (carry > 0)
            {
                result += carry % 10;
                carry /= 10;
            }

            char[] resultArr = result.ToCharArray();
            Array.Reverse(resultArr);
            string finalResult = new string(resultArr).TrimStart('0');

            int resultSign = (a.dau == b.dau) ? 1 : -1;
            return new SoNguyenLon(finalResult, resultSign);
        }

        private static Complex[] FFT(Complex[] x)
        {
            int N = x.Length;
            if (N <= 1) return x;

            Complex[] even = new Complex[N / 2];
            Complex[] odd = new Complex[N / 2];

            for (int k = 0; k < N / 2; k++)
            {
                even[k] = x[2 * k];
                odd[k] = x[2 * k + 1];
            }

            even = FFT(even);
            odd = FFT(odd);

            Complex[] combined = new Complex[N];
            for (int k = 0; k < N / 2; k++)
            {
                double t = -2 * Math.PI * k / N;
                Complex twiddle = new Complex(Math.Cos(t), Math.Sin(t));
                combined[k] = even[k] + twiddle * odd[k];
                combined[k + N / 2] = even[k] - twiddle * odd[k];
            }
            return combined;
        }

        private static Complex[] IFFT(Complex[] x)
        {
            int N = x.Length;
            for (int i = 0; i < N; i++)
                x[i] = Complex.Conjugate(x[i]);

            x = FFT(x);

            for (int i = 0; i < N; i++)
            {
                x[i] = Complex.Conjugate(x[i]);
                x[i] /= N;
            }
            return x;
        }



    }

}






    

