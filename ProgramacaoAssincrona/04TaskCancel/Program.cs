using System.Diagnostics;

namespace _04TaskCancel
{
    class Program
    {
        private static CancellationTokenSource cancellationTokenSource;
        static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                //não precisa pois foi definido tempo
                //cancellationTokenSource = new CancellationTokenSource();
                //cancellationTokenSource?.Cancel();

                //var resultado = await OperacaoLongaduracaoCancelavel(100, cancellationTokenSource.Token);

                //Console.WriteLine("Cancelamento automático após 3 segundos");
                //await ExecutaCancelamentoComTimeout(3000);
                Console.WriteLine("Cancelamento manual");
                await ExecutaCancelamentoManual();

                //Console.WriteLine(resultado);
            }
            catch (Exception)
            {

                Console.WriteLine($"Tarefa Cancelada:" +
                    $"tempo expirado após: {stopwatch.Elapsed}");
            }
            Console.ReadKey();
        }
        private static Task<int> OperacaoLongaduracaoCancelavel(int valor, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Executou Operação de longa duração");

            Task<int> task = null;

            task = Task.Run(() =>
            {
                int resultado = 0;
                for (int i = 0; i < valor; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                        throw new TaskCanceledException(task);

                    Thread.Sleep(50);
                    resultado += i;

                }
                return resultado;
            });
            return task;
        }

        public static async Task ExecutaCancelamentoComTimeout(int tempo)
        {
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(tempo);
                try
                {
                    var resultado = await OperacaoLongaduracaoCancelavel (100,
                        cancellationTokenSource.Token);
                }
                catch (TaskCanceledException)
                {

                    throw;
                }
            }
        }

        public static async Task ExecutaCancelamentoManual()
        {
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                var TecladoTask = Task.Run(() =>
                {
                    Console.WriteLine("Pressione algo para Cancelar...");
                    Console.ReadKey();
                    cancellationTokenSource.Cancel();
                });
                try
                {
                    var tarefa = OperacaoLongaduracaoCancelavel(500, cancellationTokenSource.Token);
                    var resultado = await tarefa;
                    Console.WriteLine($"Resultado: {resultado}");
                }
                catch (TaskCanceledException)
                {

                    throw;
                }
                await TecladoTask;
            }
        }

        private static Task<int> OperacaoLongaDuracao(int valor)
        {
            Console.WriteLine("Executou Operação de longa duração!");

            //inicia a tarefa e retorna
            return Task.Run(() =>
            {
                int resultado = 0;
                //Itera no laço for
                for (int i = 0; i < valor; i++)
                {
                    //gasta o tempo
                    Thread.Sleep(50);
                    resultado += i;
                }
                return resultado;
            });
        }
    }
}