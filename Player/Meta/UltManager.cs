using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
	public class UltManager : MonoBehaviourPunCallbacks
	{
		public string type;
		Player player;
	    // Start is called before the first frame update
	    void Start()
	    {
	        player = GetComponent<Player>();
	    }

	    // Update is called once per frame
	    void Update()
	    {
	        
	    }

	    public void Active(){
	    	switch(type){
	    		case "Gosth":
	    			GetComponent<GosthManager>().SpawnSoul();
	    		break;
	    		case "Blob":
	    			GetComponent<BlobManager>().EatHat();
	    		break;
	    		case "Observer":
	    			GetComponent<ObserverManager>().Use();
	    		break;
	    		case "Faker":
	    			GetComponent<FakerManager>().Use();
	    		break;
	    	}
	    }

	    public void Death(){
	    	switch(type){
	    		case "Gosth":
	    			GetComponent<GosthManager>().Death();
	    		break;
	    	}	
	    }

	    public bool hasHatOnStomach(){
	    	return type == "Blob" && GetComponent<BlobManager>().hasHatOnStomach();
	    }
	}
}