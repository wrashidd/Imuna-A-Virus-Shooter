using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for Triple bullet actions
*/
public class bulletTriple : MonoBehaviour
{
    private float _speed = 14.0f;

    // Update is called once per frame
    // Runs bullets shoot and scale up coroutine
    void Update()
    {
        StartCoroutine(shotScaleRoutine());
    }

    // Triggers bullet rescaling and its destruction sequence
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
