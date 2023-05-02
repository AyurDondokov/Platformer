using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject particlePickUp;
    [SerializeField] float delay;
    [SerializeField] UnityEvent onKeyPickUp;
    

    public async void Picked() 
    {
        Instantiate(particlePickUp, transform.position, Quaternion.identity);
        Destroy(gameObject);
        await Task.Delay(TimeSpan.FromSeconds(delay));
        onKeyPickUp.Invoke();
    }
}
