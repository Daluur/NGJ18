using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour {

    public Image Background, Bar, InsanityBar;
	public Gradient Gradient;

    private PlayerHealth playerHealth;

	public void AssignPlayer(PlayerHealth player)
	{
		playerHealth = player;
		gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (playerHealth.GetIsInsane())
        {
            UpdateInsanityBar();
        }
        else { 
            UpdateSanityBar();
        }
    }

    private void UpdateSanityBar() {
        InsanityBar.gameObject.SetActive(false);
        Bar.gameObject.SetActive(true);
        var fillAmount = playerHealth.Sanity / 100f;
        Bar.fillAmount = fillAmount;
		Bar.color = Gradient.Evaluate(fillAmount);
        //if(fillAmount < 0.2)
        //{
        //    //TODO: Possibly do some animation
        //    Bar.color = CriticalColor;
        //}else
        //{
        //    Bar.color = NormalColor;
        //}

    }

    private void UpdateInsanityBar()
    {
        InsanityBar.gameObject.SetActive(true);
        Bar.gameObject.SetActive(false);
        var fillAmount = (playerHealth.BreakDuration - (Time.timeSinceLevelLoad - playerHealth.InsanityStartTime)) / playerHealth.BreakDuration;
        InsanityBar.fillAmount = fillAmount;

    }

}
