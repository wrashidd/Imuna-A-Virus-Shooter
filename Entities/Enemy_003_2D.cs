using System.Collections;
using UnityEngine;

/*
This class is responsible for Enemy 003 game entity behaviour
Version #2
*/
public class Enemy_003_2D : MonoBehaviour
{
    private float _speed = 0.5f;
    private int enemy003Life = 100;
    private Player _player;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;

    [SerializeField]
    private ParticleSystem damageParticle_4SingleShot;

    [SerializeField]
    private ParticleSystem damageParticle_4TripleShot;

    [SerializeField]
    private ParticleSystem damageParticle_4SuperShot;

    [SerializeField]
    private ParticleSystem damageParticle_4Death;
    private int shotID;

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
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);
        transform.Rotate(0f, 0f, 0.01f, Space.World);

        if (transform.position.y < -30f)
        {
            float randomX = Random.Range(-8.5f, 8.5f);
            transform.position = new Vector3(randomX, 30, 0);
        }
    }

    // Runs color change on damage routine and destruction sequence
    IEnumerator ondamageColorChangeRoutine()
    {
        if (enemy003Life > 0)
        {
            gameObject.GetComponent<Transform>().GetChild(0).gameObject.SetActive(false);
            DamageParticlePFX();
            yield return new WaitForSeconds(0.05f);
            gameObject.GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            gameObject.GetComponent<Transform>().GetChild(0).gameObject.SetActive(false);
            gameObject.GetComponent<Transform>().GetChild(1).gameObject.SetActive(false);
            shotID = 3;
            DamageParticlePFX();
            gameObject.GetComponent<Transform>().GetChild(2).gameObject.SetActive(true);
            gameObject.GetComponent<Transform>().GetComponent<CapsuleCollider>().enabled = false;

            yield return new WaitForSeconds(2.95f);
            Destroy(this.gameObject);
            _player.PowerAdd(20);
            _spawnManager.enemy003Dead = true;
        }
    }

    // Instantiates one of the 4 types of Particle Effects
    private void DamageParticlePFX()
    {
        switch (shotID)
        {
            case 0:
                Instantiate(
                    damageParticle_4SingleShot,
                    transform.position,
                    transform.rotation * Quaternion.Euler(0f, 90f, 0f)
                );
                break;
            case 1:
                Instantiate(
                    damageParticle_4TripleShot,
                    transform.position,
                    transform.rotation * Quaternion.Euler(0f, 90f, 0f)
                );
                break;
            case 2:
                Instantiate(
                    damageParticle_4SuperShot,
                    transform.position,
                    transform.rotation * Quaternion.Euler(0f, 90f, 0f)
                );
                break;
            case 3:
                Instantiate(
                    damageParticle_4Death,
                    transform.position + new Vector3(0f, 1f, 0f),
                    transform.rotation * Quaternion.Euler(0f, 90f, 0f)
                );
                break;
            default:
                print("Enemy_003: No damage!");
                break;
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
            StartCoroutine(ondamageColorChangeRoutine());
        }

        if (other.tag == "bulletOval")
        {
            Destroy(other.gameObject);
            shotID = 2;

            if (_player != null)
            {
                enemy003Life = enemy003Life - 20;
                StartCoroutine(ondamageColorChangeRoutine());
            }
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            shotID = 0;
            enemy003Life = enemy003Life - Random.Range(1, 2);
            StartCoroutine(ondamageColorChangeRoutine());
        }

        if (other.tag == "TripleShot")
        {
            Destroy(other.gameObject);
            shotID = 1;
            enemy003Life = enemy003Life - Random.Range(3, 4);
            StartCoroutine(ondamageColorChangeRoutine());
        }

        if (other.tag == "Shield_Powerup")
        {
            enemy003Life = enemy003Life - 25;
            shotID = 2;
            StartCoroutine(ondamageColorChangeRoutine());
        }

        if (other.tag == "Invisible")
        {
            Debug.Log("Invisible Tag Detected");
        }
        if (other.tag == "Pass Border")
        {
            _player.Damage();
            _player.Damage();
            _uiManager.handleborderPass();
            _uiManager.playerlifeCenterScreenMessage(-2);
        }
    }
}
