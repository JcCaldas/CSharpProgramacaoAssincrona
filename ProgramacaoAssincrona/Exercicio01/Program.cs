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

await ExecutaOperacaoAsync();

Console.ReadKey();

static async Task ExecutaOperacaoAsync()
{
    //1º - criar um 'CancellationTokenSource'
    var tempo = 20;
    var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(tempo));//TimeSpan.FromSeconds -> define o tempo, nesse caso em segundos

    Console.WriteLine("\nIniciando o download do arquivo...");
    Console.WriteLine($"A operação será cancelada após {tempo} segundos...");

    //2º - tratamento de exceções
    try
    {

        using var httpClient = new HttpClient(); //após a instância ser utilizada será liberada
        var diretorio = @"c:\temp\teste\";
        var arquivo = "arquivo.txt";
        var destino = Path.Combine(diretorio, arquivo);
        var urlDestino = "https://www.macoratti.net/dados/Poesia.txt";
        //Desafio Pessoal: criar um método que verifica se o diretório existe, se não existir deve-se cria-lo

        VerificaDiretorio(diretorio);


        //Método do httpClient, Assíncrono que retorna uma Task
        //Com o await, o compilador aguarda a tarefa ser concluída antes de ir para a próxima linha de código
        var response = await httpClient.GetAsync(urlDestino,
            HttpCompletionOption.ResponseHeadersRead, //Especifica que a resposta do servidor seja lida por partes. Primeiro a cabeçalho, depois a respota do corpo do request. Isso reduz a memória e melhora o desempenho
            cancellationTokenSource.Token); //Indica que que a operação pode ser cancelada

        //Armazena o tamanho do arquivo que será feito o download para monitorar o progresso do download
        var totalBytes = response.Content.Headers.ContentLength;

        //variável para indicar os bytes que já foram lidos
        var readBytes = 0L; //'L' indica que é um 'long'

        //criação do arquivo e preparação para a escrita
        //'await 'vai aguardar todo o processo ser concluido antes de ir para a próxima linha de código
        //'using' vai garantir que o objeto 'fileStream' seja fechado e liberado da memória após o uso
        await using var fileStream = new FileStream(destino, FileMode.Create,
                                                    FileAccess.Write);

        //Ler o conteúdo da resposta do método Assíncrono
        //Lê do conteúdo da resposta (response) o fluxo de dados como uma string.
        //Obs: a maioria dos método assícronos no C# suportam o cancellationToken.
        await using var contentStream = await response.Content.ReadAsStreamAsync(cancellationTokenSource.Token);

        //Separa um espaço na memória para ler os bytes do 'contentStream'
        /*Cria um array de bytes. O espaço não pode ser muito grande, para não ocupar muita memória e 
         nem muito pequeno, para não ocupar muita operação em disco*/
        var buffer = new byte[81920];

        //Quantidade de bytes lidos
        int bytesRead;

        //Laço para ler o conteúdo do arquivo e gravar na variável de destino
        /*condição: gravar na variável 'bytesRead', os bytes lidos de forma assíncrona,
         o conteúdo de 'contentStream', enquanto o conteúdo de 'contentStream' for maior que '0'
        ou seja, houver coisas para ler.
        parâmetros do 'ReadAsync': 
        1º - array de bytes que vai utilizar para gravar os bytes lidos
        2º - é o quanto já foi lido do buffer ('0' pois não foi lido nada)
        3º - número máximo de bytes lidos no buffer (nesse caso aprox. 80Kb)
        4º - 'cancellationToken' para permitir o cancelamento*/
        while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length,
                            cancellationTokenSource.Token)) > 0)
        {
            //escreve no arquivo de forma assíncrona
            await fileStream.WriteAsync(buffer, 0, bytesRead, cancellationTokenSource.Token);

            //implementa os bytes lidos
            readBytes += bytesRead;
            Console.WriteLine($"Progresso: {readBytes}/{totalBytes}");
        }



    }

    catch (OperationCanceledException ex) //Lançada quando uma operação assíncrona é cancelada
    {
        //verifica se o tempo limite foi atingido (nesse caso do exercício)
        if (cancellationTokenSource.IsCancellationRequested)
        {
            Console.WriteLine($"\nDownload cancelado por tempo limite: {ex.Message}");
        }

        else
        //tempo do servidor, caso o cancellationToken não seja ativo devido a um tempo muito grande definido
        { 
            Console.WriteLine("\nO tempo limite para o download foi atingido.");
        }
    }

    catch (HttpRequestException ex) //Lançada quando ocorre algum erro na requição http
    {
        Console.WriteLine($"\nOcorreu um erro de rede: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nOcorreu um erro desconhecido: {ex.Message}");
    }
    finally
    {
        Console.WriteLine("\nDownload finalizado!");
        Console.WriteLine("Fim do processamento...\nPressione qualquer tecla para continuar...");
    }

    static async void VerificaDiretorio(string diretorio)
    {
        if (!Directory.Exists(diretorio))
        {
            Directory.CreateDirectory(diretorio);
            Console.WriteLine("\nDiretório criado");
        }

        else
        {
            Console.WriteLine("\nDiretório encontrado");
        }
    }
}
