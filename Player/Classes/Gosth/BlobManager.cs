using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
	public class BlobManager : MonoBehaviourPunCallbacks
	{
		PlayerNetwork playerN;
		CrownManagerPlayer cmp;
		public GameObject crownEated;
		public Transform stomachPivo;
		public float reloadTime;

		bool reload = true;

		bool digestion = false;
		bool digestionComplete = false;
	    // Start is called before the first frame update
	    void Start()
	    {
	        playerN = GetComponent<PlayerNetwork>();
	        cmp = GetComponent<CrownManagerPlayer>();
	    }

	    void Update(){
	    	stomachPivo.transform.Rotate(0, 0, 1f);
	    }


	    public void EatHat(){
	    	photonView.RPC("CmdEatHat", RpcTarget.MasterClient);	
	    }
	    [PunRPC]
	    public void CmdEatHat(){
	    	if(cmp.IsKing())
	    		return;

	    	if(digestionComplete){
	    		UseHat();
	    		Reload();
	    	}else{

		    	if(cmp.crownMode!=null && digestion == false && reload){
		    		reload = false;
		    		digestion = true;
		    		digestionComplete = false;
		    		photonView.RPC("RpcEatHat", RpcTarget.All);	
		    		CmdTransformHat();
		    	}
	    	}
	    }

	    [PunRPC]
	    public void RpcEatHat(){
	    	if(cmp.crownMode!=null){
	    		crownEated = cmp.crownMode;
	    		crownEated.transform.parent = stomachPivo;
	    		crownEated.transform.position = stomachPivo.position;
	    		crownEated.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
	    		crownEated.GetComponent<Crown>().DisableSkill();


	    		Color color = Color.white;
	    		color.a = 0.8f;
	    		crownEated.GetComponent<SpriteRenderer>().color = color;
	    		crownEated.GetComponent<SpriteRenderer>().sortingLayerName = "CrownOnBlob";
	    	}
	    }

	    [PunRPC]
	    public void CmdTransformHat(){
	    	StartCoroutine(CorTransformHat());
	    }
	    IEnumerator CorTransformHat(){
            yield return new WaitForSeconds(5f);
            crownEated.GetComponent<Crown>().Destroy();
            GameObject blobHat = PhotonNetwork.Instantiate("Prefabs/Hats/SlimeHat", stomachPivo.position, stomachPivo.rotation,0);
            photonView.RPC("RpcBlobHatInBlob", RpcTarget.All, blobHat.GetComponent<PhotonView>().ViewID);	
            digestion = false;
            digestionComplete = true;
			//crssown.GetComponent<PhotonView>().ViewID);   
	    }

	    [PunRPC]
	    public void RpcBlobHatInBlob(int ViewID){
			crownEated = PhotonView.Find(ViewID).gameObject;
			crownEated.transform.parent = stomachPivo;
			crownEated.transform.position = stomachPivo.position;
			crownEated.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

			Color color = Color.white;
	    	color.a = 0.8f;
	    	crownEated.GetComponent<SpriteRenderer>().color = color;
	    	crownEated.GetComponent<SpriteRenderer>().sortingLayerName = "CrownOnBlob";

	    }

	    public void UseHat(){
			GetComponent<CrownManagerPlayer>().SetCrown(crownEated);
	    	photonView.RPC("RpcUseHat", RpcTarget.All);	
	    }

	    [PunRPC]
	    public void RpcUseHat(){
			digestion = false;
            digestionComplete = false;
	    	crownEated = null;
	    }

	    public bool hasHatOnStomach(){
	    	return crownEated!=null;
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