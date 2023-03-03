using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for Enemy 001 game entity behaviour
*/
public class Enemy : MonoBehaviour
{
    private float _speed = 3.0f;

    private Player _player;
    private UIManager _uiManager;

    // Start is called before the first frame update
    // Accesses Player class
    // Accesses UIManager Utility class
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    // Handles Enemy 001 down movement and respawning
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -12f)
        {
            float randomX = Random.Range(-9f, 9f);

            transform.position = new Vector3(randomX, 25, 0);
        }
    }

    // Handles Enemy 001 death sequence
    IEnumerator EnemyN1DeathRoutine()
    {
        this.gameObject.GetComponent<Transform>().GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.01f);
        GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.24f);
        Destroy(this.gameObject);
    }

    // On each interaction with the player triggers single damage to the player
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
                StartCoroutine(EnemyN1DeathRoutine());
            }
        }

        if (other.tag == "Laser" || other.tag == "TripleShot")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                StartCoroutine(EnemyN1DeathRoutine());
                _player.PowerAdd(Random.Range(2, 4));
            }
        }

        if (other.tag == "bulletOval")
        {
            Destroy(this.gameObject);
            if (_player != null)
            {
                _player.PowerAdd(Random.Range(2, 4));
            }
        }

        if (other.tag == "Pass Border")
        {
            _uiManager.handleborderPass();
        }

        if (other.tag == "Shield_Powerup")
        {
            StartCoroutine(EnemyN1DeathRoutine());
        }

        if (other.tag == "Invisible")
        {
            Debug.Log("Invisible Tag Detected");
        }
    }
}
