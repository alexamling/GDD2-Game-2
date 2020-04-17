using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD_Manager : MonoBehaviour
{
    //[SerializeField] int PlayerMaxHealth = 100;
    //private int playerHealth;
    //[SerializeField] int PlayerMaxMana = 100;
    //private int playerMana;
    //[SerializeField] TextMeshProUGUI healthText;
    //[SerializeField] TextMeshProUGUI manaText;

    //adding them individually here instead of adding them to the list directly is not great but necessary because we want the enum and object name to match and you cannot create dynamic enums
    //this method minimizes the number of places where the code needs to change to accomodate adding or removing menus
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject Instructions;
    [SerializeField] GameObject InGameHUD;
    [SerializeField] GameObject PauseMenu;
    List<GameObject> menus = new List<GameObject>();

    public enum UIState
    {
        MainMenu,
        Instructions,
        InGameHUD,
        PauseMenu
    }
    UIState currentUIState;

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
    //    healthText.text = "Health: " + playerHealth + "/" + PlayerMaxHealth;
    //    manaText.text = "Mana: " + playerMana + "/" + PlayerMaxMana;
    }

    /// <summary>
    /// changes the current UI state based on the given input and changes the visibility of UI elements
    /// </summary>
    /// <param name="nextState"></param>
    public void ChangeUIState(UIState nextState)
    {
        currentUIState = nextState;

        foreach (GameObject menu in menus)
        {
            if (menu.name != currentUIState.ToString())
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
        } 
    }
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
