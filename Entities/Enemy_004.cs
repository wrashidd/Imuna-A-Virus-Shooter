using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for Enemy 004 game entity behaviour
*/
public class Enemy_004 : MonoBehaviour
{
    private float _speed = 0.45f;

    private Player _player;
    private UIManager _uiManager;

    // Start is called before the first frame update
    // Accesses Player game object
    // Accesses UIManager Utility class
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    // Handles Enemy 004 down movement
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    // Handles Enemy 004 death sequence
    IEnumerator EnemyN1DeathRoutine()
    {
        this.gameObject.GetComponent<Transform>().GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.01f);
        GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.24f);
        Destroy(this.gameObject);
    }

    // On interaction with the player triggers damage to the player
    // On interaction with the bullets and Sheild PowerUp triggers damage to itself
    // Removes this game object on passing bottom border
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
                _player.PowerAdd(Random.Range(2, 4));
                StartCoroutine(EnemyN1DeathRoutine());
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
