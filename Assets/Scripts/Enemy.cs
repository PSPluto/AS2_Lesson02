using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision)
    //}
}
