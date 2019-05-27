using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
	public class FakerManager : MonoBehaviourPunCallbacks
	{
		Player player;
		PlayerNetwork playerN;
		public string[] hats;
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
	    	photonView.RPC("RpcSpawnHat",RpcTarget.MasterClient);	
	    }

	    [PunRPC]
	    public void RpcSpawnHat(){
	    	if(playerN.hasHat() || !reload)
	    		return;

	    	reload = false;

	    	GameObject p = PhotonNetwork.Instantiate("Prefabs/Hats/FakeHats/" + GetRandomHat(), transform.position, transform.rotation,0);//"Prefabs/Hats/Coroa"

	    	Reload();

	        // p.GetComponent<Projectile>().player = gameObject;
	  		// p.GetComponent<Projectile>().Fire(left,force);
	    }

	    public string GetRandomHat(){
            int random = Random.Range(0, hats.Length);

            return hats[random];
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
