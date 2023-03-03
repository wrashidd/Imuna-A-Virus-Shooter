using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for Enemy 005 game entity behaviour
*/
public class Enemy_005 : MonoBehaviour
{
    private float _speed = 8.0f;
    private Player _player;

    Color normalColor = new Color(0.472f, 0.472f, 0.472f);
    Color damageColor = new Color(1f, 0.96f, 0.08f);

    // Start is called before the first frame update
    // Accesses Player class
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    // Spawns Enemy005 in the game
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
            Destroy(this.gameObject);
        }
    }

    // Changes color on collision with the player
    IEnumerator onCollisionWithPlayerRoutine()
    {
        transform.GetComponent<Renderer>().material.SetColor("_BaseColor", damageColor);
        yield return new WaitForSeconds(0.2f);
        transform.GetComponent<Renderer>().material.SetColor("_BaseColor", normalColor);
    }

    // On interaction with the player triggers double damage to the player
    // On interaction with the bullets triggers damage to itself
    // On receiving damage from Oval bullet power ups the player
    // On receiving damage from Laser or Triple bullet destroys this game object
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
                _player.Damage();
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
    }
}
