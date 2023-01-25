using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_005 : MonoBehaviour
{



    private float _speed = 8.0f;
    private Player _player;

    Color normalColor = new Color(0.472f, 0.472f, 0.472f);
    Color damageColor = new Color(1f, 0.96f, 0.08f);
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemy005();
       
    }


    void spawnEnemy005()
    {
        
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        float rotateYAmount = Random.Range(0.1f, 0.4f);
        transform.Rotate(0, rotateYAmount, 0.01f, Space.Self);
        if (transform.position.y < -24.0f)
        {
            /*float RandomX = Random.Range(-9.0f, 9.0f);
            float RandomZ = Random.Range(0, 2.15f);
            float RandomY = Random.Range(13f, 25f);

            transform.position = new Vector3(RandomX, RandomY,0);
            transform.localScale = Vector3.one * Random.Range(0.3f, 1f);*/
            Destroy(this.gameObject);

        }
    }

    IEnumerator onCollisionWithPlayerRoutine()
    {
     transform.GetComponent<Renderer>().material.SetColor("_BaseColor", damageColor);
        yield return new WaitForSeconds(0.2f);
      transform.GetComponent<Renderer>().material.SetColor("_BaseColor", normalColor);
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
                _player.Damage();

                
                //Destroy(this.gameObject);
            }

        }

        


        if (other.tag == "Laser" || other.tag == "TripleShot")
        {
            Destroy(other.gameObject);

        }

        if (other.tag == "bulletOval")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            _player.PowerAdd(Random.Range(5, 8));

        }


        /* if (other.tag == "Pass Border")
         {
             _uiManager.handleborderPass();

         }*/

        /*   if (other.tag == "Shield_Powerup")
           {
               StartCoroutine(EnemyN1DeathRoutine());
           }*/


    }

}
