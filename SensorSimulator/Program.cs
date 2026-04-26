using Shared;
using System.Net.Http.Json;


var http = new HttpClient();
int index = 0;

while (true)
{
    var sensor = new SensorData
    {
       
        Temperatura = new Random().Next(20,100),
        Umidade = new Random().Next(1, 100),
        Timestamp = DateTime.Now
    };
    try
    {
        var response = await http.PostAsJsonAsync("https://localhost:44379/api/v1/sensores", sensor);

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[ERRO] {response.StatusCode} - {erro}");
        }
        else
        {
            Console.WriteLine($"[SUCESSO] {sensor.Timestamp:HH:mm:ss} -> Temp: {sensor.Temperatura}°C | Umid: {sensor.Umidade}%");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[FALHA DE CONEXÃO] A API está rodando? Erro: {ex.Message}");
    }

    await Task.Delay(2000); // Intervalo de 2 segundos
}
