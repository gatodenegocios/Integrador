using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
	public class DeathCrown : MonoBehaviour
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
	    	if(crown.player==null){
	    		return;
	    	}

	        crown.player.GetComponent<PlayerNetwork>().KillAndRespawnPlayer();
	    }

/*
	    void OnCollisionEnter2D(Collision2D col) {
                if(col.gameObject.tag != "Player" || !PhotonNetwork.IsMasterClient)
                    return;
                
                GameObject player = col.gameObject;


                player.GetComponent<PlayerNetwork>().KillAndRespawnPlayer();
        }
*/

	}
}
