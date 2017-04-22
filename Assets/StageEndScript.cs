using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageEndScript : MonoBehaviour {

    private Text[] texts;
    private Image[] scoreBar;

    private int totalEnemies;
    private int defeated;
    private float fillrate;
    private string rank;
	
	void Awake () {
        texts = GetComponentsInChildren<Text>();
        scoreBar = GetComponentsInChildren<Image>();
        HideElements();
	}

    public void HideElements()
    {
        foreach (var a in texts) {
            a.gameObject.SetActive(false);
        }
        scoreBar[1].gameObject.SetActive(false);
    }

    private void setBarText(string newText) {
        Text barText = transform.Find("kill ratio bar/Text/").GetComponent<Text>();
        barText.text = newText;
    }

    private void setRankText(float killRatio) {
        Text rankText = transform.Find("Rank text/").GetComponent<Text>();
        if (killRatio <= .1f) rankText.text = "E";
        else if (killRatio <= .4f) rankText.text = "D";
        else if (killRatio <= .6f) rankText.text = "C";
        else if (killRatio <= .8f) rankText.text = "B";
        else if (killRatio <= .99f) rankText.text = "A";
        else if (killRatio == 1f) rankText.text = "S";
    }


    IEnumerator fillBar(float maxAmount) {
        float fillAmount = 0;
        Image bar = transform.Find("kill ratio bar/kill ratio/").GetComponent<Image>();       
        for (int i = 0; i < 100; i++) {           
            bar.fillAmount = fillAmount;
            fillAmount += .01f;
            if (fillAmount > maxAmount) fillAmount = maxAmount;
            yield return new WaitForSeconds(.1f);
        }

    }
       

    public void ShowElements() {

        foreach (var a in texts)
        {
            a.gameObject.SetActive(true);
        }
        scoreBar[1].gameObject.SetActive(true);

        totalEnemies = GameManagerScript.Instance.totalEnemies;
        defeated = GameManagerScript.Instance.enemiesDefeated;

        string newBarText = defeated.ToString() + " / " + totalEnemies.ToString();
        setBarText(newBarText);     

        fillrate = (float)defeated / (float)totalEnemies;
        setRankText(fillrate);  
        StartCoroutine("fillBar", fillrate);

        
    }
}
