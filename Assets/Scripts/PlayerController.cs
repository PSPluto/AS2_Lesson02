using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject prefab = null;
    public GameObject SEManager = null;
    public Transform shotpoint = null;

    public float speed = 200f;
    public float bulletSpeed = 2;
    public float area = 5;

    public GameObject lookAxis;
    public GameObject gyro;
    private Vector3 lookAngles;
    private float gyroAngle;

    private Vector3 inputMoveVelocity;
    public bool tiltInvert;

    void Start()
    {
    }
    void Update()
    {
        float zSpeed = 5f * Time.deltaTime;
        transform.Translate(0f, 0f, zSpeed);

        lookAngles.x += (tiltInvert ? -1 : 1) * inputMoveVelocity.y;
        lookAngles.y += inputMoveVelocity.x;
        gyroAngle += inputMoveVelocity.x;

        lookAngles.x = Mathf.Clamp(lookAngles.x, -15f, 15f);
        lookAngles.y = Mathf.Clamp(lookAngles.y, -15f, 15f);
        gyroAngle = Mathf.Clamp(gyroAngle, -50f, 50f);


        lookAxis.transform.eulerAngles = lookAngles;

        Vector3 gyroEuler = gyro.transform.eulerAngles;
        gyroEuler.z = gyroAngle;
        gyro.transform.eulerAngles = gyroEuler;


        // 目標値に近づける
        lookAngles = Vector3.Lerp(lookAngles, Vector3.zero, Time.deltaTime * 3f);
        gyroAngle = Mathf.Lerp(gyroAngle, 0f, Time.deltaTime * 10f);


    }


    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        Vector3 move = new Vector3(
            Mathf.Round(input.x),
            Mathf.Round(input.y
        ), 0f);

        Vector3 delta = move * speed;
        transform.Translate(delta);

        inputMoveVelocity = move;
    }

    public void OnAttack(InputValue value)
    {
        Debug.Log($"攻撃アクション[{value.Get<float>()}]");
        GameObject obj;
        if (shotpoint != null)
        {
            obj = Instantiate(prefab, shotpoint.position, shotpoint.rotation);
        }
        else
        {
            obj = Instantiate(prefab, transform.position + Vector3.forward, Quaternion.identity);
        }
        AudioSource Sorce = SEManager.GetComponent<AudioSource>();
        Sorce.Play();
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
    }
}
