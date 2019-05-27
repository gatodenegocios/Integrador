using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
	public class Soul : MonoBehaviourPunCallbacks
	{
		// public Player player;
		public GameObject player;
		MoveSoul moveSoul;
		public GameObject[] particles;
		GameObject hatPossess;

		bool possess = false;
		bool isPossessed = false;

	    // Start is called before the first frame update
	    void Start()
	    {
	        moveSoul = GetComponent<MoveSoul>(); 

	    	if(!photonView.IsMine)
        		return;

        	GetComponent<SoulInputKeyboard>().enabled = true;
        	// GetComponent<MoveSoul>().enabled = true;
	    }

	    // Update is called once per frame
	    void Update()
	    {
	        
	    }

	    public void MoveLeft(){
	    	moveSoul.MoveLeft();
	    }
	    public void MoveRight(){
	    	moveSoul.MoveRight();
	    }

	    public void MoveUp(){
	    	moveSoul.MoveUp();
	    }

	    public void MoveDown(){
			moveSoul.MoveDown();
	    }

	    public void Possess(){
	    	photonView.RPC("RpcPossess",RpcTarget.MasterClient);   
	    }
	    [PunRPC]
	    public void RpcPossess(){
	    	possess = true;
	    	Release();

	    }
	    
	    public void Release(){
	    	StartCoroutine(CorRelease());
	    	//Possessing = false
	    }
	
		IEnumerator CorRelease(){
			yield return new WaitForSeconds(0.5f);
	    	possess = false;
		}	    

		[PunRPC]
	    public void RpcPossessing(int ViewID){

	    	GameObject hat = PhotonView.Find(ViewID).gameObject;


	    	isPossessed = true;
	    	// hat.transform.parent = transform;
	    	// hat.transform.position = transform.position;
	    	transform.parent = hat.transform;
	    	transform.position = hat.transform.position;
	    	moveSoul.SetPossess(hat);
	    	hat.GetComponent<Crown>().SetSoul(gameObject);
	    	hatPossess = hat;

	    	// GetComponent<PhotonView>().TransferOwnership(hat.GetComponent<PhotonView>().ViewID);
	    	hat.GetComponent<PhotonView>().TransferOwnership(photonView.Owner);



	    }

	    public void RpcDeath(){
	    	photonView.RPC("Death", GetComponent<PhotonView>().Owner);
	    }

	    [PunRPC]
	    public void Death(){
	    	StartCoroutine(CorDeath());
	    	print(player);
	    	player.GetComponent<GosthManager>().Reload();
	    }

	    IEnumerator CorDeath(){
	    	if(hatPossess != null){
	    		hatPossess.GetComponent<Crown>().SetSoul(null);
	    		hatPossess.GetComponent<PhotonView>().TransferOwnership(photonView.Owner);
	    	}

	    	transform.parent = null;

	    	photonView.RPC ("DisableParticles", RpcTarget.All);

	    	// print("É MEU");
	    	// print();

	    	GetComponent<SoulInputKeyboard>().enabled = false;

	    	yield return new WaitForSeconds(1f);

	    	if(player!= null && GetComponent<PhotonView>().IsMine)
	    		player.GetComponent<GosthManager>().EnablePlayer();

	    	PhotonNetwork.Destroy(gameObject);
	    }




	    [PunRPC]
	    void DisableParticles(){
	    	for(int i  = 0 ; i < particles.Length; i ++){
	    		particles[i].transform.parent = null;
	    		particles[i].GetComponent<ParticleManager>().Stop();
	    	}
	    }

	    public void SetParent(GameObject parent){

	
	    	photonView.RPC("RpcSetParent", RpcTarget.All,parent.GetComponent<PhotonView>().ViewID);
	    }

	    [PunRPC]
	    public void RpcSetParent(int ViewID){
	    	GameObject parent = PhotonView.Find(ViewID).gameObject;
	    	player = parent;

	    }


	    void OnTriggerStay2D(Collider2D coll)
    	{

    		if(PhotonNetwork.IsMasterClient && coll.gameObject.tag == "Foot" && hatPossess == null && player!=null){
    			if(coll.gameObject.transform.parent.gameObject != player){
					RpcDeath();
					// print(player);
					// print(coll.gameObject.transform.parent.gameObject == player);
					// return;
				}
    		}
    		if(!PhotonNetwork.IsMasterClient || !possess || (coll.gameObject.tag != "Hat" && coll.gameObject.tag != "FakeHat" )  || isPossessed)
    			return;

    		print(coll.gameObject.tag);
    		if(coll.gameObject.tag == "Hat"){
    			photonView.RPC("RpcPossessing",RpcTarget.All,coll.gameObject.GetComponent<PhotonView>().ViewID);   
    		}else if(coll.gameObject.tag == "FakeHat"){
    			// player.GetComponent<>
    			// player.GetComponent<CrownManagerPlayer>().SetCrown(coll.gameObject);

    			coll.gameObject.GetComponent<Crown>().Active();
    			coll.gameObject.transform.position = player.transform.position;
    			RpcDeath();
    		}
    	}

    	// void OnCollision

	}
}