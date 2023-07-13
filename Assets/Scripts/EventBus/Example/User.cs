using UnityEngine;

public class User : MonoBehaviour
{
    void Update()
    {
        EventBusNS.EventBus.Instance.Invoke<WantToPrintSignal>(new WantToPrintSignal("Hello world!"));
    }
}
