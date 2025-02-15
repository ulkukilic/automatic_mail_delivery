using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace automatic_mail_delivery
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Bağlantı dizesi, SQL Server'a bağlanmak için gerekli bilgileri içerir.
            String cs = "Data Source=elıf;Initial Catalog=SQLFULL; User ID =udemy; Password = 1";

            // SQL sorgusu, Orders tablosundaki 1997 ve 1998 yıllarına ait siparişleri çekiyor.
            String sql = "SELECT * \r\nFROM Orders\r\nWHERE OrderDate >= '1997-01-01' AND OrderDate <= '1998-12-31';\r\n";

            // SQL sorgusunu çalıştırmak ve sonuçları DataTable'a doldurmak için SqlDataAdapter kullanıyoruz.
            SqlDataAdapter sda = new SqlDataAdapter(sql, cs);
            DataTable dt = new DataTable();
            sda.Fill(dt);  // Sorgu sonucunu dt DataTable'ına yükler.

            string mailBody = "";
            // DataTable'dan her bir satırı döngü ile alır ve e-posta içeriğini oluşturur.
            foreach (DataRow item in dt.Rows)
            {
                // OrderDate ve CustomerID değerlerini e-posta içeriğine ekliyoruz.
                mailBody += item["OrderDate"] + " " + item["CustomerID"];
            }

            // E-posta gönderme fonksiyonunu çağırıyoruz.
            Sendmail(mailBody);
        }

        // E-posta gönderme fonksiyonu
        private static void Sendmail(string mailBody)
        {
            // MailMessage nesnesi, e-posta gönderimini sağlamak için kullanılır.
            MailMessage eposta = new MailMessage();

            // Gönderen e-posta adresini belirtiyoruz.
            eposta.From = new MailAddress("ulkuklc@gmail.com");

            // Alıcı e-posta adresini ekliyoruz.
            eposta.To.Add("ulkuklccc@gmail.com");

            // E-posta başlığını belirtiyoruz.
            eposta.Subject = " son siparis";

            // E-posta içeriğini belirtiyoruz.
            eposta.Body = mailBody;

            // SmtpClient nesnesi, e-posta göndermek için kullanılan SMTP sunucu ayarlarını yapar.
            SmtpClient smtp = new SmtpClient();

            // SMTP sunucusuna bağlanmak için kimlik bilgilerini sağlıyoruz (e-posta adresi ve şifre).
            smtp.Credentials = new System.Net.NetworkCredential("ulkukl@gmail.com", "password");

            // 587 portu, SSL ile güvenli e-posta gönderimi için yaygın olarak kullanılır.
            smtp.Port = 587;  // SMTP sunucusunun kullanacağı port, 587 genellikle güvenli bağlantılar için kullanılır.

            // SMTP sunucusunun adresini belirliyoruz (Gmail için).
            smtp.Host = "smtp.gmail.com";  // SMTP sunucu adresi Gmail için 'smtp.gmail.com' olmalı.

            // SSL (Secure Sockets Layer) kullanarak güvenli bir bağlantı sağlıyoruz.
            smtp.EnableSsl = true;  // E-posta güvenliği için SSL şifrelemesi etkinleştirilmiştir.

            // E-postayı gönderiyoruz.
            smtp.Send(eposta);
        }
    }
}
