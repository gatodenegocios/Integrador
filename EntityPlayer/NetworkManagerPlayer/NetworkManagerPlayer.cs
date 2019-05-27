using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
    public class NetworkManagerPlayer : MonoBehaviourPunCallbacks
    {
    	public string player;
        public GameObject playerSpawned;
        GameObject roundManager;
        Vector2 lastSpawnPoint;
        // Start is called before the first frame update
        void Start()
        {
            // lastSpawnPoint = new Vector2(0,0);
    		CheckLocalPlayer(); 
        }

        void CheckLocalPlayer(){
              /*         if(PhotonNetwork.isMasterClient){
                CmdAddToList();    
            }
 
            if(isServer)
                CmdAddToList(s);
    */


            if(PhotonNetwork.IsMasterClient){
                AddToList();
            }


        	if(!photonView.IsMine)
        		return;
        	
            //GameObject playerSpawned = Instantiate(player, new Vector2(0,0),transform.rotation);
            //GameObject playerSpawned = PhotonNetwork.Instantiate("Prefabs/Player/"+this.player.name, new Vector3(0f,5f,0f), Quaternion.identity, 0);    

            SpawnOnServer(new Vector2(0,0));
            
            // NetworkServer.Spawn(playerSpawned);

            //playerSpawned.GetComponent<PlayerNetwork>().EnableLocalPlayer();

        	
        }

        public void AddToList(){
            StartCoroutine(CorAddToList());
        }
        IEnumerator CorAddToList(){
            do{
                yield return new WaitForSeconds(0.1f);
                roundManager = GameObject.Find("RoundManager");
            }while(roundManager==null);
            //roundManager = GameObject.Find("RoundManager");
            roundManager.GetComponent<RoundManager>().AddEntityPlayers(gameObject);

        }



        // [TargetRpc]
        void TargetVerifyLocalPlayer(NetworkConnection target, GameObject go){//player.GetComponent<NetworkIdentity>().connectionToClient
            playerSpawned = go;
            go.GetComponent<PlayerNetwork>().EnableLocalPlayer();
            // Invoke("pulo",1f);
        }

        // [Command]
        public void KillAndRespawnPlayer(){
            StartCoroutine(CorKillAndRespawnPlayer());
        }

        IEnumerator CorKillAndRespawnPlayer(){
            KillPlayer();
            yield return new WaitForSeconds(3f);
            SpawnOnServer(lastSpawnPoint);
            //GetComponent<PhotonView>();//.viewID; int target = 

            //photonView.RPC ("SpawnOnServer", RpcTarget.Others, GetComponent<PhotonView>());
            // SpawnOnServer();
        }

        public void RpcSpawnOnServer(Vector2 position){
            photonView.RPC("SpawnOnServer", GetComponent<PhotonView>().Owner,position);
        }

        [PunRPC]
        public void SpawnOnServer(Vector2 position){
            if(playerSpawned!= null)
                return;


            lastSpawnPoint = position;

            playerSpawned = PhotonNetwork.Instantiate("Prefabs/Player/"+this.player, lastSpawnPoint, Quaternion.identity, 0);    

            playerSpawned.GetComponent<PlayerNetwork>().SetNetworkManagerPlayer(this);


            //PhotonNetwork.Instantiate("playerSpawned", new Vector3(0, 0, 0), Quaternion.identity, 0);
            //NetworkServer.SpawnWithClientAuthority(playerSpawned, connectionToClient);
            //TargetVerifyLocalPlayer(connectionToClient,playerSpawned);


            // playerSpawned.GetComponent<LifeManager>().CmdStartInvulnerable();//
            playerSpawned.GetComponent<PlayerNetwork>().EnableLocalPlayer();
        }

        // [TargetRpc]
        public void TargetHitterEffect(){
            playerSpawned.GetComponent<Player>().HitterEffect();
        }

        // [Command]
        public void KillPlayer(){
            photonView.RPC("RpcKillPlayer", GetComponent<PhotonView>().Owner);
        }
        [PunRPC]
        public void RpcKillPlayer(){
            if(playerSpawned != null)
                playerSpawned.GetComponent<PlayerNetwork>().Death();
        }

        // [Command]

        public void ThrowKingCrown(Vector3 position, Vector2 velocity){
            photonView.RPC("CmdThrowKingCrown", RpcTarget.MasterClient,position,velocity);
        }
        [PunRPC]
        public void CmdThrowKingCrown(Vector3 position, Vector2 velocity){
            roundManager.GetComponent<RoundManager>().CmdThrowKingCrown(position,velocity);
        }

        public bool IsLocalPlayer(){
            return photonView.IsMine;
        }


    }
}