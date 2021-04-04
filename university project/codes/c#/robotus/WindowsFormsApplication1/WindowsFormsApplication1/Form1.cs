using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO.Ports;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Threading;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        DataTable oku = new DataTable();
        DataTable oku1 = new DataTable();
        SerialPort sp;
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        MySqlConnection baglanti;
        MySqlDataAdapter listele1, listele, listeleci, listeleci1;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // baglanti = new MySqlConnection("Server=192.168.1.22;Port=3306;Database=robot;Uid=root;Pwd=root");
            baglanti = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=robot;Uid=root;Pwd=root");
            if (baglanti.State != System.Data.ConnectionState.Open)
                try
                {

                   
                    baglanti.Open();
                    label1.Text = "bağlandı";
                    //deneme
                    /*   MySqlDataAdapter listele = new MySqlDataAdapter("select * from durumcu where id=1", baglanti);
                       DataTable oku = new DataTable();
                       listele.Fill(oku);
                       dataGridView1.DataSource = oku;*/
                    //MessageBox.Show(dataGridView1.Rows[0].Cells[0].Value.ToString()); 

                   sp = new SerialPort("/dev/ttyACM0");
                   // sp = new SerialPort("COM5");
                    sp.BaudRate = 9600;
                   sp.Open();

                    listele = new MySqlDataAdapter("select * from durumcu where id=1", baglanti);
                    listele1 = new MySqlDataAdapter("select * from dolaporanlari where id=1", baglanti);
                    listele1.Fill(oku1);
                    listele.Fill(oku);
                    dataGridView1.DataSource = oku;
                    dataGridView2.DataSource = oku1;
                    /*
                                         MySqlCommand cmd1 = new MySqlCommand("select sutun from dolaporanlari", baglanti);
                                         var tokens1 = ((Int32)cmd1.ExecuteScalar());
                                         var firstName1 = tokens1;
                                         MessageBox.Show(firstName1.ToString());
 
                                         string Query = "update durumcu set Durum='durdu' where id=1";
                                         //This is  MySqlConnection here i have created the object and pass my connection string.  
                    
                                         MySqlCommand MyCommand2 = new MySqlCommand(Query, baglanti);
                                         MySqlDataReader MyReader2;
                                         MyReader2 = MyCommand2.ExecuteReader();   */
                    t.Interval = 1000;
                    t.Start();


                }
                catch (MySqlException ex)
                {
                    label1.Text = "hata" + ex;
                }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           try{
               /* int index = dataGridView1.Rows[0].Index;
                dataGridView1.Rows.RemoveAt(index);
                int index1 = dataGridView2.Rows[0].Index;
                dataGridView2.Rows.RemoveAt(index1);*/
               oku = new DataTable();
               dataGridView1.Rows.Clear();
               dataGridView1.Refresh();
               dataGridView2.Rows.Clear();
               dataGridView2.Refresh();
               listele1.Fill(oku1);
                listele.Fill(oku);
                dataGridView1.DataSource = oku;
                dataGridView2.DataSource = oku1;
                label2.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                if (label2.Text == "Okunuyor")
                {
                     okumert();
                }
        }
            catch (Exception)
            {
                oku = new DataTable();
                baglanti = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=robot;Uid=root;Pwd=root");
                baglanti.Open();
                listeleci = new MySqlDataAdapter("select * from durumcu where id=1", baglanti);
                listeleci1 = new MySqlDataAdapter("select * from dolaporanlari where id=1", baglanti);
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                dataGridView2.Rows.Clear();
                dataGridView2.Refresh();
                
                listeleci1.Fill(oku1);
                listeleci.Fill(oku);
                dataGridView1.DataSource = oku;
                dataGridView2.DataSource = oku1;
                label2.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                if (label2.Text == "Okunuyor")
                {

                   
                    okumert();
                }
            }
            
        }

        public void okumert()
        {
            //okuma
            string a1 = dataGridView2.Rows[0].Cells[2].Value.ToString();
            int tablosayisi = int.Parse(a1);
            //sensor okuma

            int i = 1, t = 0;
            while (true)
            {
                while (i % 2 == 1)
                {
                    t = t + 3 - (int.Parse(sp.ReadLine()) / 20);
                    sp.WriteLine("4000");
                    Thread.Sleep(4100);
                    t = t + 3 - (int.Parse(sp.ReadLine()) / 20);
                    i++;
                }
                if (i == tablosayisi + 1)
                {
                    break;
                }
                while (i % 2 == 0)
                {
                     sp.WriteLine("-1100");
                    Thread.Sleep(1200);
                    t = t + 3 - (int.Parse(sp.ReadLine()) / 20);
                    sp.WriteLine("1");
                    sp.WriteLine("4000");
                    Thread.Sleep(4100);
                    t = t + 3 - (int.Parse(sp.ReadLine()) / 20);
                    sp.WriteLine("1");
                    sp.WriteLine("-1100");
                    Thread.Sleep(1200);
                    i++;
                }
                if (i == tablosayisi + 1)
                {
                    break;
                }
            }
            if (i % 2 == 1)
            {
                sp.WriteLine("1");
                sp.WriteLine((-1 * 1000 * (i - 1)).ToString());
                Thread.Sleep(1100 * (i - 1));
            }
            else
            {
                sp.WriteLine("1");
                sp.WriteLine("4000");
                Thread.Sleep(4100);
                sp.WriteLine((-1*1000 * (i - 2)).ToString());
                Thread.Sleep(1100 * (i - 2));
            }
            
            label3.Text = t.ToString();
            string Query = "update durumcu set Durum='durdu',adet='" + t.ToString()+"' where id=1";
            MySqlCommand MyCommand2 = new MySqlCommand(Query, baglanti); 
            MySqlDataReader MyReader2;
            MyReader2 = MyCommand2.ExecuteReader();
            

        }
    }


}
