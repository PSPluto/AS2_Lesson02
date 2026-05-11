using System.Collections;
using System.Threading;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(killtimer());
    }
    IEnumerator killtimer()
    {
        yield return new WaitForSeconds(2f);
        GameObject.Destroy(gameObject);
    }
}
