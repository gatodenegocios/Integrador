using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
    public class HermesHat : MonoBehaviourPunCallbacks
    {
    	Crown crown;
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }

	    // Update is called once per frame
	    void Update()
	    {
	        
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
		   	crown.player.GetComponent<PlayerNetwork>().KillAndRespawnPlayer();
		}
	}
}