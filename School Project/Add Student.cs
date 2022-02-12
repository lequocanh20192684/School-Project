﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace School_Project
{
    public partial class Add_Student : Form
    {
        #region khởi tạo tham số ban đầu cần thiết
        private SqlConnection cn;
        private SqlDataReader dr;
        private ClassDB db = new ClassDB();
        private Mngstudent f;
        private string _title = "Hệ thống quản lý";
        private string a;

        public Add_Student(Mngstudent f)
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.GetConnection();
            this.f = f;
        }
        #endregion

        //tự động tinh tuổi khi nhập ngày sinh (thông qua hàm years)
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            a = years(dateofbirthbox.Value.Date, DateTime.Now.Date).ToString();
        }

        private int years(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) + (((end.Month > start.Month) || (end.Month == start.Month) && (end.Day >= start.Day)) ? 1 : 0);
        }

        //lưu thông tin
        private void savebutton_Click(object sender, EventArgs e)
        {

            //tạo list các textbox để kiểm tra đã điền đủ hay chưa
            List<string> textbox = new List<string>();
            textbox.Add(namebox.Text); textbox.Add(genderbox.Text); textbox.Add(placeofbirthbox.Text);
            textbox.Add(ethnicbox.Text); textbox.Add(nationalitybox.Text); textbox.Add(hometownbox.Text); textbox.Add(addressbox.Text);
            textbox.Add(momnamebox.Text); textbox.Add(dadnamebox.Text); textbox.Add(heightbox.Text); textbox.Add(weightbox.Text);
            textbox.Add(phonenumbox.Text); textbox.Add(statusbox.Text);

            try
            {
                foreach (string s in textbox)
                {
                    if (string.IsNullOrEmpty(s))
                    {
                        MessageBox.Show("Thông tin điền thiếu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                }
                if (MessageBox.Show("Tất cả thông tin đã được nhập đúng?", _title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("INSERT INTO R2(tenHocSinh,Trangthai,Gioitinh,NgaySinh,Noisinh,Dantoc,Quoctich,Quequan,Diachi,Tenme,Tenbo,Sodienthoai,Chieucao,Cannang, Tuoi) VALUES(@tenHocSinh,@Trangthai,@Gioitinh,@NgaySinh,@Noisinh,@Dantoc,@Quoctich,@Quequan,@Diachi,@Tenme,@Tenbo,@Sodienthoai,@Chieucao,@Cannang, @Tuoi)", cn);
                    cm.Parameters.AddWithValue("@tenHocSinh", namebox.Text);
                    cm.Parameters.AddWithValue("@Trangthai", statusbox.Text);
                    cm.Parameters.AddWithValue("@Gioitinh", genderbox.SelectedItem.ToString());
                    cm.Parameters.AddWithValue("@NgaySinh", dateofbirthbox.Text);
                    cm.Parameters.AddWithValue("@NoiSinh", placeofbirthbox.Text);
                    cm.Parameters.AddWithValue("@Dantoc", ethnicbox.Text);
                    cm.Parameters.AddWithValue("@Quoctich", nationalitybox.Text);
                    cm.Parameters.AddWithValue("@Quequan", hometownbox.Text);
                    cm.Parameters.AddWithValue("@Diachi", addressbox.Text);
                    cm.Parameters.AddWithValue("@Tenme", momnamebox.Text);
                    cm.Parameters.AddWithValue("@Tenbo", dadnamebox.Text);
                    cm.Parameters.AddWithValue("@Sodienthoai", phonenumbox.Text);
                    cm.Parameters.AddWithValue("@Chieucao", heightbox.Text);
                    cm.Parameters.AddWithValue("@Cannang", weightbox.Text);
                    cm.Parameters.AddWithValue("@Tuoi", a);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Lưu thông tin thành công", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.LoadRecords();
                    Close();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //xóa thông tin vừa nhập
        private void clearbutton_Click(object sender, EventArgs e)
        {
            namebox.Clear();
            genderbox.SelectedIndex = -1;
            ethnicbox.Clear();
            nationalitybox.Clear();
            placeofbirthbox.Clear();
            hometownbox.Clear();
            addressbox.Clear();
            momnamebox.Clear(); dadnamebox.Clear();
            dateofbirthbox.Value = DateTime.Now; statusbox.SelectedIndex = -1;
            phonenumbox.Clear();
        }
    }
}