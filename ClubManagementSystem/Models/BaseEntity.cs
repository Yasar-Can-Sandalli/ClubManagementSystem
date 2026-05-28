using System;

namespace ClubManagementSystem.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}



/* TODO 
    Murat Hocam merhabalar Ben Yaşar Can Sandallı
    Nesne Tabanlı Programlama Dersi Final Ödevimi Tanıtmak İçin Bu kısa videoyu çekiyorum (mikrofonum yok malesef , sesli bir şekilde tanıtmayı çok isterdim !)
    Projemi Windowsun Desktop Uygulamarından Biri Olan WPF APP ile yaptım, Kendi Kulübüm Olan RotomDX için geliştirdiğim bir Kulüp Yönetim projesidir
    Uygulamayı Aynı zamanda SQL Server Veri Tabanına Bağladım (Üyeler , Kulüp Etlimlikleri vs. bilgilerini tutmak için)
    Kodların açıklanması için ayrıyeten bir rapor daha hazırlayıp DBS ye yükleyeceğim
    Birde Bu video yu bayrama 4 gün kala çekiyorum ,Kurban Bayramınızı da en içten dileklerimle kutlarım :)
    Sevgiler....
    
*/