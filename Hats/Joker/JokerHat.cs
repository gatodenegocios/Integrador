using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
    public class JokerHat : MonoBehaviourPunCallbacks
    {
    	//fturamente tenho que criar varios proeteise um aleaoriador de projetil, mas por enquanto vamos nos
    	//contentar com o basico
    	public string[] projetil;
        public float force;
    	Crown crown;
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
    		GameObject p = PhotonNetwork.Instantiate("Prefabs/Projetil/JokerBalls/"+ GetRandomBall(), spawnPosition.position, transform.rotation,0);//"Prefabs/Hats/Coroa"
            p.GetComponent<Projectile>().player = crown.player;

    		p.GetComponent<Projectile>().Fire(left,force);
        }

        public string GetRandomBall(){
            int random = Random.Range(0, projetil.Length);

            return projetil[random];
        }



        
    }
}