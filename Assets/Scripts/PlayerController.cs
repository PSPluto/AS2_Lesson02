using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Shot Settings")]
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private AudioSource seAudioSource = null;
    [SerializeField] private Transform shotpoint = null;
    [SerializeField] private float bulletSpeed = 2f;
    
    [Header("Shot particle Settings")]
    [SerializeField] private GameObject muzzleFlashPrefab = null;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 200f;
    [SerializeField] private bool tiltInvert = false;

    [Header("Rotation Settings")]
    [SerializeField] private GameObject lookAxis = null;
    [SerializeField] private GameObject gyro = null;

    [Header("barrier Settings")]
    public GameObject barrier;
    public MeshRenderer barrierRenderer;
    public bool barrierActivation;

    private Vector3 lookAngles;
    private float gyroAngle;
    private Vector3 inputMoveVelocity;

    void Update()
    {
        // 前進処理
        transform.Translate(0f, 0f, 5f * Time.deltaTime);

        // 入力による角度計算
        lookAngles.x += (tiltInvert ? -1f : 1f) * inputMoveVelocity.y;
        lookAngles.y += inputMoveVelocity.x;
        gyroAngle += inputMoveVelocity.x;

        // クランプ処理
        lookAngles.x = Mathf.Clamp(lookAngles.x, -15f, 15f);
        lookAngles.y = Mathf.Clamp(lookAngles.y, -15f, 15f);
        gyroAngle = Mathf.Clamp(gyroAngle, -50f, 50f);

        // 回転の適用
        lookAxis.transform.eulerAngles = lookAngles;

        Vector3 gyroEuler = gyro.transform.eulerAngles;
        gyroEuler.z = gyroAngle;
        gyro.transform.eulerAngles = gyroEuler;

        // 復元処理（目標値へ補間）
        lookAngles = Vector3.Lerp(lookAngles, Vector3.zero, Time.deltaTime * 3f);
        gyroAngle = Mathf.Lerp(gyroAngle, 0f, Time.deltaTime * 10f);


    }
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("接触");
        if (collider.transform.tag.Equals("Item/Barrier"))
        {
            Debug.Log("アイテム接触");
            Material m = barrierRenderer.material;
            barrierActivation = true;

            m.SetInt("_IsActive", 1);
        }
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        Vector3 move = new Vector3(
            Mathf.Round(input.x),
            Mathf.Round(input.y),
            0f
        );

        transform.Translate(move * speed);
        inputMoveVelocity = move;
    }

    public void OnAttack(InputValue value)
    {
        if (!value.isPressed) return;

        SpawnBolt();

        seAudioSource.Play();

        SpawnShotParticle();

    }

    public void SpawnBolt()
    {
        GameObject obj = shotpoint != null
            ? Instantiate(prefab, shotpoint.position, shotpoint.rotation)
            : Instantiate(prefab, transform.position + Vector3.forward, Quaternion.identity);
        if (obj.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.AddForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
    public void SpawnShotParticle()
    {
        GameObject obj = Instantiate(muzzleFlashPrefab, transform.position + Vector3.forward, Quaternion.identity);
        Destroy(obj, 3f);
    }
}