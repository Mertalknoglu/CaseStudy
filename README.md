Backend Developer Case Study 
Bu depo, .NET, CQRS, Onion Mimari, JWT tabanlı Kimlik Doğrulama ve diğer modern tasarım prensiplerini kullanarak geliştirilen bir backend case study uygulamasını içermektedir. Uygulama, ölçeklenebilirliği artıran mikroservisler (kimlik doğrulama, ürün yönetimi, log toplama) içermektedir.

Kullanılan Teknolojiler:
.NET 8.0
C#
RabbitMQ (olay tabanlı mimari için)
Redis (cache yönetimi)
Docker (containerization)(eksik)
JWT (kimlik doğrulama)
MassTransit (mesajlaşma broker entegrasyonu)

Proje Genel Bakış
Bu proje, üç ana mikroservis içerir:

Auth Mikroservisi - JWT token üretimi ve doğrulaması sağlar.
Product Mikroservisi - Ürün yönetimi işlemleri CQRS deseni ve olay tabanlı mimari ile yapılır.
Log Mikroservisi - Diğer servislerden gelen logları toplar ve yapılandırılmış loglama kullanarak yönetir.
