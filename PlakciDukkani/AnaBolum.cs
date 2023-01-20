using PlakciDukkani.Classes;
using PlakciDukkani.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlakciDukkani
{
    public partial class AnaBolum : Form
    {
        UygulamaDbContext db = new UygulamaDbContext();
        Album seciliAlbum;
        public AnaBolum()
        {
            InitializeComponent();
            OrnekVerileriYukle();
            dgvBilgiler.DataSource = db.Albumler.ToList();
            
        }

        private void OrnekVerileriYukle()
        {
            if (db.Albumler.Any()) return;
            var a1 = new Album() { Ad ="Karma", Sanatci="Tarkan", CikisYili=2001, DevamMi=true, Fiyat=60m, İndirimOrani=0 };
            var a2 = new Album() { Ad = "Yerli Plaka", Sanatci = "Ceza", CikisYili = 2006, DevamMi = false, Fiyat = 50m, İndirimOrani = 2 }; 
            var a3 = new Album() { Ad = "Mançoloji", Sanatci = "Barış Manço", CikisYili = 1999, DevamMi = false, Fiyat = 70m, İndirimOrani = 3 };
            var a4 = new Album() { Ad = "Galiba", Sanatci = "Sagopa", CikisYili = 2007, DevamMi = true, Fiyat = 80m, İndirimOrani = 4 };
            var a5 = new Album() { Ad = "Parmak İzi", Sanatci = "Şebnem Ferah", CikisYili = 2018, DevamMi = true, Fiyat = 34m, İndirimOrani = 5 };
            db.Albumler.AddRange(a1, a2, a3, a4, a5);
            db.SaveChanges();
        }

        

        private void btnEkle_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtAlbumAd.Text=="" || txtFiyat.Text=="" || txtSanatci.Text=="" || (rdoDevam.Checked==false && rdoDurdu.Checked==false)|| txtIndirim.Text=="" ||txtYil.Text=="")
                {
                    MessageBox.Show("Boşlukları doldurunuz.");
                    return;
                }
                
                Album yeniAlbum = new Album();
                yeniAlbum.Ad = txtAlbumAd.Text;
                yeniAlbum.Sanatci = txtSanatci.Text;
                yeniAlbum.CikisYili = Convert.ToInt32(txtYil.Text);
                yeniAlbum.İndirimOrani = Convert.ToDouble(txtIndirim.Text);
                yeniAlbum.Fiyat = Convert.ToDecimal(txtFiyat.Text);
                if (rdoDevam.Checked==true)
                {
                    yeniAlbum.DevamMi = true;
                }
                else if(rdoDurdu.Checked==true)
                {
                    yeniAlbum.DevamMi=false;
                }
                db.Albumler.Add(yeniAlbum);
                db.SaveChanges();
                MessageBox.Show("Albüm eklenmiştir.");
                Temizle();
                dgvBilgiler.DataSource = db.Albumler.ToList();
                

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata"+ex.Message);
            }
        }

        private void Temizle()
        {
            txtAlbumAd.Text ="";
            txtSanatci.Text ="";
            txtYil.Text ="";
            txtIndirim.Text ="";
            txtFiyat.Text ="";
            
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (seciliAlbum==null)
                {
                    MessageBox.Show("Güncellenecek albümü seçmediniz.");
                    return;
                }
                var guncellenecekalbum = db.Albumler.Find(seciliAlbum.Id);
                guncellenecekalbum.Ad = txtAlbumAd.Text;
                guncellenecekalbum.Sanatci = txtSanatci.Text;
                guncellenecekalbum.CikisYili = Convert.ToInt32(txtYil.Text);
                guncellenecekalbum.İndirimOrani = Convert.ToDouble(txtIndirim.Text);
                guncellenecekalbum.Fiyat = Convert.ToDecimal(txtFiyat.Text);
                if (rdoDevam.Checked == true)
                {
                    guncellenecekalbum.DevamMi = true;
                }
                else if (rdoDurdu.Checked == true)
                {
                    guncellenecekalbum.DevamMi = false;
                }
                db.SaveChanges();
                MessageBox.Show("Albüm güncellenmiştir.");
                Temizle();
                dgvBilgiler.DataSource = db.Albumler.ToList();
                

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata" + ex.Message);
            }
        }

        private void dgvBilgiler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            seciliAlbum = db.Albumler.Find(Convert.ToInt32(dgvBilgiler.Rows[e.RowIndex].Cells[0].Value));
            Getir();
        }

        private void Getir()
        {
            txtAlbumAd.Text = seciliAlbum.Ad;
            txtSanatci.Text = seciliAlbum.Sanatci;
            txtYil.Text = seciliAlbum.CikisYili.ToString();
            txtIndirim.Text = seciliAlbum.İndirimOrani.ToString();
            txtFiyat.Text = seciliAlbum.Fiyat.ToString();
            if (seciliAlbum.DevamMi == true)
            {
                rdoDevam.Checked = true;

            }
            else if (seciliAlbum.DevamMi == false)
            {
                rdoDurdu.Checked = true;

            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (seciliAlbum == null)
                {
                    MessageBox.Show("Silinecek albümü seçmediniz.");
                    return;
                }
                
                db.Albumler.Remove(seciliAlbum);
                db.SaveChanges();
                MessageBox.Show("Albüm silinmiştir.");
                Temizle();
                dgvBilgiler.DataSource = db.Albumler.ToList();
               

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata" + ex.Message);
            }
        }

        private void btnList2_Click(object sender, EventArgs e)
        {
            dgvBilgiler.DataSource=db.Albumler.Where(x=>x.DevamMi==false).Select(x=>new { AlbumAdi=x.Ad, Sanatçı=x.Sanatci }).ToList();
            dgvBilgiler.Enabled=false;
        }

        private void btnList3_Click(object sender, EventArgs e)
        {
            dgvBilgiler.DataSource = db.Albumler.Where(x => x.DevamMi == true).Select(x => new { AlbumAdi = x.Ad, Sanatçı = x.Sanatci }).ToList();
            dgvBilgiler.Enabled = false;

        }

        private void btnList4_Click(object sender, EventArgs e)
        {
            dgvBilgiler.DataSource = db.Albumler.OrderByDescending(x => x.Id).Take(10).Select(x => new { AlbumAdi = x.Ad, Sanatçı = x.Sanatci }).ToList();
            dgvBilgiler.Enabled = false;

        }

        private void btnList5_Click(object sender, EventArgs e)
        {
            dgvBilgiler.DataSource = db.Albumler.Where(x => x.İndirimOrani != 0).OrderByDescending(x => x.İndirimOrani).Select(x => new { AlbumAdi = x.Ad, Sanatçı = x.Sanatci }).ToList();
            dgvBilgiler.Enabled = false;

        }

        private void btnList1_Click(object sender, EventArgs e)
        {
            dgvBilgiler.DataSource = db.Albumler.ToList();
            dgvBilgiler.Enabled = true;

        }

        private void AnaBolum_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
