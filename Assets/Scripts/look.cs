using UnityEngine;

public class look : MonoBehaviour
{
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        transform.position = (new Vector3(target.position.x, target.position.y , target.position.z -5f  ) - transform.position) / 1;
    }
}
