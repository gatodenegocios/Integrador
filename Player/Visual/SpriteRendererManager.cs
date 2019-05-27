using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpriteRendererManager : MonoBehaviour // MonoBehaviourPun 
{
    public SpriteRenderer localPlayerIndicator;
	SpriteRenderer sr;
    bool invul = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void ShowLocalPlayer(){
        localPlayerIndicator.color = Color.green;

    }

    public void TurnLeft(){
    	sr.flipX = true;
    }
    public void TurnRight(){
    	sr.flipX = false;	
    }

    public void StunOn(){
        sr.color = Color.grey;
    }
    public void StunOff(){
        sr.color = Color.white;
    }

    public void TurnGreen(){
        sr.color = Color.green;   
    }


    public void StartInvunerableEffect(){
        StartCoroutine(TimerInvulnerableEffect());
    }
    IEnumerator TimerInvulnerableEffect(){
        if(sr == null)
            sr = GetComponent<SpriteRenderer>();
            
        invul = true;
        while(invul){
            sr.enabled = false;
            yield return new WaitForSeconds(0.15f);   
            sr.enabled = true;
            yield return new WaitForSeconds(0.15f);
        }
        
    }


    public void EndInvunerableEffect(){
        invul = false;
        sr.enabled = true;
    }
}
