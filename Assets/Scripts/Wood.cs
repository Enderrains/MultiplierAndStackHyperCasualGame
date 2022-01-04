using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField] List<GameObject> Woods = new List<GameObject>();
    [SerializeField] List<GameObject> bridgeWoods = new List<GameObject>();
    int currentWoods = 1;
    GameManager gameManager;
    [SerializeField] TextMeshPro woodScore;
    [SerializeField] GameObject goldParticle;
    [SerializeField] int GoldMultiplier = 5;


    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        woodScore.text = currentWoods.ToString();

        if (currentWoods <= 0)
        {
            gameManager.isLevelComplate = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Multiplier multiplier = other.GetComponent<Multiplier>();

        EnemyManager enemyManager = other.GetComponent<EnemyManager>();

        if (other.CompareTag("multiplier") && multiplier.isAvailable)
        {
            switch (multiplier.multiplierOperation)
            {
                case '+':
                    for (int i = currentWoods; i < currentWoods + multiplier.multiplierValue; i++)
                    {
                        if (i < Woods.Count)
                        {
                            Woods[i].SetActive(true);
                        }
                    }
                    currentWoods += multiplier.multiplierValue;
                    break;
                case '-':

                    for (int i = currentWoods; i > currentWoods - multiplier.multiplierValue; i--)
                    {
                        if (i <= Woods.Count && i > 0)
                        {
                            Woods[i].SetActive(false);
                        }
                    }
                    currentWoods -= multiplier.multiplierValue;
                    break;
                case '*':
                    for (int i = currentWoods; i < currentWoods * multiplier.multiplierValue; i++)
                    {
                        if (i < Woods.Count)
                        {
                            Woods[i].SetActive(true);
                        }
                    }
                    currentWoods *= multiplier.multiplierValue;
                    break;
                case '/':
                    for (int i = currentWoods; i > currentWoods / multiplier.multiplierValue; i--)
                    {
                        if (i <= Woods.Count && i > 0)
                        {
                            Woods[i].SetActive(false);
                        }
                    }
                    currentWoods /= multiplier.multiplierValue;
                    break;
                default:
                    break;
            }

            multiplier.isAvailable = false;
        }

        //else if (other.CompareTag("enemy") && enemyManager.isAvaible && currentWoods > 0)
        //{
        //    Instantiate(goldParticle, new Vector3(other.transform.position.x, other.transform.position.y,
        //    other.transform.position.z), Quaternion.identity);

        //    gameManager.coinAudio.Play();

        //    currentWoods -= enemyManager.health;
        //    gameManager.Gold += (int)(enemyManager.gold * gameManager.�ncomeMultiplier);
        //    gameManager.endLevelGold += (int)(enemyManager.gold * gameManager.�ncomeMultiplier);
        //    Destroy(other.gameObject);
        //}
        //else if (other.CompareTag("lastEnemy") && enemyManager.isAvaible && currentWoods > 0)
        //{
        //    Instantiate(goldParticle, new Vector3(other.transform.position.x, other.transform.position.y,
        //    other.transform.position.z), Quaternion.identity);

        //    gameManager.coinAudio.Play();

        //    currentWoods -= enemyManager.health;
        //    gameManager.Gold += (int)(enemyManager.gold * gameManager.�ncomeMultiplier);
        //    gameManager.endLevelGold += (int)(enemyManager.gold * gameManager.�ncomeMultiplier);
        //    Destroy(other.gameObject);
        //    Invoke("WaitLevelCompleted", 0.4f);
        //}
    }
    
    public void BuildBridge()
    {
        Debug.Log(currentWoods);
        int activeBridgeWood = 0;
        for (int i = 0; i < currentWoods; i++)
        {
            
            if (i<bridgeWoods.Count-1)
            {
                bridgeWoods[i].SetActive(true);
                activeBridgeWood++;
            }              
        }
        bridgeWoods[activeBridgeWood-1].tag = "lastWood";
    }

    public void UpgradeWood()
    {
        for (int i = 0; i < gameManager.woodLevel; i++)
        {
            if (i < Woods.Count)
            {
                Woods[i].SetActive(true);
                currentWoods = gameManager.woodLevel;
            }
        }
    }

    public void GoldCalculator() { 
    
        gameManager.endLevelGold = (int)(currentWoods*GoldMultiplier*gameManager.�ncomeMultiplier);
        gameManager.Gold += gameManager.endLevelGold;
    }
    void WaitLevelCompleted()
    {
        gameManager.isLevelComplate = true;
    }
}
