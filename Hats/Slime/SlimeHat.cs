using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
	public class SlimeHat : MonoBehaviourPunCallbacks
	{
    	Crown crown;
	    // Start is called before the first frame update
        // Start is called before the first frame update
        void Start()
        {
            crown = GetComponent<Crown>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)){
                photonView.RPC("RpcSpawnBall",RpcTarget.MasterClient,crown.player.GetComponent<Player>().left);	
    		}
        }
        [PunRPC]
        public void RpcSpawnBall(bool left){

            if(crown==null){
                crown = GetComponent<Crown>();
            }

            Transform spawnPosition = crown.player.GetComponent<Player>().GetPivo(left);
    		GameObject p = PhotonNetwork.Instantiate("Prefabs/Projetil/SlimeBalls/SlimeBall", spawnPosition.position, transform.rotation,0);//"Prefabs/Hats/Coroa"
            p.GetComponent<Projectile>().player = crown.player;

    		p.GetComponent<Projectile>().Fire(left,10f);
        }
	}
}