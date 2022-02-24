using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mkadmi_PP_Constants
{
    public static string PLAYER_PICKER_SAVED_PLAYERS_KEY = "pp_saved_players";
    public static char PLAYER_PICKER_PLAYERS_DITINGUISHER_CHAR = '@';
    public static string PP_ANIM_DISPLAY_BOOL_NAME = "isShowingPP";

}

public class Mkadmi_PP_Manager : MonoBehaviour
{

    public static bool isPlayerPickerDisplayed = false;

    public Animator myAnim;

    public InputField newPlayerNameInput;

    public GameObject player;
    public Transform playersListContainer;

    public Image playerSelectedPanelImg;
    public Sprite nonePlayerSelectedSprite, selectedPlayerSprite;

    public GameObject playersListPanel, addPlayersPanel, playerPickerPanel;

    public Text randomChosenPlayertxt;

    List<Mkadmi_PP_Player> playersList = new List<Mkadmi_PP_Player>();

    GameObject selectedPanel;
    Coroutine randomSelectionCoroutine;

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetString(Mkadmi_PP_Constants.PLAYER_PICKER_SAVED_PLAYERS_KEY, string.Empty));

        Select_Panel(0);
        Select_Panel(2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            myAnim.SetBool(Mkadmi_PP_Constants.PP_ANIM_DISPLAY_BOOL_NAME, !myAnim.GetBool(Mkadmi_PP_Constants.PP_ANIM_DISPLAY_BOOL_NAME));
            isPlayerPickerDisplayed = !isPlayerPickerDisplayed;
        }
    }

    public void Add_Player()
    {
        string savedPlayers = PlayerPrefs.GetString(Mkadmi_PP_Constants.PLAYER_PICKER_SAVED_PLAYERS_KEY,string.Empty);
        savedPlayers += newPlayerNameInput.text + "_0_1" + Mkadmi_PP_Constants.PLAYER_PICKER_PLAYERS_DITINGUISHER_CHAR;

        PlayerPrefs.SetString(Mkadmi_PP_Constants.PLAYER_PICKER_SAVED_PLAYERS_KEY, savedPlayers);
        newPlayerNameInput.text = "";
    }

    void Show_Players_List()
    {
        Empty_Players_List();
        string[] playersArray = PlayerPrefs.GetString(Mkadmi_PP_Constants.PLAYER_PICKER_SAVED_PLAYERS_KEY, string.Empty).Split(Mkadmi_PP_Constants.PLAYER_PICKER_PLAYERS_DITINGUISHER_CHAR);
        for (int i = 0; i < playersArray.Length; i++)
        {
            if (!string.IsNullOrEmpty(playersArray[i]))
            {
                Mkadmi_PP_Player playerScript = Instantiate(player, playersListContainer).GetComponent<Mkadmi_PP_Player>();

                playerScript.Set_Me(this, playersArray[i]);
                playersList.Add(playerScript);
            }
        }
    }

    public void Remove_Player(Mkadmi_PP_Player playerScript)
    {
        if (playerScript != null)
        {
            string[] playersArray = PlayerPrefs.GetString(Mkadmi_PP_Constants.PLAYER_PICKER_SAVED_PLAYERS_KEY, string.Empty).Split(Mkadmi_PP_Constants.PLAYER_PICKER_PLAYERS_DITINGUISHER_CHAR);
            string newPlayersList = "";
            for (int i = 0; i < playersList.Count; i++)
            {
                if (playerScript != playersList[i])
                {
                    newPlayersList += playersArray[i] + Mkadmi_PP_Constants.PLAYER_PICKER_PLAYERS_DITINGUISHER_CHAR;
                }
            }
            PlayerPrefs.SetString(Mkadmi_PP_Constants.PLAYER_PICKER_SAVED_PLAYERS_KEY, newPlayersList);
            playersList.Remove(playerScript);
            Destroy(playerScript.gameObject);
        }
    }

    public void Update_Player(Mkadmi_PP_Player playerScript)
    {
        if (playerScript != null)
        {
            string[] playersArray = PlayerPrefs.GetString(Mkadmi_PP_Constants.PLAYER_PICKER_SAVED_PLAYERS_KEY, string.Empty).Split(Mkadmi_PP_Constants.PLAYER_PICKER_PLAYERS_DITINGUISHER_CHAR);
            string newPlayersList = "";
            for (int i = 0; i < playersList.Count; i++)
            {
                if (playerScript != playersList[i])
                {
                    newPlayersList += playersArray[i] + Mkadmi_PP_Constants.PLAYER_PICKER_PLAYERS_DITINGUISHER_CHAR;
                }
                else
                {
                    newPlayersList += playerScript.Parse_Data() + Mkadmi_PP_Constants.PLAYER_PICKER_PLAYERS_DITINGUISHER_CHAR;
                }
            }
            PlayerPrefs.SetString(Mkadmi_PP_Constants.PLAYER_PICKER_SAVED_PLAYERS_KEY, newPlayersList);
        }
    }

    public void Roll_Names()
    {
        playerSelectedPanelImg.sprite = nonePlayerSelectedSprite;
        if (randomSelectionCoroutine != null) StopCoroutine(randomSelectionCoroutine);

        randomSelectionCoroutine = StartCoroutine(Select_Random_Player());
    }

    IEnumerator Select_Random_Player()
    {
        int randomPlayer = 0;
        do
        {
            randomPlayer = Random.Range(0, playersList.Count);
        } while (!playersList[randomPlayer].isPresent);

        for (int i = 0; i < playersList.Count; i++)
        {
            randomChosenPlayertxt.text = playersList[Random.Range(0, playersList.Count)].playerName.text;
            yield return new WaitForSeconds(4/ playersList.Count);
        }

        randomChosenPlayertxt.text = playersList[randomPlayer].playerName.text;
        playerSelectedPanelImg.sprite = selectedPlayerSprite;

    }

    void Empty_Players_List()
    {
        for (int i = 0; i < playersListContainer.childCount; i++)
        {
            Destroy(playersListContainer.GetChild(i).gameObject);
        }
        playersList = new List<Mkadmi_PP_Player>();
    }

    public void Select_Panel(int panelIndex)
    {
        if (selectedPanel != null)
        {
            selectedPanel.SetActive(false);
        }

        //panelIndex = 0 : playersListPanel | 1 = addPlayersPanel | 2 = playerPickerPanel
        switch (panelIndex)
        {
            case 0: selectedPanel = playersListPanel; Show_Players_List(); break;
            case 1: selectedPanel = addPlayersPanel;break;
            case 2: selectedPanel = playerPickerPanel; break;
        }

        selectedPanel.SetActive(true);
    }

}
