using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;

namespace Com.Uata.KingAndCast
{
  public class LifeManager : MonoBehaviourPunCallbacks
  {
    [SerializeField]
  	bool invulnerable = true;
  	PlayerNetwork playerNetwork;
  	public int fullLife = 300;
  	int life;

  	public Transform view;
  	
      // Start is called before the first frame update
      void Start()
      {
          playerNetwork = GetComponent<PlayerNetwork>();
          life = fullLife;
      }

      // Update is called once per frame
      void Update()
      {
	  	  float size =(float) life/fullLife;          
	  	  view.transform.localScale = new Vector3(size,view.transform.localScale.y,1);

        if(!PhotonNetwork.IsMasterClient)
          return;

        if(life > fullLife){
          Damage(1);
        }
      }

      public void HitByFoot(){
      	 playerNetwork.KillAndRespawnPlayer();
      }

      public void Damage(int damage){
        if(getInvulnerable())
          return;
          
      	life-=damage;
        photonView.RPC("RpcUpdateLife",RpcTarget.Others,life);


      	if(life<=0){
      		playerNetwork.KillAndRespawnPlayer();
      	}
      }

      [PunRPC]
      void RpcUpdateLife(int l){
        life = l;
      }

      public bool getInvulnerable(){
        return invulnerable;
      }

      

      //[Command] UNET
     	public void StartInvulnerable(){
     		StartCoroutine(Invulnerable());
     	}

     	IEnumerator Invulnerable(){
     		if(playerNetwork== null)
     			playerNetwork = GetComponent<PlayerNetwork>();

     		invulnerable = true;
     		//VISUAL
     		playerNetwork.StartInvulnerableEffect();
     		yield return new WaitForSeconds(1f);
     		invulnerable = false;
     		//DESATIVER VISUAL	
     		playerNetwork.EndInvulnerableEffect(); 

     	}
  }
}
