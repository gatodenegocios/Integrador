using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
	public class StunBall : MonoBehaviour
	{
		Projectile proj;
	    // Start is called before the first frame update
	    void Start()
	    {
	        proj = GetComponent<Projectile>();
	    }

	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
	   	void OnCollisionEnter2D(Collision2D coll) {
		   	if(!PhotonNetwork.IsMasterClient || coll.gameObject.tag != "Player" || proj.player == coll.gameObject)
		   		return;

		   	GameObject player = coll.gameObject;
		   	
		   	player.GetComponent<PlayerNetwork>().Stunned(2f);

	    }
	}
}