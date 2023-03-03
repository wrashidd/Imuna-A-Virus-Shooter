using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/*
This class is responsible for managing User Interface activity
*/
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text playerScore_text;

    [SerializeField]
    private Text playerLife_text;

    [SerializeField]
    private Text playerlifeCenterScreen_text;
    private int playerlifeCenterScreen = 0;

    [SerializeField]
    private Text passed_text;
    private int passedCount = 0;
    private GameObject _passBorder;
    private bool playerAlive = true;

    [SerializeField]
    private Text bulletTriple_text;
    public int bulletTripleCount = 0;

    [SerializeField]
    private Text bulletTripleAdd_text;
    private int bulletTripleAdd = 0;

    [SerializeField]
    private Text bulletSuper_text;
    public int bulletSuperCount = 0;

    [SerializeField]
    private Text bulletSuperAdd_text;
    private int bulletSuperAdd = 0;

    private GameObject _canvas;

    private GameObject _canvasPlayerLifetext;

    [SerializeField]
    GameObject GameOverText;

    [SerializeField]
    private Text Restert_text;

    private GameManager gameManager;
    private Player _player;

    Color uiGreenColor = new Color(0.741f, 0.878f, 0.863f);
    Color uiDamageColor = new Color(0.576f, 0.314f, 0.941f);
    Color uibulletSuperColor = new Color(0.561f, 0.831f, 0f);

    // Start is called before the first frame update
    // Accesses Game Manager class and bottom border sensor game object
    void Start()
    {
        _canvasPlayerLifetext = GameObject
            .Find("Canvas")
            .GetComponent<Transform>()
            .GetChild(1)
            .GetComponent<Transform>()
            .GetChild(3)
            .GetComponent<Transform>()
            .gameObject;
        _canvas = GameObject.Find("Canvas").GetComponent<Transform>().gameObject;
        _canvas
            .GetComponent<Transform>()
            .GetChild(4)
            .gameObject.GetComponent<Transform>()
            .GetChild(0)
            .gameObject.GetComponent<Transform>()
            .GetComponent<Text>()
            .gameObject.SetActive(false);
        _canvasPlayerLifetext.SetActive(false);
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _passBorder = GameObject.Find("Pass_Border").GetComponent<Transform>().gameObject;

        if (gameManager == null)
        {
            Debug.LogError("Game_Manager is NULL");
        }

        if (_canvas == null)
        {
            Debug.LogError("Canvas is NULL");
        }

        _canvas
            .GetComponent<Transform>()
            .gameObject.GetComponent<Transform>()
            .GetChild(4)
            .gameObject.SetActive(false);
        _canvas
            .GetComponent<Transform>()
            .gameObject.GetComponent<Transform>()
            .GetChild(5)
            .gameObject.SetActive(false);

        _player = GameObject.Find("Player").GetComponent<Player>();
        GameOverText.gameObject.SetActive(false);
        playerScore_text.text = "" + 0;
        playerLife_text.text = "x " + 10;
        playerlifeCenterScreen_text.text = "- " + 0;
        passed_text.text = "10 x " + 0;
        bulletTriple_text.text = "x " + 0;
        bulletSuper_text.text = "x " + 0;
    }

    // Updates Player's score and life count in the UI
    public void playerScoreTextUpdate(int playerScore, int playerLife)
    {
        playerScore_text.text = "" + playerScore;

        playerLife_text.text = "x  " + playerLife;
    }

    // Display's notification of how many lives left in the UI
    public void playerlifeCenterScreenMessage(int SubtractionOrAddition)
    {
        playerlifeCenterScreen = playerlifeCenterScreen + SubtractionOrAddition;
        StartCoroutine(playerlifeCenterScreenMessageRoutine());
        playerlifeCenterScreen = 0;
    }

    IEnumerator playerlifeCenterScreenMessageRoutine()
    {
        if (playerAlive == true)
        {
            if (playerlifeCenterScreen < 0)
            {
                _canvasPlayerLifetext.SetActive(true);
                _canvasPlayerLifetext
                    .GetComponent<Transform>()
                    .GetChild(0)
                    .gameObject.SetActive(true);
                playerlifeCenterScreen_text.color = uiDamageColor;
                playerlifeCenterScreen_text.text = "- " + playerlifeCenterScreen * -1;
                yield return new WaitForSeconds(0.8f);
                _canvasPlayerLifetext
                    .GetComponent<Transform>()
                    .GetChild(0)
                    .gameObject.SetActive(false);
                playerlifeCenterScreen = 0;
                _canvasPlayerLifetext.SetActive(false);
            }
            else
            {
                _canvasPlayerLifetext.SetActive(true);
                _canvasPlayerLifetext
                    .GetComponent<Transform>()
                    .GetChild(1)
                    .gameObject.SetActive(true);
                playerlifeCenterScreen_text.color = uiGreenColor;
                playerlifeCenterScreen_text.text = "+ " + playerlifeCenterScreen;
                yield return new WaitForSeconds(1f);
                _canvasPlayerLifetext
                    .GetComponent<Transform>()
                    .GetChild(1)
                    .gameObject.SetActive(false);
                playerlifeCenterScreen = 0;
                _canvasPlayerLifetext.SetActive(false);
            }
        }
    }

    // Update's passed enemy count in the UI
    public void passedCountTextUpdate(int passedOne)
    {
        passed_text.text = "10 x " + passedOne;
        StartCoroutine(passedfontSizeChangeRoutine());
    }

    // Changes the font in the UI
    IEnumerator passedfontSizeChangeRoutine()
    {
        _canvas.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Text>().fontSize = 18;
        yield return new WaitForSeconds(0.25f);

        _canvas.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Text>().fontSize = 14;
    }

    // Updates Triple bullet count text in the UI
    public void bulletTripleTextUpdate(int bulletTriple)
    {
        bulletTriple_text.text = "x " + bulletTriple;
    }

    // Updates Triple bullet count text when bullets added in the UI
    public void bulletTripleAddTextUpdate(int bulletTripleAdd)
    {
        _canvas
            .GetComponent<Transform>()
            .GetChild(4)
            .gameObject.GetComponent<Transform>()
            .GetChild(0)
            .gameObject.SetActive(true);
        this.bulletTripleAdd = this.bulletTripleAdd + bulletTripleAdd;
        bulletTripleAdd_text.text = "+" + this.bulletTripleAdd;
        StartCoroutine(bulletTripleAddTextUpdateRoutine());
        this.bulletTripleAdd = 0;
    }

    IEnumerator bulletTripleAddTextUpdateRoutine()
    {
        yield return new WaitForSeconds(2);
        _canvas
            .GetComponent<Transform>()
            .GetChild(4)
            .gameObject.GetComponent<Transform>()
            .GetChild(0)
            .gameObject.SetActive(false);
    }

    // Enables and disables Triple Bullet icons in the UI
    public void bulletTripleUIOnOFF(bool enableDisable)
    {
        _canvas
            .GetComponent<Transform>()
            .gameObject.GetComponent<Transform>()
            .GetChild(4)
            .gameObject.SetActive(enableDisable);
    }

    public void bulletSuperTextUpdate(int bulletSuper)
    {
        bulletSuper_text.text = "x " + bulletSuper;
    }

    public void bulletSuperAddTextUpdate(int bulletSuperAdd)
    {
        _canvas
            .GetComponent<Transform>()
            .GetChild(5)
            .gameObject.GetComponent<Transform>()
            .GetChild(0)
            .gameObject.SetActive(true);
        this.bulletSuperAdd = this.bulletSuperAdd + bulletSuperAdd;
        bulletSuperAdd_text.text = "+" + this.bulletSuperAdd;
        StartCoroutine(bulletSuperAddTextUpdateRoutine());
        this.bulletSuperAdd = 0;
    }

    IEnumerator bulletSuperAddTextUpdateRoutine()
    {
        yield return new WaitForSeconds(2);
        _canvas
            .GetComponent<Transform>()
            .GetChild(5)
            .gameObject.GetComponent<Transform>()
            .GetChild(0)
            .gameObject.SetActive(false);
    }

    // Enables and disables Super Bullet icons in the UI
    public void bulletSuperUIOnOFF(bool enableDisable)
    {
        _canvas
            .GetComponent<Transform>()
            .gameObject.GetComponent<Transform>()
            .GetChild(5)
            .gameObject.SetActive(enableDisable);
    }

    // Tracks Player's lives
    // Launches Game Over on 0 lives
    public void AliveCheck(int alive)
    {
        if (alive == 0)
        {
            playerAlive = false;
            GameOverSequence();
        }
    }

    // Handles bottom border sensor that counts enemy passage
    public void handleborderPass()
    {
        StartCoroutine(passBorderDamageColorChangeRoutine());

        if (passedCount >= 9)
        {
            _player.Damage();
            playerlifeCenterScreenMessage(-1);
            passedCount = 0;
            passedCountTextUpdate(0);
        }
        else
        {
            passedCount = passedCount + 1;
            passedCountTextUpdate(passedCount);
        }
    }

    IEnumerator passBorderDamageColorChangeRoutine()
    {
        if (playerAlive == true)
        {
            _passBorder.GetComponent<Renderer>().material.color = uiDamageColor;
            _passBorder.GetComponent<Renderer>().material.SetColor("_EmissionColor", uiDamageColor);
            yield return new WaitForSeconds(0.1f);
            _passBorder.GetComponent<Renderer>().material.color = uiGreenColor;
            _passBorder.GetComponent<Renderer>().material.SetColor("_EmissionColor", uiGreenColor);
        }
        else
        {
            _passBorder.GetComponent<Renderer>().material.color = uiDamageColor;
            _passBorder.GetComponent<Renderer>().material.SetColor("_EmissionColor", uiDamageColor);
        }
    }

    void GameOverSequence()
    {
        gameManager.GameOver();
        GameOverText.gameObject.SetActive(true);
        Restert_text.gameObject.SetActive(true);

        StartCoroutine(GameOVerFlickreRoutine());
    }

    // Flicker's the screen on game over
    IEnumerator GameOVerFlickreRoutine()
    {
        while (true)
        {
            GameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            GameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
