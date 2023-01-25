using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 2.0f;

    [SerializeField]
    private int powerupID; // 0 = Triple Shot, 1 = Speed, 2 = Shields 
    

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5)
        {
            Destroy(this.gameObject);
        }


    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Invisible")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    
                    case 1:
                        player.bulletSuperActivate();
                        Debug.Log("Fire Super Collected");
                        break;


                    case 2:
                        player.ShieldPlayerActivator();
                        Debug.Log("Shield Collected");
                        break;

                    default:
                        Debug.Log("Powerup Script Switch is defaulted");
                        break;

                }

            }

            

            Destroy(this.gameObject);

        }



    }
}
       
