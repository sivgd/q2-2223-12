using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class StyleMeterController : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text[] actionDisplay;
    public Slider styleMeter;
    public TMP_Text grade; 
    [Header("Level Specific")]
    public float maxStyle = 100f;

    [Header("Preferences")]
    public float styleDecayRate = 0.1f;
    public float maxMovementMult = 3f;
    public int[] rankRequirements = {40,60,80,100};
    public string[] grades = { "D", "C", "B", "A", "S" }; 
    [Header("Debug")]
    [SerializeField] float overallStyle;
    [SerializeField] float currentStyle; 


    private void Start()
    {
        resetActionQueue(); 
    }
    /// <summary>
    /// STYLE METER PROCESS
    /// 1. Get update from enemy
    /// 2. add corresponding text queue 
    /// 3. update text queue 
    /// 4. (synchronously with 2) update currentStyle
    /// 5. update grade to reflect recent style changes
    /// 6. reset text queue and current grade after certain amt of time 
    ///     - text queue should reset asynchronously 
    /// </summary>

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            addTextToQueue("TEST");
            addToCurrentStyle(5); 
            StartCoroutine(updateStyleScore());
        }
    }
    public void updateStyleMeter()
    {
       
    }
    public void addTextToQueue(string text)
    {
        for(int i = 0; i< actionDisplay.Length; i++)
        {
            if (actionDisplay[i].text == "")
            {
                actionDisplay[i].text = text;
                Debug.Log($"addTextToQueue(): Writing to text queue {actionDisplay[i].name}");
                return; 
            }
        }
        Debug.Log("addTextToQueue(): Empty text slot not available"); 
    }
    public void addToCurrentStyle(int amt)
    {
        currentStyle += amt;
    }
    IEnumerator decayStyle()
    {
        yield return new WaitForEndOfFrame();
        while (currentStyle > 0) currentStyle -= (styleDecayRate * Time.deltaTime); 
    }
    IEnumerator updateStyleScore()
    {
        int maxVal = 1;
        float currentSliderValue; 
        yield return new WaitForEndOfFrame();
        currentSliderValue = styleMeter.value;
        if (currentStyle >= rankRequirements[3])
        {
            grade.text = grades[4];
            maxVal = rankRequirements[3]; 
        }
        else if (currentStyle >= rankRequirements[2])
        {
            grade.text = grades[3];
            maxVal = rankRequirements[3]; 
        }
        else if (currentStyle >= rankRequirements[1])
        {
            grade.text = grades[2];
            maxVal = rankRequirements[2]; 
        }
        else if (currentStyle >= rankRequirements[0])
        {
            grade.text = grades[1];
            maxVal = rankRequirements[1]; 
        }
        else
        {
            grade.text = grades[0];
            maxVal = rankRequirements[0]; 
        }
        styleMeter.maxValue = maxVal;
        styleMeter.value = currentStyle;

    }
    private void resetActionQueue()
    {
        foreach(TMP_Text t in actionDisplay)
        {
            t.text = "";
            Debug.Log($"resetActionQueue(): Reset {t.name}"); 
        }
    }
}