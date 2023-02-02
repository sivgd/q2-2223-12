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
    public wallRunning wallRunning;
    public slideSystem slideSystem;
    public playerMove playerMove; 
    private int kills = 0;
    public float style = 0f;
    public float finalTime = 0f; 
    public float time = 0f;

    private bool runTimer;
    private bool gameOver;
    private void Start()
    {
        runTimer = true;
        wallRunning.enabled = true;
        slideSystem.enabled = true;
        playerMove.enabled = true;
        gameOver = false;
        Time.timeScale = 1f; 

    }
    private void Update()
    {

        time += Time.deltaTime; 
        if (!runTimer) finalTime = time; 
        if (gameOver) gameOverSequence();
        UpdateDeathUIStats(); 
    }


    void gameOverSequence()
    {
        Time.timeScale = 0;
        StartCoroutine(GameOver()); 
    }
    IEnumerator GameOver()
    {
        yield return new WaitUntil(()=> gameOver);
        wallRunning.enabled = false;
        slideSystem.enabled = false;
        playerMove.enabled = false;
        runTimer = false; 
        UpdateDeathUIStats(); 
    }
    void UpdateDeathUIStats()
    {
        float cTime = time; 
        styleText.SetText($"STYLE: {style}");     //for some reason this line throws a null reference exception but doesn't actually halt the game 
        styleGrade.SetText(CalculateGrade(style, styleRequired));
        killsText.SetText($"KILLS: " + kills);
        killsGrade.SetText(CalculateGrade((float)kills, (float)killsRequired));   // cast is not redundant ignore editor
        timeText.SetText($"TIME: {cTime}");
        timeGrade.text = CalculateGrade(time, (minutesRequired * 60f));
        killsGrade.color = getGradeColor(killsGrade);
        styleGrade.color = getGradeColor(styleGrade);
        timeGrade.color = getGradeColor(timeGrade);

    }
    Color getGradeColor(TMP_Text textObj)
    {
        string text = textObj.text;
        if (text.Contains(grades[0])) return gradeColors[0];
        else if (text.Contains(grades[1])) return gradeColors[1];
        else if (text.Contains(grades[2])) return gradeColors[2];
        else if (text.Contains(grades[3])) return gradeColors[3];
        else if (text.Contains(grades[4])) return gradeColors[4];
        return Color.white; 
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
