public class Process 
{
    public int Index { get; init; }
    public string Label { get; init; } = string.Empty;
    public int[] State { get; init; } = new int[5];

    public bool IsFailed { get; set; } = false;

    public void Test(Process[] processes)
    {
        if (!IsFailed) {
            var testedIndex = (Index + 1) % 5;
            var testedProcess = processes[testedIndex];

            do
            {
                testedProcess = processes[testedIndex];

                State[testedIndex] = testedProcess.IsFailed
                    ? -1
                    : State[testedIndex] + 1;
                
                if (testedProcess.IsFailed)
                    testedIndex = (testedIndex + 1) % 5;
            } while (testedProcess.IsFailed && testedIndex != Index);
        }
    }
}
