using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
	public class GosthManager : MonoBehaviourPunCallbacks
	{
		FindCamera findCamera;
		InputKeyboardPlayer inputKeyboard;
		GameObject soul;
		CrownManagerPlayer crownMP;

		public float reloadTime;
		bool reload = true;

	    // Start is called before the first frame update
	    void Start()
	    {
	        findCamera = GetComponent<FindCamera>();
	        inputKeyboard = GetComponent<InputKeyboardPlayer>();
	        crownMP = GetComponent<CrownManagerPlayer>();
	    }

	    // Update is called once per frame
	    void Update()
	    {
	        
	    }

	    void DesablePlayer(){	    	
	    	GetComponent<InputKeyboardPlayer>().enabled = false;
	    	// if(crownMP.crownMode!= null){
	    		// crownMP.crownMode
	    	// }
	    }
	    public void EnablePlayer(){
	    	GetComponent<InputKeyboardPlayer>().enabled = true;
	    	TargetCamera();
	    }

	   	public void SpawnSoul(){
	   		if(crownMP.crownMode!= null || !reload)
	   			return;

	   		reload = false;

	    	soul = PhotonNetwork.Instantiate("Prefabs/Player/GosthSoul", transform.position, Quaternion.identity, 0);    

	    	soul.GetComponent<Soul>().SetParent(gameObject);
	    	DesablePlayer();
	    	findCamera.SetOtherCameraTargetInstantanious(soul.transform);
	    }

	    public void TargetCamera(){
	    	findCamera.SetOtherCameraTarget(gameObject.transform);	
	    }

	    public void RpcDeath(){
	    	photonView.RPC("Death", GetComponent<PhotonView>().Owner);
	    	// Reload();
	    }

	    public void Death(){
	    	if(soul != null){
	    		soul.GetComponent<Soul>().Death();
	    		// photonView.RPC("Reload", RpcTarget.MasterClient);
	    	}
	    }

	    public void Reload(){
	    	StartCoroutine(CorReload());
	    }
	    IEnumerator CorReload(){
	    	yield return new WaitForSeconds(reloadTime);
	    	photonView.RPC("InstaReload", GetComponent<PhotonView>().Owner);
	    }

	    [PunRPC]
	    void InstaReload(){
	    	reload = true;
	    }


	}
}