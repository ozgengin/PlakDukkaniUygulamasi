using PlakciDukkani.Classes;
using PlakciDukkani.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlakciDukkani
{
    public partial class YeniKullaniciEkle : Form
    {
        UygulamaDbContext db = new UygulamaDbContext();
        public YeniKullaniciEkle()
        {
            InitializeComponent();
        }

        public bool KullaniciAdiKayitliMi(string kullaniciAdi)
        {
            var kullaniciAdiKontrol = db.Yoneticiler.Where(x => x.KullaniciAd == kullaniciAdi).FirstOrDefault();
            if (kullaniciAdiKontrol != null)
            {
                return true;
            }
            else return false;
        }
        private string sha256_hash(string sifre)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(sifre)).Select(l => l.ToString("X2")));
            }
        }

        private void btnKayit_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtKullaniciAd.Text == "" || txtSifre.Text == "" || txtTekrar.Text == "")
                {
                    MessageBox.Show("Boşlukları doldurunuz.");
                    return;
                }
                if (KullaniciAdiKayitliMi(txtKullaniciAd.Text) == false && txtSifre.Text == txtTekrar.Text)
                {
                    Yonetici yonetici = new Yonetici();
                    yonetici.KullaniciAd = txtKullaniciAd.Text;
                    yonetici.Sifre = sha256_hash(txtSifre.Text);
                    db.Yoneticiler.Add(yonetici);
                    db.SaveChanges();
                    MessageBox.Show("Yönetici eklenmiştir.");
                    Form1 form1 = new Form1();
                    form1.Show();
                    this.Close();
                }
                else if (KullaniciAdiKayitliMi(txtKullaniciAd.Text) == true && txtSifre.Text == txtTekrar.Text)
                {
                    MessageBox.Show("Bu kullanıcı adı bulunmaktadır.");
                    return;
                }
                else if (KullaniciAdiKayitliMi(txtKullaniciAd.Text) == false && txtSifre.Text != txtTekrar.Text)
                {
                    MessageBox.Show("Şifreler birbiriyle uyumsuzdur.");
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata" + ex.Message);
            }
        }

        private void txtSifre_TextChanged_1(object sender, EventArgs e)
        {
            string sifre;
            string karakter = "!:*+";
            string kucukHarf = "abcçdefghıijklmnoöpresştuüvyzwxq";
            string buyukHarf = "ABCÇDEFGHIİJKLMNOÖPRSŞTUÜVYZWXQ";
            sifre = txtSifre.Text;
            sha256_hash(sifre);
            if (sifre.Length >= 8 && sifre.Any(s => karakter.Contains(s)) && sifre.Any(s => kucukHarf.Contains(s)) && sifre.Any(s => buyukHarf.Contains(s)))
            {
                lblDerece.Text = "Uygun Kriter";
                lblDerece.ForeColor = Color.Green;
            }
        }
    }
}
