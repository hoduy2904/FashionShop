using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace FashionShop.Models
{
    public class ThuVien
    {
        public static string tinhPhanTram(double giatricu,double giatrimoi)
        {
            try
            {
                return (Math.Floor(((giatrimoi - giatricu) / giatricu) * 100)) + "%";
            }catch
            {
                return "0%";
            }
        }
        public static string guid()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            return GuidString;
        }
        public static string EncodeMD5(string pass)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(pass);
            bs = md5.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (byte b in bs)
            {
                s.Append(b.ToString("x1").ToLower());
            }
            pass = s.ToString();
            return pass;
        }
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            string khongdau = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            khongdau = Regex.Replace(khongdau, "[^a-zA-Z0-9 ]+", "");
            Regex trimmer = new Regex(@"\s\s+");
            khongdau = trimmer.Replace(khongdau, " ").Trim();
            return khongdau.Replace(" ", "-").ToLower();
        }
        public static string VietnamDong(double? giatien)
        {
            var format = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            return String.Format(format, "{0:c0}", giatien);
        }
    }
}