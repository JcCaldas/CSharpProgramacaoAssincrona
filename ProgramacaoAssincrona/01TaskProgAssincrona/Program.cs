using _01TaskProgAssincrona;

Console.WriteLine("01 Task Programação Assíncrona\n");

Console.WriteLine("### Café da manhã Síncrono ###");
CafeDaManha();

Console.WriteLine("Fim processamento do café da manhã síncrono");
Console.ReadKey();

Console.WriteLine("### Café da manhã Assíncrono ###");
CafeDaManhaAsync();


Console.ReadKey();

//Café da manhã Síncrono
static void CafeDaManha()
{
    Console.WriteLine("\n-- Preparar o café");
    var cafe = PrepararCafe();

    Console.WriteLine("\n** Preparar o pão");
    var pao = PrepararPao();

    ServirCafe (cafe, pao);
}

static void ServirCafe(Cafe cafe, Pao pao)
{
    Console.WriteLine("\n>> Servindo o café da manhã");
    Thread.Sleep(2000);
    Console.WriteLine(">>Café servido");
}

static Cafe PrepararCafe()
{
    Console.WriteLine("-Fervendo a água");
    Thread.Sleep(2000);
    Console.WriteLine("-Coando o café");
    Thread.Sleep(2500);
    Console.WriteLine("-Adoçando o café");
    return new Cafe();
}

static Pao PrepararPao()
{
    Console.WriteLine("*Partit o pão");
    Thread.Sleep(2000);
    Console.WriteLine("*Passa manteiga no pão");
    Thread.Sleep(2500);
    Console.WriteLine("*Tostar o pão");
    return new Pao();
}

//Café da manhã Assíncrono
static async void CafeDaManhaAsync()
{
    Console.WriteLine("\n-- Preparar o café");
    var tarefaCafe = PrepararCafeAsync();

    Console.WriteLine("\n** Preparar o pão");
    var tarefaPao = PrepararPaoAsync();

    var cafe = await tarefaCafe; //aguarda a tarefa e retorna o resultado.
    var pao = await tarefaPao; //aguarda a tarefa e retorna o resultado.

    ServirCafe(cafe, pao);
}

//Método "ServirCafe" só ocorre após as tarefas estarem prontas.

static async Task<Cafe> PrepararCafeAsync() //indica ao compilador que é um método assíncrono, e no retorno indico que é um Task (tarefa) do tipo Cafe.
{
    Console.WriteLine("-Fervendo a água");
    await Task.Delay(2000); //pausa assíncrona
    Console.WriteLine("-Coando o café");
    await Task.Delay(2500);
    Console.WriteLine("-Adoçando o café");
    return new Cafe();
}

static async Task<Pao> PrepararPaoAsync() //indica ao compilador que é um método assíncrono, e no retorno indico que é um Task (tarefa) do tipo Pao.
{
    Console.WriteLine("*Partit o pão");
    await Task.Delay(2000); //pausa assíncrona
    Console.WriteLine("*Passa manteiga no pão");
    await Task.Delay(2500);
    Console.WriteLine("*Tostar o pão");
    return new Pao();
}