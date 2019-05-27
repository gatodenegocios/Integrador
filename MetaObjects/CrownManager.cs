using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
    public class CrownManager : MonoBehaviourPunCallbacks
    {
    	public GameObject kingCrown;
        [SerializeField] GameObject kingCrownSpawned;
        Transform kingCrownTransformSpawned = null;
        public Transform[] kingCrownSpawnPoints;

        public string[] hats;

        public Transform[] crownSpawnPoints;
        ArrayList crownSpawnPointsAvaliable = new ArrayList();
        ArrayList hatsSpawned = new ArrayList();
        
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        // [Command]
        public void ResetSpawnPoints(){
            // kingCrownTransformSpawned = null;
        	crownSpawnPointsAvaliable.Clear();
    		for(int i = 0; i< crownSpawnPoints.Length; i++){
    			crownSpawnPointsAvaliable.Add(crownSpawnPoints[i]);
    		}
        }
        // [Command]
        public void DeleteKingCrown(){
        	if(kingCrownSpawned != null)
        		kingCrownSpawned.GetComponent<Crown>().Destroy();    	
        }

        // [Command]
        public void SpawnKingCrown(){
            DeleteKingCrown();
        	for(int i = 0 ; i < 1; i++){
                Transform tf = kingCrownTransformSpawned;

                if(tf == null){
            		int sp = Random.Range(0, kingCrownSpawnPoints.Length);

                    tf = kingCrownSpawnPoints[sp];
            		// tf = (Transform) crownSpawnPointsAvaliable[sp];

                    // crownSpawnPointsAvaliable.Remove(tf);

                    kingCrownTransformSpawned = tf;
                }

        		// kingCrownSpawned = Instantiate(kingCrown, tf.position,transform.rotation);
                if(kingCrownSpawned != null){
                    PhotonNetwork.Destroy(kingCrownSpawned);
                }
                kingCrownSpawned = PhotonNetwork.Instantiate("Prefabs/Hats/"+kingCrown.name, tf.position, transform.rotation,0);//"Prefabs/Hats/Coroa"
        	}

        }

        public void DeleteHats(){
            for(int i = 0 ; i < hatsSpawned.Count; i++){
                GameObject hat = (GameObject) hatsSpawned[i];
                if(hat!= null){
                    hat.GetComponent<Crown>().Destroy();    
                }
            }
            hatsSpawned.Clear();
        }

        public void SpawnHats(){
            hatsSpawned.Clear();   

            for(int i = 0 ; i < crownSpawnPoints.Length-1; i++){

                int sp = Random.Range(0, crownSpawnPointsAvaliable.Count);

                Transform tf = (Transform) crownSpawnPointsAvaliable[sp];

                crownSpawnPointsAvaliable.Remove(tf);

                int chapeuIndex = Random.Range(0, hats.Length);

                hatsSpawned.Add(PhotonNetwork.Instantiate("Prefabs/Hats/"+hats[chapeuIndex], tf.position, transform.rotation,0));//"Prefabs/Hats/Coroa"
            }
        }



        // [Command]
        public void CmdThrowKingCrown(Vector3 position, Vector3 velocity){
            // photonView.RPC("RpcEnableKingCrown", RpcTarget.All);//ENVIAR O COMANDO PRRAAA COROAs

            kingCrownSpawned.GetComponent<KingCrown>().CmdEnable(position,velocity);
        }
        // [ClientRpc]
        [PunRPC]
        public void RpcEnableKingCrown(GameObject kc){
            kc.active = true;
        }

        // [Command]
        public void ResetCrown(){
                SpawnKingCrown();
        }

    }
}