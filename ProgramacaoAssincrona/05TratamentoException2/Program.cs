Console.WriteLine("05 Tratamento Exception parte 2\n");



Console.ReadKey();


static async Task LancaExceptionAsync()
{
	try
	{
		var primeiraTask = Task.Run(() =>
		{
			Task.Delay(1000);
			throw new IndexOutOfRangeException
			("IndexOutOfRangeException lançada explicitamente.");
		});
		await primeiraTask;//await na tarefa, a exceção é capturada. Caso contrario o compilador não captura a exceção.
	}
	catch (Exception ex)
	{

		Console.WriteLine("Bloco catch executado");
		Console.WriteLine($"{ex.Message}");
    }
}