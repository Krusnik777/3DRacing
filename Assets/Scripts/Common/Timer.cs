using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public event UnityAction EventOnFinished;

    [SerializeField] private float m_time;

    private float value;
    public float Value => value;

    private void Start()
    {
        value = m_time;
    }

    private void Update()
    {
        value -= Time.deltaTime;

        if (value <= 0)
        {
            enabled = false;

            EventOnFinished?.Invoke();
        }
    }
}
