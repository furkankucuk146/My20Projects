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
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection = new SqlConnection("Server=LAPTOP-B75NTCVD;initial catalog=DbCustomer;integrated security=true");
        private void btnList_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("select CustomerId,CustomerName,CustomerSurname,CustomerBalance,CustomerStatus,CityName\r\nfrom TblCustomer inner join TblCity on TblCity.CityId=TblCustomer.CustomerCity", sqlConnection);
            //hafızada tutulan verileri ekrana yansıtmak için köprü görevi görecek adapter
            SqlDataAdapter adapter = new SqlDataAdapter(command); //adapter ın içinde sorgudan gelen değer var select'li
            DataTable dataTable = new DataTable(); //hafızaya almak için dataTable isminde sanal bir tablo olusturdum
            adapter.Fill(dataTable);//bu dataTable ın içine adapter dan gelen veriyi doldurucam
            dataGridView1.DataSource = dataTable; //datagridview aracında göstermeye calısıyoruz.
            sqlConnection.Close(); //bağlantıyı aç kapa yapıyoruz   execute CustomerListWithCity
        }

        private void btnProcedure_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("execute CustomerListWithCity", sqlConnection);
            //hafızada tutulan verileri ekrana yansıtmak için köprü görevi görecek adapter
            SqlDataAdapter adapter = new SqlDataAdapter(command); //adapter ın içinde sorgudan gelen değer var select'li
            DataTable dataTable = new DataTable(); //hafızaya almak için dataTable isminde sanal bir tablo olusturdum
            adapter.Fill(dataTable);//bu dataTable ın içine adapter dan gelen veriyi doldurucam
            dataGridView1.DataSource = dataTable; //datagridview aracında göstermeye calısıyoruz.
            sqlConnection.Close(); //bağlantıyı aç kapa yapıyoruz. kullanmasak da olur. sqldatadapter bunu zaten yapıyor aslında
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * from TblCity", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cmbCity.ValueMember = "CityId"; //City tablosunda combobox a gelen şehir bilgilerinin arka tarafta bir id değeri olacak.
            //o id değeri valuemember a yazılıyor.
            cmbCity.DisplayMember = "CityName"; //displaymember da kullanıcıya gözükecek olan kısım
            cmbCity.DataSource = dataTable; //dataTable veri kaynağı olarak gönderilecek
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("insert into TblCustomer (CustomerName,CustomerSurname,CustomerCity,CustomerBalance,CustomerStatus) values (@customerName,@customerSurname,@customerCity,@customerBalance,@customerStatus)", sqlConnection);
            command.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            command.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            command.Parameters.AddWithValue("@customerCity", cmbCity.SelectedValue);
            command.Parameters.AddWithValue("@customerBalance", txtBalance.Text);
            if (rdbActive.Checked)
            {
                command.Parameters.AddWithValue("@customerStatus", true);
            }
            if (rdbPassive.Checked)
            {
                command.Parameters.AddWithValue("@customerStatus", false);
            }
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Başarıyla Eklendi");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("delete from TblCustomer where CustomerId=@customerId", sqlConnection);
            command.Parameters.AddWithValue("@customerId", txtCustomerId.Text);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri başarılı bir şekilde silindi.", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("update TblCustomer set  CustomerName=@customerName,CustomerSurname=@customerSurname,CustomerCity=@customerCity,CustomerBalance=@customerBalance,CustomerStatus=@customerStatus where CustomerId=@customerId", sqlConnection);
            command.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            command.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            command.Parameters.AddWithValue("@customerCity", cmbCity.SelectedValue);
            command.Parameters.AddWithValue("@customerBalance", txtBalance.Text);
            command.Parameters.AddWithValue("@customerId", txtCustomerId.Text);
            if (rdbActive.Checked)
            {
                command.Parameters.AddWithValue("@customerStatus", true);
            }
            if (rdbPassive.Checked)
            {
                command.Parameters.AddWithValue("@customerStatus", false);
            }
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Başarıyla Güncellendi");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("select CustomerId,CustomerName,CustomerSurname,CustomerBalance,CustomerStatus,CityName\r\nfrom TblCustomer inner join TblCity on TblCity.CityId=TblCustomer.CustomerCity where CustomerName=@customerName", sqlConnection);
            command.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            //hafızada tutulan verileri ekrana yansıtmak için köprü görevi görecek adapter
            SqlDataAdapter adapter = new SqlDataAdapter(command); //adapter ın içinde sorgudan gelen değer var select'li
            DataTable dataTable = new DataTable(); //hafızaya almak için dataTable isminde sanal bir tablo olusturdum
            adapter.Fill(dataTable);//bu dataTable ın içine adapter dan gelen veriyi doldurucam
            dataGridView1.DataSource = dataTable; //datagridview aracında göstermeye calısıyoruz.
            sqlConnection.Close(); //bağlantıyı aç kapa yapıyoruz   execute CustomerListWithCity
        }
    }
}
