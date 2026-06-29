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
    private void Update()
    {
        RayCast();
    }
    IEnumerator killtimer()
    {
        yield return new WaitForSeconds(5f);
        GameObject.Destroy(gameObject);
    }
    public void RayCast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit, 1.0f, 1 >> 0))
        {
            Debug.Log($"hit:{hit.transform?.name }");
            if (hit.transform.name.Contains("Enemy"))
            {
                Destroy(hit.transform.gameObject);
                Destroy(this);
            }
        }
    }
}
