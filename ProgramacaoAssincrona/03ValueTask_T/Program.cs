Console.WriteLine("03 ValueTask<T>\n");

Console.WriteLine("Informe o primeiro número inteiro:");
int num1 = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("\nInforme o segundo número inteiro:");
int num2 = Convert.ToInt32(Console.ReadLine());

var soma = /*await*/ CalcularSoma(num1, num2).Result;//utilizando o result, ele bloqueia a Thread principal

Console.ForegroundColor = ConsoleColor.Yellow;//altera a cor do console
Console.WriteLine($"\n{num1} + {num2} = {soma}");



Console.ReadKey();

static async ValueTask<int> CalcularSoma(int num1, int num2)
{
    if (num1 == 0 && num2 == 0)
        return 0;

    return await Task.Run(() => num1 + num2);//Usado para executar algo em uma nova Thread, separada da Thread principal. Utilizando quando não se pode ocupar a Thread principal. Retorna um Task
}