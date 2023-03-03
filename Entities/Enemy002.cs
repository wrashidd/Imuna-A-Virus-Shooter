using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

/*
This class is resposible for Enemy 002 game object behaviour
*/
public class Enemy002 : MonoBehaviour
{
    private float _speed = 4.0f;

    [SerializeField]
    private GameObject AccessSpawn_Manager;

    private Player _player;
    private UIManager _uiManager;

        // Update is called once per frame
    // Handles Enemy 002 down movement and respawning
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // move down at 4 min/s
        //if reach bottom respawn at top with a new random x position

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -15f)
        {
            float randomX = Random.Range(-7.5f, 7.5f);
            transform.position = new Vector3(randomX, 10, 0);
            transform.localScale = Vector3.one * Random.Range(1.0f, 1.2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
                Destroy(this.gameObject);
            }
        }

        if (other.tag == "Laser" || other.tag == "TripleShot")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.PowerAdd(Random.Range(2, 4));
                Destroy(this.gameObject);
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
            Destroy(this.gameObject);
        }

        if (other.tag == "Invisible")
        {
            Debug.Log("Invisible Tag Detected");
        }
    }
}
