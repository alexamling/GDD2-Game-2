using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Most of this code comes from here: https://fractalpixels.com/devblog/unity-2D-progress-bars
/// </summary>

public class FillBar : MonoBehaviour
{
    [SerializeField] Slider sliderBar;
    [SerializeField] TextMeshProUGUI displayText;

    private float maxValue;
    private float currentValue;
    //float mappedCurrentValue;

    public float MaxValue
    {
        get { return maxValue; }
        set 
        { 
            maxValue = value;
            sliderBar.maxValue = maxValue;
        }
    }
    public float CurrentValue
    {
        get { return currentValue; }
        set 
        { 
            currentValue = value;
            //mappedCurrentValue = Map(currentValue, 0, maxValue, 0, 1);
            sliderBar.value = CurrentValue;
            displayText.text = currentValue.ToString() + "/" + maxValue.ToString();  //(sliderBar.value * 100).ToString("0.00") + "%";
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private float Map(float value, float minIn, float maxIn, float minOut, float maxOut)
    //{
    //    return (value - minIn) / (maxIn - minIn) * (maxOut - minOut) + minOut;
    //}
}
