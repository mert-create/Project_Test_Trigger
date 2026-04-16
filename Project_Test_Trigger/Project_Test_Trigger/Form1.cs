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

namespace Project_Test_Trigger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection bgl = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=DbTest;Integrated Security=True;TrustServerCertificate=True");
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLKITAPLAR",bgl);
            DataTable dt=new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void sayac()
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("SELECT*FROM TBLSAYAC", bgl);
            SqlDataReader dr = komut.ExecuteReader();   
            while (dr.Read())
            {
                lblkitap.Text = dr[0].ToString();
            }
            bgl.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            sayac();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLKITAPLAR (AD,YAZAR,SAYFA,YAYINEVI,TÜR) VALUES (@p1,@p2,@p3,@p4,@p5)", bgl);
            komut.Parameters.AddWithValue("@p1",txtad.Text);
            komut.Parameters.AddWithValue("@p2",txtyazar.Text);
            komut.Parameters.AddWithValue("@p3",txtsayfa.Text);
            komut.Parameters.AddWithValue("@p4",txtyayınevı.Text);
            komut.Parameters.AddWithValue("@p5",txttur.Text);
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Kitap sisteme eklendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
            sayac();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secılen = dataGridView1.SelectedCells[0].RowIndex;

            txtıd.Text = dataGridView1.Rows[secılen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secılen].Cells[1].Value.ToString();
            txtyazar.Text = dataGridView1.Rows[secılen].Cells[2].Value.ToString();
            txtsayfa.Text = dataGridView1.Rows[secılen].Cells[3].Value.ToString();
            txtyayınevı.Text = dataGridView1.Rows[secılen].Cells[4].Value.ToString();
            txttur.Text = dataGridView1.Rows[secılen].Cells[5].Value.ToString();

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("Delete From TBLKITAPLAR Where ID=@p1",bgl);
            komut.Parameters.AddWithValue("@p1",txtıd.Text);
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Kitap sistemden silindi");
            listele();
            sayac();
        }
    }
}
