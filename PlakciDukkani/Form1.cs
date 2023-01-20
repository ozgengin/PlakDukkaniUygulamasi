using PlakciDukkani.Classes;
using PlakciDukkani.Context;
using System.Security.Cryptography;
using System.Text;

namespace PlakciDukkani
{
    public partial class Form1 : Form
    {
        UygulamaDbContext db = new UygulamaDbContext();
        public static int id;
        public static string kullaniciAdi;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            YeniKullaniciEkle yeniKullaniciEkle = new YeniKullaniciEkle();
            yeniKullaniciEkle.Show();
            this.Hide();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                var sorgulama = db.Yoneticiler.Where(x => x.KullaniciAd == txtKullaniciAd.Text).FirstOrDefault();
                kullaniciAdi = sorgulama.KullaniciAd;
                id = sorgulama.Id;
                GirisYapKontrol();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Bu kullanýcý Kayýtlý deðildir. Lütfen yeni hesap oluþturunuz.");
                txtKullaniciAd.Text = "";
                txtSifre.Text = "";
            }
        }
        private void GirisYapKontrol()
        {
            var kullaniciAdiKontrol = db.Yoneticiler.Where(x => x.KullaniciAd == txtKullaniciAd.Text).FirstOrDefault();
            var sifreKontrol = db.Yoneticiler.Where(x => x.Sifre == sha256_hash(txtSifre.Text)).FirstOrDefault();
            if (kullaniciAdiKontrol != null)
            {
                if (kullaniciAdiKontrol.Sifre == sha256_hash(txtSifre.Text))
                {
                    MessageBox.Show("Giriþ Baþarýlý");
                    AnaBolum anaBolum = new AnaBolum();
                    anaBolum.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Þifre ya da kullanýcý adý hatalý!\n"
                        + "Lütfen tekrar deneyiniz.");
                }
            }
            else
                MessageBox.Show("Kayýtlý kullanýcý bulunamadý");
        }
        private string sha256_hash(string sifre)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(sifre)).Select(l => l.ToString("X2")));
            }
        }
    }
}