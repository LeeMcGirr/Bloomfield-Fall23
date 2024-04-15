using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIplayer : MonoBehaviour
{
    public List<string> myInventory;
    public int currentRunScore;
    public int[] highScores = new int[5];

    [Header("SceneChange Vars")]
    public GameObject sceneChanger;
    Button changerButton;
    TextMeshProUGUI changeButtonText;

    
    [Header("name buttons")]
    public TMP_InputField myInput;
    public string playerName;
    public GameObject inputField;
    public GameObject submitButton;
    public GameObject WelcomeObject;
    TextMeshProUGUI WelcomeText; 

    public string welcomeMessage;
    public string replaceText;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        WelcomeText = WelcomeObject.GetComponent<TextMeshProUGUI>();
        DontDestroyOnLoad(this.gameObject);
        UIplayer[] players = FindObjectsOfType<UIplayer>();
        Debug.Log("there are this many players: " + players.Length);
        if(players.Length > 1) { Destroy(this.gameObject); }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName()
    {
        playerName = myInput.text;
        submitButton.SetActive(false);
        inputField.SetActive(false);

        string newWelcome = welcomeMessage.Replace
                            (replaceText, playerName);
        WelcomeText.text = newWelcome;

        sceneChanger.SetActive(true);

    }
    public void inventoryAdd(string item)
    {
        myInventory.Add(item);
    }
}
