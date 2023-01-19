using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [Header("S Rank Requirements")]
    public int killsRequired;
    public float styleRequired;
    public float minutesRequired;

    private int kills;
    private float style;
    private float time;



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
    #endregion
}
