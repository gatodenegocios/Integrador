using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
	public class FakeStunCrown : MonoBehaviour
	{
		Crown crown;
	    // Start is called before the first frame update
	    void Start()
	    {
	        crown = GetComponent<Crown>();

	    }

	    // Update is called once per frame
	    void Update()
	    {
	    	// if(crown.player==null || !PhotonNetwork.IsMasterClient){
	    	// 	return;
	    	// }

	     //    crown.player.GetComponent<PlayerNetwork>().KillAndRespawnPlayer();
	    }

	    void OnEnable(){
	    	if(PhotonNetwork.IsMasterClient){
	    		StartCoroutine(CorActiveSkil());
	    	}
	    }

	    IEnumerator CorActiveSkil(){
	    	if(crown==null){
	    		crown = GetComponent<Crown>();
	    	}
	    	while(crown.player==null){
	    		yield return new WaitForSeconds(0.1f);
	    	}
	    	
	    	float time = GetComponent<Crown>().lifeTime;
	    	crown.player.GetComponent<PlayerNetwork>().CmdStunOn(time);
	    }
	}
}