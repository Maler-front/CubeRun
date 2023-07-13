using EventBusNS.Signals;

public class WantToPrintSignal : IEventBusSignal
{
    public string line;

    public WantToPrintSignal(string line)
    {
        this.line = line;
    }
}
