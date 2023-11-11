using System.Threading.Tasks;
using System;

public class FunctionCaller
{
    private Action action;

    public static async void RepeatAudio(Action function, float waitTime)
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(waitTime));
            function.Invoke();
        }
    }
}
