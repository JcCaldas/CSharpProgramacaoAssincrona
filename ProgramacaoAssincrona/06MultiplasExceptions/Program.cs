Console.WriteLine("06 Tratamento de Multiplas Exceções\n");

await LancaMultiplasExcecoesAsync();

Console.ReadKey();


/*Caso tenha mais de uma exceção e o tratamento seja feito da forma 'convencional', não será possível,
 pois irá capturar apenas a primeira exceção*/
/*static async Task LancaMultiplasExcecoesAsync()
{
    try
    {
        var primeiraTask = Task.Run(() =>
        {
            Task.Delay(1000);
            throw new IndexOutOfRangeException
            ("IndexOutOfRangeException lançada explicitamente.");
        });

        var segundaTask = Task.Run(() =>
        {
            Task.Delay(1000);
            throw new InvalidOperationException
            ("InvalidOperationException lançada explicitamente.");
        });
        await Task.WhenAll(primeiraTask, segundaTask);
    }

    catch (IndexOutOfRangeException ex)
    {
        Console.WriteLine(ex.Message);
    }

    catch (InvalidOperationException ex)
    {
        Console.WriteLine(ex.Message);
    }

}*/

/*Para o tratamento de mais de uma exceção, deve-se criar um objeto do tipo 'Task'
 que receberá as tarefas, fora do bloco 'try-catch', e no bloco catch, utilizar a propriedade 'InnerExceptions'
da classe 'AggregateException', que herda a propriedade 'InnerExceptions' da classe
'Exception'. Após capturar todas as exceções em uma variável, que será uma coleção de
exeções, basta iterar dentro dela, com um laço 'foreach' e utilizando a propriedade
'InnerExceptions' da variável*/
static async Task LancaMultiplasExcecoesAsync()
{
    Task? tarefas = null;
    try
    {
        var primeiraTask = Task.Run(() =>
        {
            Task.Delay(1000);
            throw new IndexOutOfRangeException
            ("IndexOutOfRangeException lançada explicitamente.");
        });

        var segundaTask = Task.Run(() =>
        {
            Task.Delay(1000);
            throw new InvalidOperationException
            ("InvalidOperationException lançada explicitamente.");
        });
        tarefas =  Task.WhenAll(primeiraTask, segundaTask); //WhenAll -> espera todas as tarefas de uma vez
        await tarefas;
    }

    catch
    {
        Console.WriteLine("Ocorreram as seguintes exceções:\n");
        AggregateException todasExceptions = tarefas.Exception;

        foreach (var ex in todasExceptions.InnerExceptions)
        {
            Console.WriteLine ($"{ex.Message}");
            Console.WriteLine (ex.GetType().ToString());

        }
    }

}