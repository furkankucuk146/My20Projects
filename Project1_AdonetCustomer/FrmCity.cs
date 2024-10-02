using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1_AdonetCustomer
{
    public partial class FrmCity : Form
    {
        public FrmCity()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection = new SqlConnection("Server=LAPTOP-B75NTCVD;initial catalog=DbCustomer;integrated security=true");
        //integrated security ne işe yarar? true ya da false olması ne işe yarar ?
        private void btnList_Click(object sender, EventArgs e)
        {
            
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * From TblCity", sqlConnection);
            //hafızada tutulan verileri ekrana yansıtmak için köprü görevi görecek adapter
            SqlDataAdapter adapter = new SqlDataAdapter(command); //adapter ın içinde sorgudan gelen değer var select'li
            DataTable dataTable = new DataTable(); //hafızaya almak için dataTable isminde sanal bir tablo olusturdum
            adapter.Fill(dataTable);//bu dataTable ın içine adapter dan gelen veriyi doldurucam
            dataGridView1.DataSource = dataTable; //datagridview aracında göstermeye calısıyoruz.
            sqlConnection.Close(); //bağlantıyı aç kapa yapıyoruz
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("insert into TblCity (CityName,CityCountry) values (@cityName,@cityCountry)", sqlConnection);
            command.Parameters.AddWithValue("@cityName",txtCityName.Text);
            command.Parameters.AddWithValue("@cityCountry",txtCityCountry.Text); //parametreyi değeri ile beraber ekledik
            command.ExecuteNonQuery(); //değişiklikleri kaydediyor ve veritabanına yansıtıyor ve sayısını döner. 1 tane kayıt ekliyoruz, 1 dönecek
            sqlConnection.Close();
            MessageBox.Show("Şehir başarılı bir şekilde eklendi");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Aynı isimde şehir vs olabileceği için silme işlemini id ye göre yapacagız
            //Müşteri silerkende mesela aynı isimde müşteri olabilir, o yüzden id ye göre işlem yapmamız gerekiyor.
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("delete from TblCity where CityId=@cityId", sqlConnection);
            command.Parameters.AddWithValue("@cityId",txtCityId.Text);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Şehir başarılı bir şekilde silindi.","Uyarı!",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("update TblCity set CityName=@cityName,CityCountry=@cityCountry where CityId=@cityId", sqlConnection);
            command.Parameters.AddWithValue("@cityName", txtCityName.Text);
            command.Parameters.AddWithValue("@cityCountry", txtCityCountry.Text);
            command.Parameters.AddWithValue("@cityId", txtCityId.Text);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Şehir başarılı bir şekilde güncellendi.", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * from TblCity where CityName=@cityName", sqlConnection);
            command.Parameters.AddWithValue("@cityName", txtCityName.Text);
            //hafızada tutulan verileri ekrana yansıtmak için köprü görevi görecek adapter
            SqlDataAdapter adapter = new SqlDataAdapter(command); //adapter ın içinde sorgudan gelen değer var select'li
            DataTable dataTable = new DataTable(); //hafızaya almak için dataTable isminde sanal bir tablo olusturdum
            adapter.Fill(dataTable);//bu dataTable ın içine adapter dan gelen veriyi doldurucam
            dataGridView1.DataSource = dataTable; //datagridview aracında göstermeye calısıyoruz.
            sqlConnection.Close(); //bağlantıyı aç kapa yapıyoruz
        }
    }
}
