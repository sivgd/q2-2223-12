using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System; 

public class ScoreKeeper : MonoBehaviour
{
    [Header("S Rank Requirements")]
    public int killsRequired;
    public float styleRequired;
    public float minutesRequired;

    [Header("Death UI References")]
    public TMP_Text styleText;
    public TMP_Text killsText;
    public TMP_Text timeText;
    public TMP_Text styleGrade;
    public TMP_Text killsGrade;
    public TMP_Text timeGrade;

    [Header("Preferences")]
    public Color[] gradeColors;
    private string[] grades = { "D", "C", "B", "A", "S" };    // please keep this the same  :pleading:

    [Header("Movement Scripts")]
    public MonoBehaviour[] movementScripts;
    private int kills;
    private float style;
    private float time;

    private bool runTimer;
    private bool gameOver;
    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (runTimer) time += Time.deltaTime;
        if (gameOver) gameOverSequence();
    }

    void gameOverSequence()
    {
        Time.timeScale = 0;
        StartCoroutine(GameOver()); 
    }
    IEnumerator GameOver()
    {
        yield return new WaitUntil(()=> gameOver); 
        foreach(MonoBehaviour mvscript in movementScripts)
        {
            mvscript.enabled = false; 
        }
        UpdateDeathUIStats(); 
    }
    void UpdateDeathUIStats()
    {
        styleText.text = "" + style;     //fuck you C# why can't you be cool like javascript
        styleGrade.text = CalculateGrade(style, styleRequired);
        killsText.text = "" + kills;
        killsGrade.text = CalculateGrade((float)kills, (float)killsRequired);   // cast is not redundant ignore editor
        timeText.text = $"{Mathf.Floor(time / 60f)}:{Mathf.Abs(Mathf.Floor(time / 60f) - time)}";
        timeGrade.text = CalculateGrade(time, (minutesRequired * 60f)); 
    }
    /// <summary>
    /// Outputs the corresponding grade by comparing the current value to the max value 
    /// </summary>
    /// <param name="currentValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    string CalculateGrade(float currentValue, float maxValue) // just perform a typecast to float for kills
    {
        double ratio = currentValue / maxValue;
        if (ratio >= 1) return grades[4];
        else if (ratio >= 0.8) return grades[3];
        else if (ratio >= 0.6) return grades[2];
        else if (ratio >= .1) return grades[1];
        else if (ratio >= 0) return grades[0];
        return "F"; 

    }
    string CalculateGrade(float currentValue, float maxValue,bool isTime) // just perform a typecast to float for kills
    {
        double ratio = currentValue / maxValue;
        if (isTime)
        {
            if (ratio >= 1) return grades[1];
            else if (ratio >= 0.8) return grades[2];
            else if (ratio >= 0.6) return grades[3];
            else if (ratio >= .1) return grades[4];
            else if (ratio >= 0) return grades[4];
            return "E";
        }
        else
        {
            if (ratio >= 1) return grades[4];
            else if (ratio >= 0.8) return grades[3];
            else if (ratio >= 0.6) return grades[2];
            else if (ratio >= .1) return grades[1];
            else if (ratio >= 0) return grades[0];
            return "F";
        }
        return "E"; // E for error! 

    }
    #region ACCESSORS AND MUTATORS
    public int getKills()
    {
        return kills; 
    }
    public float getStyle()
    {
        return style; 
    }
    public float getTime()
    {
        return time; 
    }
    public void addToKills(int num)
    {
        kills += num; 
    }
    public void setKills(int newVal)
    {
        kills = newVal; 
    }
    public void setStyle(float newVal)
    {
        style = newVal; 
    }
    public void setTime(float newVal)
    {
        time = newVal; 
    }
    public void StopTimer()
    {
        runTimer = false; 
    }
    public void StartTimer()
    {
        runTimer = true; 
    }
    public void setGameOver(bool gameOver)
    {
        this.gameOver = gameOver; 
    }
    #endregion
}
