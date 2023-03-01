using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erythrocyte : MonoBehaviour
{
    private float _speed = 8f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Rotate(new Vector3(55, 0, 0) * 0.5f * Time.deltaTime, Space.World);

        if (transform.position.y < -10f)
        {
            float randomX = Random.Range(-7f, 7f);
            transform.position = new Vector3(randomX, 5, 0);
        }
    }
}
