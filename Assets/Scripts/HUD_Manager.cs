using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD_Manager : MonoBehaviour
{
    public enum UIState
    {
        MainMenu,
        Instructions,
        InGameHUD,
        PauseMenu
    }
    UIState currentUIState;
    [SerializeField] UIState startingUIState;

    //adding them individually here instead of adding them to the list directly is not great but necessary because we want the enum and object name to match and you cannot create dynamic enums
    //this method minimizes the number of places where the code needs to change to accomodate adding or removing menus
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject Instructions;
    [SerializeField] GameObject InGameHUD;
    [SerializeField] GameObject PauseMenu;
    List<GameObject> menus = new List<GameObject>();

    [SerializeField] Player player;
    float playerMaxHealth; //need this to have health bars, get from initial health value

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI manaText;

    #region properties
    //public int PlayerHealth
    //{
    //    get { return playerHealth; }
    //    set { playerHealth = value; }
    //}
    //public int PlayerMana
    //{
    //    get { return playerMana; }
    //    set { playerMana = value; }
    //}
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        menus.Add(MainMenu);
        menus.Add(Instructions);
        menus.Add(InGameHUD);
        menus.Add(PauseMenu);

        ChangeUIState(startingUIState);
        playerMaxHealth = player.health;

        //playerHealth = player.health;
        //PlayerHealth = PlayerMaxHealth;
        //PlayerMana = PlayerMaxMana;
        //healthText = transform.GetComponentsInChildren<TextMeshProUGUI>(true).FirstOrDefault(t => t.name == "Health");
        ////healthText = transform.GetComponentInChildren<TextMeshProUGUI>(true);
        //Debug.Log(healthText);
        //manaText = transform.GetComponentsInChildren<TextMeshProUGUI>(true).FirstOrDefault(t => t.name == "Mana");
        ////manaText = transform.Find("Mana").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + player.health + "/" + playerMaxHealth;
        //manaText.text = "Mana: " + playerMana + "/" + PlayerMaxMana;
    }

    /// <summary>
    /// changes the current UI state based on the given input and changes the visibility of UI elements
    /// </summary>
    /// <param name="nextState"></param>
    void ChangeUIState(UIState nextState)
    {
        //No, Unity does not allow you to use an enum as an on click parameter in the inspector. Yes, people have been asking for this basic functionality for 6 years
        //currentUIState = (UIState)System.Enum.Parse(typeof(UIState), nextState);
        currentUIState = nextState;

        foreach (GameObject menu in menus)
        {
            if (menu.name != currentUIState.ToString() + "Panel")
            {
                menu.SetActive(false);
                Debug.Log("not state " + currentUIState.ToString() + ", its " + menu.name);
            }
            else
            {
                menu.SetActive(true);
                Debug.Log("THIS ONE");
            }
        } 
    }

    #region button onClick methods

    /// <summary>
    /// shows the MainMenu of the game
    /// </summary>
    public void ShowMainMenu()
    {
        ChangeUIState(UIState.MainMenu);
    }

    public void ShowInstructions()
    {
        ChangeUIState(UIState.Instructions);
    }

    /// <summary>
    /// sets current ui state to InGameHud
    /// </summary>
    public void ShowGameHUD()
    {
        ChangeUIState(UIState.InGameHUD);
    }

    public void ShowPause()
    {
        ChangeUIState(UIState.PauseMenu);
    }

    #endregion
    //switch (currentUIState)
    //{
    //    case UIState.MainMenu:
    //        foreach(GameObject menu in menus)
    //        {
    //            if (menu.name != )
    //        }
    //        break;
    //    case UIState.Instructions:
    //        break;
    //    case UIState.InGameHUD:
    //        break;
    //    case UIState.PauseMenu:
    //        break;
    //    default:
    //        break;
    //}

}
