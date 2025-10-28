# Speech to Text - C# Uygulaması

Bu proje, ücretsiz bir Speech to Text (Konuşmadan Metne) uygulamasıdır. NAudio kütüphanesi kullanılarak geliştirilmiştir.

## Özellikler

- ✅ Tamamen ücretsiz
- ✅ Mikrofon ile ses kaydı
- ✅ Kullanıcı dostu arayüz
- ✅ Özelleştirilebilir kayıt süresi
- ✅ WAV formatında kayıt

## Teknoloji Stack

- **.NET 9.0** - Framework
- **NAudio** - Ses kaydı için

## Kurulum

### Gereksinimler
- .NET 9.0 SDK veya üzeri
- Windows 10/11
- Mikrofon

### Adımlar

1. Projeyi indirin veya klonlayın

2. Paketleri yükleyin:
```bash
cd SpeechToTextApp
dotnet restore
```

3. Projeyi derleyin:
```bash
dotnet build
```

4. Uygulamayı çalıştırın:
```bash
dotnet run
```

## Kullanım

1. Uygulamayı çalıştırın
2. Kayıt süresini girin (varsayılan: 5 saniye)
3. Herhangi bir tuşa basın ve konuşun
4. Ses dosyası `ses_kaydi.wav` olarak kaydedilir

## Transkripsiyon (Metne Çevirme)

Uygulama ses kaydı yapar ancak otomatik transkripsiyon yapmaz. Transkripsiyon için aşağıdaki ücretsiz servisleri kullanabilirsiniz:

### 1. AssemblyAI (Önerilen)
- **Ücretsiz Limit**: Aylık 5 saat
- **Link**: https://www.assemblyai.com/
- En kolay API'lerden biri

### 2. Deepgram
- **Ücretsiz Limit**: Aylık 12,000 dakika
- **Link**: https://deepgram.com/
- Hızlı ve güvenilir

### 3. Google Cloud Speech-to-Text
- **Ücretsiz Limit**: İlk 60 dakika/ay
- **Link**: https://cloud.google.com/speech-to-text
- Sonraki dakika için ücretlendirme var

### 4. Azure Speech Services
- **Ücretsiz Limit**: Aylık 5 saat
- **Link**: https://azure.microsoft.com/services/cognitive-services/speech-services/
- Microsoft'un servisi

## API Kullanımı (Gelecek Güncellemeler)

İsterseniz bu projeye API entegrasyonu ekleyebilirsiniz. Örnek kodlar için `API_ENTEGRASYON.md` dosyasına bakın (gelecek güncellemede eklenecek).

## Sorun Giderme

### "Mikrofon bulunamadı" hatası
- Windows ses ayarlarından mikrofon iznini kontrol edin
- Başka bir program mikrofonu kullanmıyor olmalı

### "Ses kaydedilmiyor" sorunu
- Mikrofonun ses ayarlarından açık olduğunu kontrol edin
- Test için Windows'un kendi ses kaydı uygulamasını kullanın

## Dosya Konumları

- Kaydedilen ses dosyası: Proje klasöründe `ses_kaydi.wav`
- Süre limiti: Maksimum 300 saniye (5 dakika)

## Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

## Kaynaklar

-

