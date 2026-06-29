using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        // TODO: Bolt‘¤‚Éˆ—ˆÚ‚·
        if (collision.transform.tag == "Bolt")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
