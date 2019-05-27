using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
	public class ObserverManager : MonoBehaviourPunCallbacks
	{
		public float force;
		Player player;
		PlayerNetwork playerN;
		public float reloadTime;
		bool reload = true;
	    // Start is called before the first frame update
	    void Start()
	    {
	        player = GetComponent<Player>();
	        playerN = GetComponent<PlayerNetwork>();
	    }

	    // Update is called once per frame
	    void Update()
	    {
	        
	    }

	    public void Use(){
	    	photonView.RPC("RpcSpawnProjectil",RpcTarget.MasterClient,player.GetComponent<Player>().left);	
	    }

	    [PunRPC]
	    public void RpcSpawnProjectil(bool left){
	    	if(playerN.hasHat() || !reload)
	    		return;

	    	reload = false;

	        Transform spawnPosition = player.GetComponent<Player>().GetPivo(left);
	    	GameObject p = PhotonNetwork.Instantiate("Prefabs/Projetil/Observer/ObserverBall", spawnPosition.position, transform.rotation,0);//"Prefabs/Hats/Coroa"
	        p.GetComponent<Projectile>().player = gameObject;
	  		p.GetComponent<Projectile>().Fire(left,force);

	  		Reload();
	     }

	    void Reload(){
	    	StartCoroutine(CorReload());
	    }
	    IEnumerator CorReload(){
	    	yield return new WaitForSeconds(reloadTime);
	    	reload = true;
	    }
	}
}
