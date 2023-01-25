using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDown : MonoBehaviour
{

    private float _speed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            //rotate -180 this prefab
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

            if (transform.position.y >= -4f)
            {
                transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
            }


            if (transform.position.y <= -7f)
            {
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                Destroy(this.gameObject);
            }
        
    }
}
