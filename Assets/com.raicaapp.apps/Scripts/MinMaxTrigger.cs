using System;
using UnityEngine;

public class MinMaxTrigger : MonoBehaviour
{
    [SerializeField] int min;
    [SerializeField] int max;

    public static Action<int, int> OnTriggerEnter { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            return;
        }

        OnTriggerEnter?.Invoke(min, max);
    }
}
