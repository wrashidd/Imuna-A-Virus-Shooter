using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject playerAssembler;

    [SerializeField]
    private GameObject passBorder;

    [SerializeField]
    private float _speed = 11.0f;
    private float _speedMultiplier = 2.5f;

    [SerializeField]
    private float _Rotspeed = 150.0f;
    private float _RotspeedMultiplier = 2f;
    public ParticleSystem playerDeathParticals;

    private UIManager _uiManager;

    //[SerializeField]
    private int playerScore = 0;
    private int addLifeAfterPoints = 100;
    private int playerLife = 10;

    private GameObject _passBorder;

    private int powerUptime = 25;

    [SerializeField]
    private GameObject _bulletSinglePrefab;

    [SerializeField]
    private GameObject _laserTriplePrefab;
    private bool _laserShotOnOFF = true;

    private bool tripleshotActive = false;
    private int bulletTripleCount = 0;
    private bool bulletTripleOnOFF = false;

    private bool bulletSuperOnOFF = false;
    private bool bulletSuperActive = false;
    private int bulletSuperCount = 0;

    public int enemy003TripleShotBonus = 0;

    [SerializeField]
    private GameObject _bulletOval;

    private bool shieldPlayerActive = false;

    [SerializeField]
    private GameObject shieldPlayerVisualizer;

    private float _fireRate = 0.1f;

    private float _canFire = -1f;

    private SpawnManager spawnManager;
    private GameObject _uiLifeColorChange;

    private Background background;
    private Background_B1 background_b1;

    Color emissionColorWhite = new Color(0.345f, 0.345f, 0.345f);
    Color emissionColorMagenta = new Color(0.307f, 0.019f, 1.0f);
    Color emissionColorGreen = new Color(0.6498f, 0.7264f, 0.4248f);
    Color uiGreenColor = new Color(0.741f, 0.878f, 0.863f);
    Color uiDamageColor = new Color(0.576f, 0.314f, 0.941f);
    Color uiNotActiveColor = new Color(0.25f, 0.25f, 0.25f);
    Color uibulletSuperColor = new Color(0.561f, 0.831f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        playerStart();
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiLifeColorChange = GameObject
            .Find("Canvas")
            .GetComponent<Transform>()
            .GetChild(1)
            .gameObject.GetComponent<Transform>()
            .GetChild(2)
            .gameObject;
        _passBorder = GameObject.Find("Pass_Border").GetComponent<Transform>().gameObject;
        background = GameObject.Find("Artery").GetComponent<Background>();
        background_b1 = GameObject.Find("Artery_B1").GetComponent<Background_B1>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manger is Null");
        }

        if (spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        background = GameObject.Find("Artery").GetComponent<Background>();

        if (background == null)
        {
            Debug.LogError("The Background is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            if (_laserShotOnOFF == true || bulletTripleOnOFF == false && bulletSuperOnOFF == false)
            {
                FireLaser();
            }
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            if (tripleshotActive == true && bulletTripleOnOFF == true)
            {
                FireTripleShot();
            }
            else if (bulletSuperActive == true && bulletSuperOnOFF == true)
            {
                FireSuperShot();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (bulletSuperActive == true)
            {
                bulletSuperOnOFF = false;
            }

            bulletTripleOnOFF = !bulletTripleOnOFF;

            if (tripleshotActive == true)
            {
                //bulletSuperSpeedandColor();


                if (bulletTripleOnOFF == false)
                {
                    _laserShotOnOFF = true;
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(4)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(1)
                        .gameObject.SetActive(false);
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(4)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(2)
                        .gameObject.SetActive(true);
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(4)
                        .gameObject.GetComponent<Transform>()
                        .GetComponent<Text>()
                        .color = uiNotActiveColor;
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(4)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(3)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(0)
                        .gameObject.GetComponent<Transform>()
                        .gameObject.GetComponent<Text>()
                        .color = uiGreenColor;
                }
                else
                {
                    bulletNormalSpeedandColor();
                    _laserShotOnOFF = false;
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(4)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(1)
                        .gameObject.SetActive(true);
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(4)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(2)
                        .gameObject.SetActive(false);
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(4)
                        .gameObject.GetComponent<Transform>()
                        .GetComponent<Text>()
                        .color = uiGreenColor;
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(4)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(3)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(0)
                        .gameObject.GetComponent<Transform>()
                        .gameObject.GetComponent<Text>()
                        .color = uiNotActiveColor;
                }
            }
            else
            {
                bulletTripleOnOFF = true;
                _laserShotOnOFF = true;
                bulletNormalSpeedandColor();
                _uiManager
                    .GetComponent<Transform>()
                    .GetChild(4)
                    .gameObject.GetComponent<Transform>()
                    .GetChild(1)
                    .gameObject.SetActive(true);
                _uiManager
                    .GetComponent<Transform>()
                    .GetChild(4)
                    .gameObject.GetComponent<Transform>()
                    .GetChild(2)
                    .gameObject.SetActive(false);
                _uiManager
                    .GetComponent<Transform>()
                    .GetChild(4)
                    .gameObject.GetComponent<Transform>()
                    .GetComponent<Text>()
                    .color = uiGreenColor;
                _uiManager
                    .GetComponent<Transform>()
                    .GetChild(4)
                    .gameObject.GetComponent<Transform>()
                    .GetChild(3)
                    .gameObject.GetComponent<Transform>()
                    .GetChild(0)
                    .gameObject.GetComponent<Transform>()
                    .gameObject.GetComponent<Text>()
                    .color = uiNotActiveColor;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (tripleshotActive == true)
            {
                bulletTripleOnOFF = false;

                // bulletSuperSpeedandColor();
            }

            bulletSuperOnOFF = !bulletSuperOnOFF;
            if (bulletSuperActive == true)
            {
                if (bulletSuperOnOFF == false)
                {
                    bulletNormalSpeedandColor();
                    _laserShotOnOFF = true;
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(5)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(1)
                        .gameObject.SetActive(false);
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(5)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(2)
                        .gameObject.SetActive(true);
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(5)
                        .gameObject.GetComponent<Transform>()
                        .GetComponent<Text>()
                        .color = uiNotActiveColor;
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(5)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(3)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(0)
                        .gameObject.GetComponent<Transform>()
                        .gameObject.GetComponent<Text>()
                        .color = uibulletSuperColor;
                }
                else
                {
                    bulletSuperSpeedandColor();
                    _laserShotOnOFF = false;
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(5)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(1)
                        .gameObject.SetActive(true);
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(5)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(2)
                        .gameObject.SetActive(false);
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(5)
                        .gameObject.GetComponent<Transform>()
                        .GetComponent<Text>()
                        .color = uibulletSuperColor;
                    _uiManager
                        .GetComponent<Transform>()
                        .GetChild(5)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(3)
                        .gameObject.GetComponent<Transform>()
                        .GetChild(0)
                        .gameObject.GetComponent<Transform>()
                        .gameObject.GetComponent<Text>()
                        .color = uiNotActiveColor;
                }
            }
            else
            {
                bulletNormalSpeedandColor();
                _laserShotOnOFF = true;
                bulletSuperOnOFF = true;
                _uiManager
                    .GetComponent<Transform>()
                    .GetChild(5)
                    .gameObject.GetComponent<Transform>()
                    .GetChild(1)
                    .gameObject.SetActive(true);
                _uiManager
                    .GetComponent<Transform>()
                    .GetChild(5)
                    .gameObject.GetComponent<Transform>()
                    .GetChild(2)
                    .gameObject.SetActive(false);
                _uiManager
                    .GetComponent<Transform>()
                    .GetChild(5)
                    .gameObject.GetComponent<Transform>()
                    .GetComponent<Text>()
                    .color = uibulletSuperColor;
                _uiManager
                    .GetComponent<Transform>()
                    .GetChild(5)
                    .gameObject.GetComponent<Transform>()
                    .GetChild(3)
                    .gameObject.GetComponent<Transform>()
                    .GetChild(0)
                    .gameObject.GetComponent<Transform>()
                    .gameObject.GetComponent<Text>()
                    .color = uiNotActiveColor;
            }
        }
    }

    void CalculateMovement()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");

        float VerticalInput = Input.GetAxis("Vertical");

        Vector3 WASD = new Vector3(HorizontalInput, VerticalInput, 0);

        transform.Translate(WASD * _speed * Time.deltaTime, Space.World);
        transform.Rotate(WASD * _Rotspeed * Time.deltaTime, Space.World);

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -4, 0)
        );

        if (transform.position.x >= 10.43f)
        {
            transform.position = new Vector3(-10.43f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -10.43f)
        {
            transform.position = new Vector3(10.43f, transform.position.y, transform.position.z);
        }
    }

    void FireTripleShot()
    {
        _laserShotOnOFF = false;
        _canFire = Time.time + _fireRate;

        if (
            Input.GetKey(KeyCode.Space) == true
            && Input.GetKey(KeyCode.LeftShift) == false
            && Input.GetKey(KeyCode.LeftControl) == false
            && Input.GetKey(KeyCode.Tab) == false
        )
        {
            Instantiate(
                _laserTriplePrefab,
                transform.position + new Vector3(0f, 1.4f, 0f),
                Quaternion.identity
            );
            if (bulletTripleCount > 0)
            {
                bulletTripleCount -= 1;
                _uiManager.bulletTripleTextUpdate(bulletTripleCount);
            }
        }
        else if (
            Input.GetKey(KeyCode.Space) == true
            && Input.GetKey(KeyCode.LeftShift) == true
            && Input.GetKey(KeyCode.LeftControl) == false
            && Input.GetKey(KeyCode.Tab) == false
        )
        {
            Instantiate(
                _laserTriplePrefab,
                transform.position + new Vector3(-1.4f, 0f, 0f),
                Quaternion.Euler(0f, 0f, 90f)
            );
            if (bulletTripleCount > 0)
            {
                bulletTripleCount -= 1;
                _uiManager.bulletTripleTextUpdate(bulletTripleCount);
            }
        }
        else if (
            Input.GetKey(KeyCode.Space) == true
            && Input.GetKey(KeyCode.LeftControl) == true
            && Input.GetKey(KeyCode.LeftShift) == false
            && Input.GetKey(KeyCode.Tab) == false
        )
        {
            Instantiate(
                _laserTriplePrefab,
                transform.position + new Vector3(1f, 0f, 0f),
                Quaternion.Euler(0f, 0f, -90f)
            );
            if (bulletTripleCount > 0)
            {
                bulletTripleCount -= 1;
                _uiManager.bulletTripleTextUpdate(bulletTripleCount);
            }
        }
        else if (
            Input.GetKey(KeyCode.Space) == true
            && Input.GetKey(KeyCode.Tab) == true
            && Input.GetKey(KeyCode.LeftShift) == false
            && Input.GetKey(KeyCode.LeftAlt) == false
        )
        {
            Instantiate(
                _laserTriplePrefab,
                transform.position + new Vector3(0f, -1f, 0f),
                Quaternion.Euler(-180f, 0f, 0f)
            );
            if (bulletTripleCount > 0)
            {
                bulletTripleCount -= 1;
                _uiManager.bulletTripleTextUpdate(bulletTripleCount);
            }
        }

        if (bulletTripleCount <= 0)
        {
            tripleshotActive = false;
            bulletTripleOnOFF = false;
            _laserShotOnOFF = true;
            _uiManager.bulletTripleUIOnOFF(false);
        }
    }

    void FireSuperShot()
    {
        _laserShotOnOFF = false;
        _canFire = Time.time + _fireRate;

        if (
            Input.GetKey(KeyCode.LeftShift) == false
            && Input.GetKey(KeyCode.LeftControl) == false
            && Input.GetKey(KeyCode.Tab) == false
        )
        {
            Instantiate(
                _bulletOval,
                transform.position + new Vector3(0, 0.62f, 0),
                Quaternion.identity
            );

            if (bulletSuperCount > 0)
            {
                bulletSuperCount -= 1;
                _uiManager.bulletSuperTextUpdate(bulletSuperCount);
            }
        }
        else if (
            Input.GetKey(KeyCode.LeftShift) == true
            && Input.GetKey(KeyCode.LeftControl) == false
            && Input.GetKey(KeyCode.Tab) == false
        )
        {
            Instantiate(
                _bulletOval,
                transform.position + new Vector3(-0.62f, 0f, 0),
                Quaternion.Euler(0f, 0f, 90)
            );
            if (bulletSuperCount > 0)
            {
                bulletSuperCount -= 1;
                _uiManager.bulletSuperTextUpdate(bulletSuperCount);
            }
        }
        else if (
            Input.GetKey(KeyCode.LeftControl) == true
            && Input.GetKey(KeyCode.LeftShift) == false
            && Input.GetKey(KeyCode.Tab) == false
        )
        {
            Instantiate(
                _bulletOval,
                transform.position + new Vector3(0.62f, 0f, 0),
                Quaternion.Euler(0f, 0f, -90f)
            );
            if (bulletSuperCount > 0)
            {
                bulletSuperCount -= 1;
                _uiManager.bulletSuperTextUpdate(bulletSuperCount);
            }
        }
        else if (
            Input.GetKey(KeyCode.Tab) == true
            && Input.GetKey(KeyCode.LeftShift) == false
            && Input.GetKey(KeyCode.LeftAlt) == false
        )
        {
            Instantiate(
                _bulletOval,
                transform.position + new Vector3(0f, -0.62f, 0),
                Quaternion.Euler(-180f, 0f, 0f)
            );
            if (bulletSuperCount > 0)
            {
                bulletSuperCount -= 1;
                _uiManager.bulletSuperTextUpdate(bulletSuperCount);
            }
        }

        if (bulletSuperCount <= 0)
        {
            bulletSuperActive = false;
            bulletSuperOnOFF = false;
            bulletNormalSpeedandColor();
            _uiManager.bulletSuperUIOnOFF(false);
            _laserShotOnOFF = true;
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (
            Input.GetKey(KeyCode.LeftShift) == false
            && Input.GetKey(KeyCode.LeftControl) == false
            && Input.GetKey(KeyCode.Tab) == false
        )
        {
            Instantiate(
                _bulletSinglePrefab,
                transform.position + new Vector3(0, 0.62f, 0),
                Quaternion.identity
            );
        }
        else if (
            Input.GetKey(KeyCode.LeftShift) == true
            && Input.GetKey(KeyCode.LeftControl) == false
            && Input.GetKey(KeyCode.Tab) == false
        )
        //else if ( Input.GetKey(KeyCode.Keypad4) == true && Input.GetKeyDown(KeyCode.Space) == true)
        {
            Instantiate(
                _bulletSinglePrefab,
                transform.position + new Vector3(-0.62f, 0f, 0),
                Quaternion.Euler(0f, 0f, 90)
            );
        }
        else if (
            Input.GetKey(KeyCode.LeftControl) == true
            && Input.GetKey(KeyCode.LeftShift) == false
            && Input.GetKey(KeyCode.Tab) == false
        )
        {
            Instantiate(
                _bulletSinglePrefab,
                transform.position + new Vector3(0.62f, 0f, 0),
                Quaternion.Euler(0f, 0f, -90)
            );
        }
        else if (
            Input.GetKey(KeyCode.Tab) == true
            && Input.GetKey(KeyCode.LeftShift) == false
            && Input.GetKey(KeyCode.LeftAlt) == false
        )
        {
            Instantiate(
                _bulletSinglePrefab,
                transform.position + new Vector3(0f, -0.62f, 0),
                Quaternion.Euler(-180f, 0f, 0)
            );
        }
    }

    public void playerStart()
    {
        StartCoroutine(playerStartRoutine());
    }

    IEnumerator playerStartRoutine()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        transform.position = new Vector3(0, 0, 0);
        GetComponent<Transform>().GetChild(1).gameObject.SetActive(true);
        playerAssembler.GetComponent<Transform>().gameObject.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        GetComponent<Transform>().GetChild(1).gameObject.SetActive(false);
        playerAssembler.GetComponent<Transform>().gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public void PowerAdd(int points)
    {
        playerScore = playerScore + points;
        setScore();
    }

    public void Damage()
    {
        if (shieldPlayerActive == true)
        {
            shieldPlayerActive = false;
            shieldPlayerVisualizer.SetActive(false);
            return;
        }

        playerLife = playerLife - 1;
        _uiManager.playerScoreTextUpdate(playerScore, playerLife);

        if (playerLife < 1)
        {
            _uiManager.playerScoreTextUpdate(playerScore, 0);
            playerDestroy();
        }

        ChangeColorOnDamage();
    }

    private void setScore()
    {
        if (playerScore >= addLifeAfterPoints)
        {
            addLifeAfterPoints = addLifeAfterPoints + 200;
            playerLife = playerLife + 1;
            _uiManager.playerlifeCenterScreenMessage(1);
            StartCoroutine(addLifeColorChangeRoutine());

            _uiManager.playerScoreTextUpdate(playerScore, playerLife);
        }
        else
        {
            _uiManager.playerScoreTextUpdate(playerScore, playerLife);
        }
    }

    IEnumerator addLifeColorChangeRoutine()
    {
        _uiLifeColorChange.SetActive(true);
        yield return new WaitForSeconds(1f);
        _uiLifeColorChange.SetActive(false);
    }

    public int getplayerLife()
    {
        return playerLife;
    }

    public void TripleShotActive()
    {
        tripleshotActive = true;
        bulletTripleCount += 50 + enemy003TripleShotBonus;
        _uiManager.bulletTripleAddTextUpdate(50 + enemy003TripleShotBonus);
        enemy003TripleShotBonus = 0;
        //bulletTripleOnOFF = true;
        _uiManager.bulletTripleUIOnOFF(true);
        _uiManager.bulletTripleTextUpdate(bulletTripleCount);
        //StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void bulletSuperActivate()
    {
        bulletSuperCount = bulletSuperCount + 5;
        _uiManager.bulletSuperAddTextUpdate(5);
        _uiManager.bulletSuperTextUpdate(bulletSuperCount);
        bulletSuperActive = true;
        _uiManager.bulletSuperUIOnOFF(true);
        //bulletSuperOnOFF = true;
    }

    void bulletSuperSpeedandColor()
    {
        _speed *= _speedMultiplier;
        _Rotspeed *= _RotspeedMultiplier;
        transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissionColorGreen);
    }

    void bulletNormalSpeedandColor()
    {
        transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissionColorWhite);
        _speed = 11.0f;
        _Rotspeed = 150f;
    }

    public void ShieldPlayerActivator()
    {
        this.gameObject.transform.tag = "Invisible";
        StartCoroutine(ShieldPlayerActiveRoutine());
    }

    IEnumerator ShieldPlayerActiveRoutine()
    {
        //shieldPlayerActive = true;
        shieldPlayerVisualizer.SetActive(true);

        yield return new WaitForSeconds(powerUptime);

        float flickreDuration = Time.time + 3.0f;
        while (Time.time < flickreDuration)
        {
            this.transform.GetChild(0).GetComponent<MeshRenderer>().gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            this.transform.GetChild(0).GetComponent<MeshRenderer>().gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }

        this.gameObject.transform.tag = "Player";
        shieldPlayerVisualizer.SetActive(false);
    }

    //********************************************************************************************************************************************************************************************
    //P L A Y E R  D E A T H
    public void playerDeath()
    {
        Instantiate(playerDeathParticals, transform.position, Quaternion.identity);
        //Destroy(gameObject);
    }

    public void playerDestroy()
    {
        StartCoroutine(playerDestroyRoutine());
    }

    IEnumerator playerDestroyRoutine()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;

        //GetComponent<Transform>().GetChild(2).gameObject.SetActive(true); // old pre def animation sequence
        yield return new WaitForSeconds(0.01f);
        //GetComponent<Transform>().GetChild(2).gameObject.SetActive(false); // old pre def animation sequence





        playerDeath();
        Destroy(this.gameObject);

        _uiManager.AliveCheck(0);
        background.StopBGmovement();
        background_b1.StopBGmovement();
        spawnManager.OnPlayerDeath();
    }

    public void ChangeColorOnDamage()
    {
        StartCoroutine(ColorChangeOnDamageRoutine());
    }

    IEnumerator ColorChangeOnDamageRoutine()
    {
        if (bulletSuperActive == false)
        {
            // transform.GetComponent< Renderer >().material.DisableKeyword("_EMISSION");
            transform
                .GetComponent<Renderer>()
                .material.SetColor("_EmissionColor", emissionColorMagenta);
            _uiManager
                .GetComponent<Transform>()
                .GetChild(1)
                .gameObject.GetComponent<Transform>()
                .GetChild(1)
                .gameObject.SetActive(true);
            _uiManager.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Text>().color =
                uiDamageColor;
            yield return new WaitForSeconds(0.2f);
            _uiManager
                .GetComponent<Transform>()
                .GetChild(1)
                .gameObject.GetComponent<Transform>()
                .GetChild(1)
                .gameObject.SetActive(false);
            _uiManager.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Text>().color =
                uiGreenColor;

            // transform.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            transform
                .GetComponent<Renderer>()
                .material.SetColor("_EmissionColor", emissionColorWhite);
        }
        else if (bulletSuperActive == true && bulletSuperOnOFF == true)
        {
            // transform.GetComponent< Renderer >().material.DisableKeyword("_EMISSION");
            transform
                .GetComponent<Renderer>()
                .material.SetColor("_EmissionColor", emissionColorMagenta);
            _uiManager
                .GetComponent<Transform>()
                .GetChild(1)
                .gameObject.GetComponent<Transform>()
                .GetChild(1)
                .gameObject.SetActive(true);
            _uiManager.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Text>().color =
                uiDamageColor;
            yield return new WaitForSeconds(0.2f);
            _uiManager
                .GetComponent<Transform>()
                .GetChild(1)
                .gameObject.GetComponent<Transform>()
                .GetChild(1)
                .gameObject.SetActive(false);
            _uiManager.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Text>().color =
                uiGreenColor;
            // transform.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            transform
                .GetComponent<Renderer>()
                .material.SetColor("_EmissionColor", emissionColorGreen);
        }
        else if (bulletSuperActive == true && bulletSuperOnOFF == false)
        {
            // transform.GetComponent< Renderer >().material.DisableKeyword("_EMISSION");
            transform
                .GetComponent<Renderer>()
                .material.SetColor("_EmissionColor", emissionColorMagenta);
            _uiManager
                .GetComponent<Transform>()
                .GetChild(1)
                .gameObject.GetComponent<Transform>()
                .GetChild(1)
                .gameObject.SetActive(true);
            _uiManager.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Text>().color =
                uiDamageColor;
            yield return new WaitForSeconds(0.2f);
            _uiManager
                .GetComponent<Transform>()
                .GetChild(1)
                .gameObject.GetComponent<Transform>()
                .GetChild(1)
                .gameObject.SetActive(false);
            _uiManager.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Text>().color =
                uiGreenColor;

            // transform.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            transform
                .GetComponent<Renderer>()
                .material.SetColor("_EmissionColor", emissionColorWhite);
        }
    }

    //*************************************************************************************************************************************************************************************
}
