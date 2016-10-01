using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventTest : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.Instance.StartListening<TakeDamageEvent>(SomeFunction);
    }

    void OnDisable()
    {
        EventManager.Instance.StopListening<TakeDamageEvent>(SomeFunction);
    }

    public void SomeFunction(TakeDamageEvent e)
    {
        Debug.Log("The message is: " + e.message);
    }
}