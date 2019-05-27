using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
    public class ScriptTapaBuraco : MonoBehaviourPunCallbacks
    {
    	public RoundManager rm;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnTriggerEnter2D(Collider2D col){
        	if(PhotonNetwork.IsMasterClient){
        		rm.ResetRound();
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}