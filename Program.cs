using System.Text.Json;

var reference = Enumerable.Range(1, 5);
var processes = reference.Select(index => new Process()
{
    Index = index - 1,
    Label = $"P{index}",
    State = reference.Select(p => p == index ? 0 : -1).ToArray()
}).ToArray();

Console.WriteLine(JsonSerializer.Serialize(processes));

Task.Factory.StartNew(async () =>
{
    var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

    while (true)
    {
        await timer.WaitForNextTickAsync();

        Console.WriteLine("\n\n-----------RODADA DE TESTE-----------");

        foreach (var process in processes)
        {
            process.Test(processes);
            Console.WriteLine($"Processo {process.Label}: {(process.IsFailed ? "FALHO" : JsonSerializer.Serialize(process.State))}");
        }
    }
}, TaskCreationOptions.LongRunning);

while (true)
{
    Console.Write("Digite o número do processo que falhou (1 a 5): ");

    var index = int.TryParse(Console.ReadLine(), out var value) ? value : 1;
    Console.WriteLine($"O processo {index} irá falhar");

    if (index >= 1 && index <= 5)
        processes[index - 1].IsFailed = true;
}