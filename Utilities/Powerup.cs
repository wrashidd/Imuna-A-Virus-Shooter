using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for Reward System
*/
public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;

    [SerializeField]
    private int powerupID; // 0 = Triple Shot, 1 = Speed, 2 = Shields

    // Update is called once per frame
    // Move's a power up object
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5)
        {
            Destroy(this.gameObject);
        }
    }

    // Activates one of the power ups - Super and Triple Shot power ups
    // Activates protective shield power up
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
