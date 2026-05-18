using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public GameObject prefab = null;
    public GameObject SEManager = null;
    [SerializeField] public Transform shotpoint = null;

    public float speed = 200f;
    public float bulletSpeed = 2;
    public float area = 5;

    void Start()
    {
    }

    // 計画（疑似コード）:
    // 1. 毎フレーム呼ばれる `Update` 内で前方移動量を計算する。
    //    - 変数名は `zSpeed`
    //    - フレームレートに依存しないよう `Time.deltaTime` を掛ける
    //    - 元の実装意図を尊重して固定値 5f を速度係数として用いる
    // 2. 正しい `transform` インスタンスの `Translate` を呼び出す（静的な `Transform` 型ではない）
    // 3. 文末にセミコロンを付けて構文エラーを解消する
    // 4. 最小限の修正に留め、他の挙動は変更しない
    void Update()
    {
        float zSpeed = 5f * Time.deltaTime;
        transform.Translate(0f, 0f, zSpeed);
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
