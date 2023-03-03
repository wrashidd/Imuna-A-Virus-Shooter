using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for Enemy 003 game entity behaviour
Version #1
*/
public class Enemy_003 : MonoBehaviour
{
    private float _speed = 0.5f;
    private int enemy003Life = 100;

    Color enemy003LifeColor = new Color(0.78f, 0.35f, 1);
    Color enemy003DamageColor = new Color(1, 1, 1);
    Color startColor = Color.white;
    Color endColor = Color.black;

    private Player _player;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    // Accesses Player game object
    // Accesses Canvas and Spawn Manager Utility classes
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    // Handles Enemy 003 2D movement and rotation
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -30f)
        {
            float randomX = Random.Range(-8.5f, 8.5f);
            transform.position = new Vector3(randomX, 30, 0);
        }
    }

    // Triggers damages according to game objects it interacts
    private void ondamageColorChange()
    {
        StartCoroutine(ondamageColorChangeRoutine());
    }

    IEnumerator ondamageColorChangeRoutine()
    {
        if (enemy003Life > 0)
        {
            GetComponent<Renderer>().material.color = enemy003DamageColor;
            yield return new WaitForSeconds(0.1f);
            GetComponent<Renderer>().material.color = enemy003LifeColor;
        }
        else
        {
            GetComponent<Renderer>().material.color = enemy003DamageColor;
            yield return new WaitForSeconds(0.01f);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(2.95f);
            Destroy(this.gameObject);
            _spawnManager.enemy003Dead = true;
        }
    }

    // Triggers damages according to game objects it interacts
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("HIt: " + other.transform.name);

        if (other.tag == "Player")
        {
            _player.Damage();
            enemy003Life = enemy003Life - 15;
            ondamageColorChange();
        }

        if (other.tag == "bulletOval")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                enemy003Life = enemy003Life - 15;
                ondamageColorChange();
            }
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            enemy003Life = enemy003Life - Random.Range(1, 2);
            ondamageColorChange();
        }

        if (other.tag == "TripleShot")
        {
            Destroy(other.gameObject);
            enemy003Life = enemy003Life - Random.Range(3, 4);
            ondamageColorChange();
        }

        if (other.tag == "Shield_Powerup")
        {
            enemy003Life = enemy003Life - 10;
            ondamageColorChange();
        }

        if (other.tag == "Invisible")
        {
            Debug.Log("Invisible Tag Detected");
        }
        if (other.tag == "Pass Border") { }
    }
}
