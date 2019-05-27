using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
    public class AcessibleArea : MonoBehaviourPunCallbacks
    {
    	RoundManager rm;

        void OnTriggerExit2D(Collider2D coll){
        	if(!PhotonNetwork.IsMasterClient)
        		return;

        	if(rm==null)
        		rm = GameObject.Find("RoundManager").GetComponent<RoundManager>();

        	if(coll.gameObject.tag == "Player"){
        		coll.gameObject.GetComponent<PlayerNetwork>().KillAndRespawnPlayer();
        	}
        		
        
        }
    }
}