using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletOval : MonoBehaviour
{
    private float _speed = 18.0f;

    private void Start() { }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(shotScalorRoutine());
    }

    IEnumerator shotScalorRoutine()
    {
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

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
