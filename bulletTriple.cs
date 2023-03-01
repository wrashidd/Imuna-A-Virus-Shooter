using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletTriple : MonoBehaviour
{
    private float _speed = 14.0f;

    private void Start() { }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(shotScaleRoutine());
    }

    IEnumerator shotScaleRoutine()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);

        if (
            transform.position.y >= 7f
            || transform.position.y <= -7f
            || transform.position.x >= 12f
            || transform.position.x <= -12f
        )
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
