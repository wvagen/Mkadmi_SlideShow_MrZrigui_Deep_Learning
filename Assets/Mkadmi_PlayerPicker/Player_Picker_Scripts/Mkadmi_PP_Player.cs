using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mkadmi_PP_Player : MonoBehaviour
{
    public Text playerName, playerScore;
    public Toggle isPresentToggle;
    public bool isPresent = true;

    Mkadmi_PP_Manager manager;

    int score = 0;

    public void Set_Me(Mkadmi_PP_Manager manager, string playerData)
    {
        this.manager = manager;

        string[] data = playerData.Split('_');

        this.playerName.text = data[0];

        score = int.Parse(data[1]);
        playerScore.text = score.ToString();

        isPresent = int.Parse(data[2]) == 1;
        isPresentToggle.isOn = isPresent;
    }



    public void Remove_Me()
    {
        manager.Remove_Player(this);
    }

    public void Increment_Score()
    {
        score += 10;
        manager.Update_Player(this);
        playerScore.text = score.ToString();
    }

    public void Decrement_Score()
    {
        score -= 10;
        manager.Update_Player(this);
        playerScore.text = score.ToString();
    }

    public void Is_Present_Trigger()
    {
        isPresent = !isPresent;
        manager.Update_Player(this);
    }

    public string Parse_Data()
    {
        return (playerName.text + "_" + score.ToString() + "_" + (isPresent ? "1" : "0"));
    } 
}
