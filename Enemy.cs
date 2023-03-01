using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 3.0f;

    //private bool enemyN1Dead = false;


    private Player _player;
    private UIManager _uiManager;

    //private EnemyN1 _enemyN1;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        //_enemyN1 = GameObject.Find("EnemyN1").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -12f)
        {
            float randomX = Random.Range(-9f, 9f);

            transform.position = new Vector3(randomX, 25, 0);
            //transform.localScale = Vector3.one * Random.Range(0.5f, 0.6f);
        }
    }

    IEnumerator EnemyN1DeathRoutine()
    {
        this.gameObject.GetComponent<Transform>().GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.01f);
        GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.24f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
                StartCoroutine(EnemyN1DeathRoutine());
                //Destroy(this.gameObject);
            }
        }

        if (other.tag == "Laser" || other.tag == "TripleShot")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                StartCoroutine(EnemyN1DeathRoutine());
                _player.PowerAdd(Random.Range(2, 4));

                //Destroy(this.gameObject);
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
