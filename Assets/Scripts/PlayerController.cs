using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public GameObject prefab = null;
    public GameObject SEManager = null;

    public float speed = 200f;
    public float bulletSpeed = 2;
    public float area = 5;

    void Start()
    {
    }

    void Update()
    {
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        Vector3 move = new Vector3(Mathf.Round(input.x), Mathf.Round(input.y), 0f);

        Vector3 delta = move * speed;
        Vector3 newPos = transform.position + delta;

        newPos.x = Mathf.Clamp(newPos.x, -area, area);
        newPos.y = Mathf.Clamp(newPos.y, -area, area);

        transform.position = newPos;
    }

    public void OnAttack(InputValue value)
    {
        Debug.Log($"çUåÇÉAÉNÉVÉáÉì[{value.Get<float>()}]");

        GameObject obj = Instantiate(prefab, transform.position + Vector3.forward, Quaternion.identity);
        AudioSource Sorce = SEManager.GetComponent<AudioSource>();
        Sorce.Play();
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
    }
}
