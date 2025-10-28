using NAudio.Wave;
using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║   Speech to Text - Otomatik         ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        
        // API Key kontrolü (daha hızlı)
        string apiKey = LoadOrGetApiKey();
        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine();
            Console.WriteLine("⚠️  API key gerekli! Deepgram.com adresinden ücretsiz alın.");
            Console.WriteLine("    (Aylık 12,000 dakika ücretsiz)");
            Console.Write("    API Key: ");
            apiKey = Console.ReadLine()?.Trim() ?? "";
            
            if (!string.IsNullOrEmpty(apiKey))
            {
                File.WriteAllText("api_key.txt", apiKey);
            }
        }
        
        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("❌ API key yok, program sonlandırılıyor.");
            Console.ReadKey();
            return;
        }
        
        Console.Write("\nKayıt süresi (saniye) [5]: ");
        int duration = int.TryParse(Console.ReadLine(), out int d) ? d : 5;
        
        Console.WriteLine("\n⏺️  Kayıt başlıyor... (tuşa bas)");
        Console.ReadKey();
        Console.WriteLine("\n🔴 KAYIT...");
        
        string audioFile = "ses.wav";
        RecordWithNAudio(audioFile, duration);
        
        Console.WriteLine("\n✅ Kayıt tamam!\n🔄 Dönüştürülüyor...");
        
        try
        {
            string text = await TranscribeWithDeepgram(audioFile, apiKey);
            
            Console.WriteLine("\n╔══════════════════════════════════╗");
            Console.WriteLine("║         SONUÇ                    ║");
            Console.WriteLine("╚══════════════════════════════════╝\n");
            Console.WriteLine(text);
            Console.WriteLine();
            
            File.WriteAllText("transkripsiyon.txt", text, Encoding.UTF8);
            Console.WriteLine("✅ transkripsiyon.txt kaydedildi.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Hata: {ex.Message}");
        }
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static string? LoadOrGetApiKey()
    {
        if (File.Exists("api_key.txt"))
        {
            return File.ReadAllText("api_key.txt").Trim();
        }
        return null;
    }

    static void RecordWithNAudio(string path, int seconds)
    {
        using var waveIn = new WaveInEvent();
        waveIn.WaveFormat = new WaveFormat(16000, 16, 1);
        
        using var writer = new WaveFileWriter(path, waveIn.WaveFormat);
        
        waveIn.DataAvailable += (s, e) => writer.Write(e.Buffer, 0, e.BytesRecorded);
        
        waveIn.StartRecording();
        Thread.Sleep(seconds * 1000);
        waveIn.StopRecording();
    }

    static async Task<string> TranscribeWithDeepgram(string audioFile, string key)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Token {key}");
        
        var bytes = await File.ReadAllBytesAsync(audioFile);
        var content = new ByteArrayContent(bytes);
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("audio/wav");
        
        var response = await client.PostAsync(
            "https://api.deepgram.com/v1/listen?language=tr&smart_format=true",
            content
        );
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"API Hatası: {response.StatusCode}");
        }
        
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return json.RootElement
            .GetProperty("results").GetProperty("channels")[0]
            .GetProperty("alternatives")[0]
            .GetProperty("transcript")
            .GetString() ?? "Metin bulunamadı";
    }
}
