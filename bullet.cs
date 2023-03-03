using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is resposible for default bullet actions
*/
public class bullet : MonoBehaviour
{
    private float _speed = 20f;

    Color emissionColorMagenta = new Color(0.307f, 0.019f, 1.0f);

    // Update is called once per frame
    // Runs bullets shoot and scale up coroutine
    void Update()
    {
        StartCoroutine(shotScalorRoutine());
    }

    // Triggers bullet rescaling and its destruction
    IEnumerator shotScalorRoutine()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);

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
