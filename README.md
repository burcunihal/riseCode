# riseCode



kafkayı ve postgreyi çalıştırmak için kullanmanız gereken docker komutları : 

docker-compose up -d 

docker run --name some-postgres -e POSTGRES_PASSWORD=mysecretpassword -d -p 5432:5432 postgres


sonrasında projerlerin içerisinde 

dotnet restore

dotnet ef database update

dotnet run 


çalıştırarak veritabanını hazırlayınız 


ardından request atmaya başlayabilirsiniz. 




NOT : kafkada karşılaştığım bir hata nedeniyle background workerda bazen message consume edemiyorum ancak süre bitmeden hatanın nedenini bulamadım o nedenle bu halde gönderiyorum. 
