using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject enemyPrefab002;
    [SerializeField]
    private GameObject enemyPrefab003;
    public bool enemy003Dead = false;
    private int enemy003Counter = 0;
   
    [SerializeField]
    private GameObject enemyPrefab004;

    [SerializeField]
    private GameObject enemyPrefab005;
   

    [SerializeField]
    private GameObject[] powerUps;
    

    [SerializeField]
    private GameObject enemyContainer;
    
    [SerializeField]
    private GameObject  Erythrocytes; 

    private bool stopSpawning = false;

    private int ErythrocyteNumber= 1;

   private Player _player;
    private UIManager _uiManager;

    private Background _backgroundArteryScript;
    private Background_B1 _backgroundArteryB1Script;
    private GameObject _backgroundArteryOnOFF;
    private GameObject _backgroundArteryB1OnOFF;

  

    private bool boss1sequence = false;

 

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _backgroundArteryScript = GameObject.Find("Artery").GetComponent<Background>();
        _backgroundArteryB1Script = GameObject.Find("Artery_B1").GetComponent<Background_B1>();

        _backgroundArteryOnOFF = GameObject.Find("Artery").GetComponent<Transform>().gameObject;
        _backgroundArteryB1OnOFF = GameObject.Find("Artery_B1").GetComponent<Transform>().gameObject;



        //Routines
        StartCoroutine(SpawnEnemyRoutine());  
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnEnemy003Routine());
        StartCoroutine(SpawnEnemy004Routine());
        StartCoroutine(SpawnEnemy005Routine());

    }

    // Update is called once per frame
    void Update()
    {
        onenemy003DeadSpawnTripleShot();
        boss1launcher();
    }



    // spawn game object every 5 sec
    IEnumerator SpawnEnemyRoutine()
    {
       while (stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 12, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            Vector3 posToSpawn2 = new Vector3(Random.Range(-7.0f, 7.0f), 14, 0);
            GameObject newEnemy002 = Instantiate(enemyPrefab002, posToSpawn2, Quaternion.identity);
            newEnemy002.transform.parent = enemyContainer.transform;
           
            
          
            
            if (ErythrocyteNumber < 11)
            {
                GameObject newErythrocyte = Instantiate(Erythrocytes, posToSpawn, Quaternion.identity);
                newErythrocyte.transform.parent = enemyContainer.transform;
                ErythrocyteNumber += ErythrocyteNumber;
            }
           

            yield return new WaitForSeconds(5.0f);
        }
    }


    IEnumerator SpawnEnemy003Routine()
    {
        while (stopSpawning == false && enemy003Counter < 3)
        {
            if (enemy003Counter < 3)
            {
                int timeWait = Random.Range(15, 25);    
                yield return new WaitForSeconds(timeWait); 
                Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 30, 0);
                GameObject newEnemy003 = Instantiate(enemyPrefab003, posToSpawn, Quaternion.identity);
                newEnemy003.transform.parent = enemyContainer.transform;
                enemy003Counter += 1;
                yield return new WaitForSeconds(timeWait);
            }

            else
            {
                
            }
            
        }
    }
    
    IEnumerator SpawnEnemy004Routine()
    {
        int timeWait = Random.Range(25, 50);
        yield return new WaitForSeconds(timeWait);

        while (stopSpawning == false)
        {
            Vector3 posToSpawn004 = new Vector3(0, 14.5f, 0.5f);
            GameObject newEnemy004 = Instantiate(enemyPrefab004, posToSpawn004, Quaternion.identity);
            newEnemy004.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(timeWait);
        }
          
    }


    IEnumerator SpawnEnemy005Routine()
    {
        float RandomX = Random.Range(-9.0f, 9.0f);
        float RandomY = Random.Range(13f, 25f);
        float RandomZ = Random.Range(0, 2.15f);

        int RandomSpawnAmount = Random.Range(75, 100);

        int timeWait = Random.Range(180, 240); // Spawn Time
        yield return new WaitForSeconds(timeWait);

        int enemy005Counter = 0;
        int enemy005SpawnAmount = RandomSpawnAmount;

        if (enemy005Counter < enemy005SpawnAmount)
        {
            stopSpawning = true;
            while (stopSpawning == true && enemy005Counter < enemy005SpawnAmount)
            {

                Vector3 posToSpawn005 = new Vector3(RandomX, RandomY, RandomZ);
                Vector3 posToSpawn2 = new Vector3(Random.Range(-9.0f, 9.0f), 20, 0);
                GameObject newEnemy005 = Instantiate(enemyPrefab005, posToSpawn2, Quaternion.identity);
                newEnemy005.transform.localScale = Vector3.one * Random.Range(0.3f, 1f);
                newEnemy005.transform.parent = enemyContainer.transform;
                int AddToEnemy005Counter = Random.Range(1, 3);
                enemy005Counter = enemy005Counter + AddToEnemy005Counter;
                float timeWaitToSpawn = Random.Range(0.25f, 0.5f);
                yield return new WaitForSeconds(timeWaitToSpawn);
            }

            StartCoroutine(WaitForBoos1SequenceLaunchRoutine());

           IEnumerator WaitForBoos1SequenceLaunchRoutine()
            {
                yield return new WaitForSeconds(7);
                boss1sequence = true;
            } 
        }
        else if (enemy005Counter >= enemy005SpawnAmount)
        {
            //stopSpawning = false;

        }


    }




    public IEnumerator SpawnPowerupRoutine()
    {
        while (stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 14, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerUp], posToSpawn, Quaternion.identity);
            float randomT = Random.Range(25.0f, 40.0f);
            yield return new WaitForSeconds(randomT);
        }
    }
        
    private void onenemy003DeadSpawnTripleShot()
    {
        if (enemy003Dead == true)
        {
            Vector3 spawnPos = new Vector3(transform.position.x +1.5f, transform.position.y + 4f, transform.position.z) ;
            Instantiate(powerUps[0], spawnPos,  Quaternion.identity);
            _player.enemy003TripleShotBonus = 100;
            
            
            enemy003Dead = false;
        }
    }    
    private void boss1launcher()
    {
        if (boss1sequence == true)
        {
            stopSpawning = true;
         
            boss1();
            boss1sequence = false;
        }
    }

    private void boss1()
    {
        if (boss1sequence == true)
        {

            //prefabClonesDestroy();
            Destroy(GameObject.FindGameObjectWithTag("Container"));
            //Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            StartCoroutine(boss1BackgroundChangeRoutine());

            IEnumerator boss1BackgroundChangeRoutine()
            {
                yield return new WaitForSeconds(3);
                _backgroundArteryScript._speedM = 6f;
                yield return new WaitForSeconds(5);
                _backgroundArteryOnOFF.SetActive(false);
                //_backgroundArteryB1OnOFF.SetActive(true);
                //Destroy(GameObject.FindGameObjectWithTag("Container"));
                _backgroundArteryB1Script._speedM = 6f;
                yield return new WaitForSeconds(3);
                _backgroundArteryB1Script._speedM = 3f;
                yield return new WaitForSeconds(4);
                _backgroundArteryB1Script._speedM = 2f;
                yield return new WaitForSeconds(4);
                _backgroundArteryB1Script._speedM = 1f;

            }
        }
    }

  
    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}
