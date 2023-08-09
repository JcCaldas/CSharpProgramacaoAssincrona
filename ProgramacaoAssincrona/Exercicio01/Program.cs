Console.WriteLine("Exercício 01\n");

/*
Exercicio de fixação realizado com a aula

- Realizar o dowload de um arquivo da internet utilizando a classe 'HttpClient'
 
- Utilizar programação assíncrona para que o aplicativo não trave durante as operações de download

- Tratar as possíveis exceções que podem ocorrer durante o download

- No tratamento de exceção o programa deve informar ao usuário sobre o erro

- Permitir que a operação seja cancelada após um período de tempo, se isso ocorrer, deve-se informar ao usuário que
a operação foi cancelada

- Ao final, o aplicativo deve exibir uma mensagem que o download foi bem sucedido

Obs: Para o cancelamento deve-se utilizar a classe 'CancellationTokenSource' e o método 'IsCancellationRequested'
*/

Console.ReadKey();

static async Task ExecutaOperacaoAsync()
{
    //1º - criar um 'CancellationTokenSource'
    var tempo = 10;
    var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(tempo));//TimeSpan.FromSeconds -> define o tempo, nesse caso em segundos

    Console.WriteLine("\nIniciando o download do arquivo...");
    Console.WriteLine($"A operação será cancelada após {tempo} segundos...");

	//2º - tratamento de exceções
	try
	{

		using var httpClient = new HttpClient(); //após a instância ser utilizada será liberada
		var destino = @"c:\temp\teste\arquivo.txt";
		var urlDestino = "https://www.macoratti.net/dados/Poesia.txt";
		//Desafio Pessoal: criar um método que verifica se o diretório existe, se não existir deve-se cria-lo

		//

    }

    catch (OperationCanceledException ex) //Lançada quando uma operação assíncrona é cancelada
	{

	}

	catch (HttpRequestException ex) //Lançada quando ocorre algum erro na requição http
	{

	}
	catch (Exception)
	{

		throw;
	}
}
