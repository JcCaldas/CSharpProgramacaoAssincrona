Console.WriteLine("05 Tratamento de Exception em programação Assíncrona \n");



TesteAsync t = new();

/*Caso o bloco 'try-catch' esteja fora de um método assíncrono, e ocorrer uma exceção dentro do método, o bloco 'catch' não será executado.
 Para que isso seja resolvido, deve-se colocar o bloco 'try-catch' dentro do método, pois a a exeção está ocorrendo dentro do método assíncrono.*/
/*try
{
    t.ChamaTarefaAsync();
}
catch (Exception ex)
{

    Console.WriteLine("Este bloco não será executado");
    Console.WriteLine(ex.Message);
}*/

t.ChamaTarefaAsync();

Console.ReadKey();

class TesteAsync
{
    public Task MinhaTarefaAsync()
    {
        return Task.Run(() =>
        {
            Task.Delay(2000);
            throw new Exception("Minha Exception...");
        });
    }

    //antes
    /*public async void ChamaTarefaAsync()
    {
        await MinhaTarefaAsync();
    }*/

    //depois
    public async void ChamaTarefaAsync()
    {
        try
        {

            await MinhaTarefaAsync();

        }
        catch (Exception ex)
        {

            Console.WriteLine("Bloco catch foi executado...");
            Console.WriteLine(ex.Message);
        }
    }
}