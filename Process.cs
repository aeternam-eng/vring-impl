using System.Text.Json;

public class Process 
{
  public int Index { get; init; }
  public string Label { get; init; } = string.Empty;
  public int[] State { get; set; } = new int[5];

  public bool IsFailed { get; set; } = false;

  public void Test(Process[] processes)
  {
    if (!IsFailed) 
    {
      var failedStuff = Enumerable.Empty<int>().ToList();
      var testedIndex = (Index + 1) % 5;
      var testedProcess = processes[testedIndex];

      do
      {
        testedProcess = processes[testedIndex];
        State[testedIndex] = testedProcess.IsFailed
            ? 1
            : 0;
        
        if (testedProcess.IsFailed)
        {
          failedStuff.Add(testedIndex);
          testedIndex = (testedIndex + 1) % 5;
        }
        else
        {
          State = testedProcess.State.Select((item, index) => {
            if (failedStuff.Contains(index))
              return 1;
            
            if (index == Index || index == testedIndex)
              return 0;

            return item;
          }).ToArray();
        }
      } while (testedProcess.IsFailed && testedIndex != Index);
    }
  }
}