using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions; 

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
    public float textLifeTime = 3f;
    public Color[] gradeColors;
    //public int[] rankRequirements = {};
    public string[] grades = { "D", "C", "B", "A", "S" }; 
    [Header("Debug (Read only)")]
    [SerializeField] float currentStyle;
    [SerializeField] private float dStyle = 0f;
    [SerializeField] private float decayRollingMult = 1f; 

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
            addToStyleScore(1); 
        }
        updateStyleMeter(); 
        
    }
    public void updateStyleMeter()
    {
        decayStyle();
        updateStyleScore();
    }
    public void addTextToQueue(string text)
    {
        int reps = 0; 
        for (int i = 0; i < actionDisplay.Length; i++)
        {
            if (actionDisplay[i].text.Contains(text))
            {
                if (actionDisplay[i].text.Contains("("))
                {
                    reps = getNumFromString(actionDisplay[i].text); 
                }
                reps++;
                actionDisplay[i].text = $"({reps}x) {text}";
                StartCoroutine(removeText(i, $"({reps}x) {text}")); 
                return; 
            }
            else if(actionDisplay[i].text == "")
            {
                reps++; 
                actionDisplay[i].text = $"({reps}x) {text}";
                StartCoroutine(removeText(i, $"({reps}x) {text}"));
                return; 
            }
        }
        Debug.Log("addTextToQueue(): Empty text slot not available");
        return; 
    }
    /// <summary>
    /// Removes the specified test from the specified index
    /// </summary>
    /// <param name="index"></param> the index of the text
    /// <param name="text"></param> the text to remove 
    /// <returns></returns>
    IEnumerator removeText(int index,string text)
    {
        yield return new WaitForSecondsRealtime(textLifeTime);
        if (actionDisplay[index].text == text)
        {
            actionDisplay[index].text = "";
        }
        yield break; 
    }
   
    void decayStyle()
    {
        if (dStyle > 0)
        {
            //decayRollingMult += Time.deltaTime;
            dStyle -= ((styleDecayRate * decayRollingMult) * Time.deltaTime);
        }
    }
    void updateStyleScore()
    {
        int maxVal = grades.Length;
        int minVal = 0; 
        int currGrade = Mathf.Clamp((int)Math.Floor(dStyle / grades.Length),0, grades.Length * grades.Length); 
        switch (currGrade)
        {
            case 0:
                grade.text = grades[0];
                grade.color = gradeColors[0];
                minVal = 0;
                maxVal = grades.Length;
                break;
            case 1:
                grade.text = grades[1];
                grade.color = gradeColors[1];
                minVal = grades.Length;
                maxVal = grades.Length * 2;
                break;
            case 2:
                grade.text = grades[2];
                grade.color = gradeColors[2];
                minVal = grades.Length*2;
                maxVal = grades.Length * 3;
                break;
            case 3:
                grade.text = grades[3];
                grade.color = gradeColors[3];
                minVal = grades.Length*3;
                maxVal = grades.Length * 4;
                break;
            case 4:
                grade.text = grades[4];
                grade.color = gradeColors[4];
                minVal = grades.Length * 3;
                maxVal = grades.Length * 4;
                break; 
        }
        if(currGrade > 0.5 * grades.Length)
        {
            Debug.Log("Player is on fire"); 
        }
        styleMeter.maxValue = maxVal - minVal;
        styleMeter.value = dStyle - minVal ;

    }
    private void resetActionQueue()
    {
        foreach(TMP_Text t in actionDisplay)
        {
            t.text = "";
            Debug.Log($"resetActionQueue(): Reset {t.name}"); 
        }
    }
    public void addToStyleScore(int amt)
    {
        dStyle += amt;
        currentStyle += amt;
        //decayRollingMult = 1f; 
    }
    public void filterStyleFromDeathType(DeathType deathType, int style)
    {
        switch (deathType)
        {
            case DeathType.Explosion:
                addTextToQueue("BOOM");
                addToStyleScore(style); 
                break;
            case DeathType.Normal:
                addTextToQueue("KILL");
                addToStyleScore(style);
                break; 
        }
    }
    private int getNumFromString(string str)
    {
        char[] strAsArray = str.ToCharArray();
        string numString = "+";
        for (int i = 0; i < strAsArray.Length; i++)
        {
            if (Char.IsDigit(strAsArray[i]))
            {
                numString += strAsArray[i];
            }
        }
        int output = Int32.Parse(numString);
        return output;
    }
}
public enum DeathType
{
    Explosion,
    Normal
}
