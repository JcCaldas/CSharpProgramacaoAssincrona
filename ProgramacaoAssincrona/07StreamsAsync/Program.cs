internal class Program
{
    //síncrono
    //private static void Main(string[] args)
    
    //assíncrono
    private static async Task Main(string[] args)
    {
        Console.WriteLine("07 - Streams Assíncronos\n");

        /*Modo síncrono de acesso:*/

        var meses = new List<string>() { "janeiro", "fevereiro", "março", "abril" };
        foreach (var mes in meses)
        {
            Console.WriteLine(mes);
        }
        
        Console.WriteLine();

        foreach (var mes in GetMeses())
        {
            Console.WriteLine(mes);
        }

        Console.WriteLine();

        /*Modo assíncrono*/
        await foreach (var mes in AsyncGetMeses())
        {
            Console.WriteLine(mes);
        }
    }
    
    /*Modo síncrono de acesso:*/
    private static IEnumerable<string> GetMeses()
    {
        yield return "Janeiro";
        yield return "Fevereiro";
        yield return "Março";
        yield return "Abril";
    }

    /*Modo assíncrono
    Pode-se usar streams assíncronos para criar IEnumerables que geram dados de forma assíncrona
    Requisitos:
    1- O método deve ser assíncrono (async/await)
    2- O tipo de retorno do método deve ser um IAsyncEnumerable<T>
    3- O corpo do método deve conter pelo menos um chamada a yield return

    Implementação apartir:
    .NET 5.0
    .NET Core 3.0 
    .NET Standard 2.1*/

    private static async IAsyncEnumerable<string> AsyncGetMeses()
    {
        yield return "Janeiro";
        yield return "Fevereiro";
        yield return "Delay de 2 segundos";
        await Task.Delay(2000);
        yield return "Março";
        yield return "Abril";
    }
}
