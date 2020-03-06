using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD_Manager : MonoBehaviour
{
    [SerializeField] int PlayerMaxHealth = 100;
    private int playerHealth;
    [SerializeField] int PlayerMaxMana = 100;
    private int playerMana;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI manaText;
    

    #region properties
    public int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }
    public int PlayerMana
    {
        get { return playerMana; }
        set { playerMana = value; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = PlayerMaxHealth;
        PlayerMana = PlayerMaxMana;
        healthText = transform.GetComponentsInChildren<TextMeshProUGUI>(true).FirstOrDefault(t => t.name == "Health");
        //healthText = transform.GetComponentInChildren<TextMeshProUGUI>(true);
        Debug.Log(healthText);
        manaText = transform.GetComponentsInChildren<TextMeshProUGUI>(true).FirstOrDefault(t => t.name == "Mana");
        //manaText = transform.Find("Mana").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + playerHealth + "/" + PlayerMaxHealth;
        manaText.text = "Mana: " + playerMana + "/" + PlayerMaxMana;
    }
}
