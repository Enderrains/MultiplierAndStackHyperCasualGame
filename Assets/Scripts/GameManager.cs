using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isLevelComplate = false;

    [HideInInspector] public AudioSource coinAudio;

    public int Gold;
    [HideInInspector] public int endLevelGold;
    [SerializeField] TextMeshProUGUI coinText;

    [SerializeField] TextMeshProUGUI upgradeWoodNeedCoinText;
    [SerializeField] GameObject notEnoughGoldText;
    [SerializeField] TextMeshProUGUI woodLevelText;
    public int woodLevel = 1;
    public int woodUpgradeNeedGold = 150;


    [SerializeField] TextMeshProUGUI upgradeIncomeNeedCoinText;
    [SerializeField] TextMeshProUGUI ıncomeLevelText;
    public int ıncomeLevel = 1;
    public float ıncomeMultiplier = 1;
    public int ıncomeUpgradeNeedGold = 150;



    [SerializeField] GameObject shopMenuPanel;
    [SerializeField] GameObject tapTopPlayPanel;


    [SerializeField] GameObject levelCompletedPanel;
    [SerializeField] TextMeshProUGUI endLevelCoinText;


    [SerializeField] GameObject wood;
    Wood woodScript;

    public int LevelIndex;



    [HideInInspector] public bool isPlay = false;

    private void Awake()
    {
        LoadSystem();
        woodScript = wood.GetComponent<Wood>();
    }

    private void Start()
    {
        LoadText();
        coinAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        coinText.text = Gold.ToString();

        if (isLevelComplate)
        {
            LevelCompletedPanel();
        }
    }


    public void WoodUpgradeButton()
    {
        if (Gold >= woodUpgradeNeedGold)
        {
            coinAudio.Play();
            woodLevel++;
            Gold -= woodUpgradeNeedGold;
            woodUpgradeNeedGold += 100;
            woodScript.UpgradeWood();
            LoadText();
            SaveSystem();
        }
        else
        {
            StartCoroutine(notEnoughGold());
        }
    }

    public void IncomeUpgradeButton()
    {
        if (Gold >= ıncomeUpgradeNeedGold)
        {
            coinAudio.Play();
            ıncomeLevel++;
            Gold -= ıncomeUpgradeNeedGold;
            ıncomeUpgradeNeedGold += 100;
            ıncomeMultiplier += 0.2f;
            LoadText();
            SaveSystem();

        }
        else
        {
            StartCoroutine(notEnoughGold());
        }

    }

    public void OpenShopMenu()
    {
        shopMenuPanel.SetActive(true);
    }

    public void ShopMenuHideButton()
    {
        shopMenuPanel.SetActive(false);
    }

    public void tapTopPlayButton()
    {
        isPlay = true;
        tapTopPlayPanel.SetActive(false);
    }

    public void LevelCompletedPanel()
    {
        wood.SetActive(false);
        levelCompletedPanel.SetActive(true);
        endLevelCoinText.text = endLevelGold.ToString();
        SaveSystem();
    }
    public void NexLevelButton()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int sceneIndex = SceneManager.sceneCountInBuildSettings - 1;

        if (nextSceneIndex <= sceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        if (nextSceneIndex > sceneIndex)
        {
            SceneManager.LoadScene(0);
        }
        if (nextSceneIndex >=3)
        {
            PlayerPrefs.SetInt("levelIndex", 0);
        }
        else
        {
            PlayerPrefs.SetInt("levelIndex", nextSceneIndex);
        }
    }
    public void ReStartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveSystem()
    {
        //gold
        PlayerPrefs.SetInt("Gold", Gold);
        //wood
        PlayerPrefs.SetInt("woodLevel", woodLevel);
        PlayerPrefs.SetInt("woodNeedGold", woodUpgradeNeedGold);

        //ıncome
        PlayerPrefs.SetInt("ıncomeLevel", ıncomeLevel);
        PlayerPrefs.SetInt("ıncomeNeedGold", ıncomeUpgradeNeedGold);
        PlayerPrefs.SetFloat("IncomeMultiplier", ıncomeMultiplier);
    }

    void LoadSystem()
    {
        //level
        int ındex = PlayerPrefs.GetInt("levelIndex");
        //gold
        Gold = PlayerPrefs.GetInt("Gold");
        //wood
        woodLevel = PlayerPrefs.GetInt("woodLevel");
        woodUpgradeNeedGold = PlayerPrefs.GetInt("woodNeedGold");

        //ıncome
        ıncomeLevel = PlayerPrefs.GetInt("ıncomeLevel");
        ıncomeUpgradeNeedGold = PlayerPrefs.GetInt("ıncomeNeedGold");
        ıncomeMultiplier = PlayerPrefs.GetFloat("IncomeMultiplier");

        if (SceneManager.GetActiveScene().buildIndex != ındex)
        {
            SceneManager.LoadScene(ındex);
        }
    }

    void LoadText()
    {
        ıncomeLevelText.text = "Level " + ıncomeLevel;
        upgradeIncomeNeedCoinText.text = ıncomeUpgradeNeedGold.ToString();

        woodLevelText.text = "Level " + woodLevel;
        upgradeWoodNeedCoinText.text = woodUpgradeNeedGold.ToString();

        woodScript.UpgradeWood();      
    }

    IEnumerator notEnoughGold()
    {
        notEnoughGoldText.SetActive(true);
        yield return new WaitForSeconds(2);
        notEnoughGoldText.SetActive(false);
    }


}
