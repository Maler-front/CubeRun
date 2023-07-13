using UnityEngine;
using EventBusNS;

public class Printer : MonoBehaviour
{
    void Start()
    {
        EventBus.Instance.Subscribe<WantToPrintSignal>(Print);
    }

    private void Print(WantToPrintSignal signal)
    {
        string text = signal.line;
        Debug.Log(text != null ? text : "");
        EventBus.Instance.Unsubscribe<WantToPrintSignal>(Print);
    }
}
