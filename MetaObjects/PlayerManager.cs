using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Realtime;



namespace Com.Uata.KingAndCast
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
    	ArrayList entityPlayers = new ArrayList();
        int numberOfPlayers = 4;
        

        public Transform[] spawnPoints;
        ArrayList spawnPointsAvaliable = new ArrayList();

        // Start is called before the first frame update
        void Start()
        {
            entityPlayers.Clear();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        // [Command]
        public void AddEntityPlayers(GameObject go) {
        	if(entityPlayers.Count < numberOfPlayers){
        		entityPlayers.Add(go);
        	}
        }

        public void KillPlayersRemain(){
        	for(int i = 0; i < entityPlayers.Count; i++){
        		GameObject go = ((GameObject)entityPlayers[i]);
        		// if(go.GetComponent<NetworkManagerPlayer>().playerSpawned != null){
                    // print(go);
        			go.GetComponent<NetworkManagerPlayer>().KillPlayer();
                // }
        	}
        }

        // [Command]
        public void SpawnPlayers(){
        	for(int i = 0; i < entityPlayers.Count; i++){
        		if(entityPlayers[i]!= null){
        			int sp = Random.Range(0, spawnPointsAvaliable.Count-1);

                    Transform tf = (Transform) spawnPointsAvaliable[sp];
                 ((GameObject)entityPlayers[i]).GetComponent<NetworkManagerPlayer>().RpcSpawnOnServer(tf.position);

        			spawnPointsAvaliable.RemoveAt(sp);
        		}
        	}
        }

        // [Command]
        public void ResetSpawnPoints(){
        	spawnPointsAvaliable.Clear();
        	for(int i = 0; i < spawnPoints.Length; i++){
        		spawnPointsAvaliable.Add(spawnPoints[i]);
        	}
        }


    }
}