using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.Uata.KingAndCast
{

    public class HeadDetector : MonoBehaviour
    {
    	public GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        void OnTriggerEnter2D(Collider2D coll){
        	if(coll.gameObject.tag == "Foot" && coll.gameObject != transform.parent.gameObject){
        		player.GetComponent<PlayerNetwork>().HitByFoot(coll.gameObject.transform.parent.gameObject);
        	}
        }
    }
}