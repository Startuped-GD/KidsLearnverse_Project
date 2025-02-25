using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreenManagment : MonoBehaviour
{
    public int LevelNumber = 0; // for testing 

    public Button lv1Btn;
    public GameObject Lv1Lock;
    public GameObject UnderProgressText;

    private List<Button> levelButtons = new();
    private List<Image> lockIcons = new(); 

    private SceneManagment sceneManager; 

    // Start is called before the first frame update
    void Start()
    {
        // Find all locsk and level buttons
        GetLocksAndButtons();

        // Find References 
        sceneManager = GameObject.Find("Scene Manager").GetComponent<SceneManagment>();

        string learningTopic = PlayerPrefs.GetString("Learning Topic");

        if(learningTopic == "Entrepreneurship")
        {
            LoadLevelSavedData();   
        }
        else
        {
            lv1Btn.interactable = false;
            Lv1Lock.SetActive(true);
            UnderProgressText.SetActive(true);
        }

    }
    private void GetLocksAndButtons()
    {
        // Find All Level Buttons 
        GameObject levelScreen = GameObject.Find("Level Buttons Handler");
        for(int i =1; i < levelScreen.transform.childCount -1 ; i++)
        {
            GameObject levelBTNObject = levelScreen.transform.GetChild(i).gameObject;
            
            Button levelButton = levelBTNObject.transform.GetChild(0).GetComponent<Button>();
            levelButtons.Add(levelButton);

            Image lockImage = levelBTNObject.transform.Find("Lock").GetComponent<Image>();
            lockIcons.Add(lockImage);   
        }
    }

    private void LoadLevelSavedData()
    {
        int unlockedLevel = PlayerPrefs.GetInt("Level", 0);
        Debug.Log(unlockedLevel);

        if (unlockedLevel > 1)
        {
            UnlockLevels(unlockedLevel);
        }
    }

    private void UnlockLevels(int levelNumber)
    {
        if (!levelButtons[levelNumber - 2].interactable && lockIcons[levelNumber - 2].enabled)
        {
            levelButtons[levelNumber - 2].interactable = true;
            lockIcons[levelNumber - 2].enabled = false;
        }
    }

    public void OpenLevel(int lv)
    {
        sceneManager.StoreUserPlayedLevel(lv); 

        // Load Level Scene
        sceneManager.LoadAnyScene(4); 
    }
}
