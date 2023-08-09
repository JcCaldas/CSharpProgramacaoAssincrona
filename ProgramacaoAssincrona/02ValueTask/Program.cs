Console.WriteLine("02 ValueTask e ValueTask<T>\n");

//ValueTask não aloca memória e nem utiliza o garbage colector

Console.WriteLine("Iniciando a operação assíncrona...");
await MetodoSemRetornoAsync();

Console.WriteLine("\nIniciando a operação assíncrona...");
var resultado = await MetodoRetornaValorAsync(20);
Console.WriteLine($"Resultado:{resultado}");



Console.ReadKey();



static async ValueTask MetodoSemRetornoAsync()
{
    await Console.Out.WriteLineAsync("* Método que não retorna valor");
    await Task.Delay(2000);
}

static async ValueTask<int> MetodoRetornaValorAsync(int valor)
{
    Console.Out.WriteLine("-Método que retorna valor");
    await Task.Delay(2000);
    return valor * 2;
}