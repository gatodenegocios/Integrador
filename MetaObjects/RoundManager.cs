using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Realtime;



namespace Com.Uata.KingAndCast
{
    public class RoundManager : MonoBehaviourPunCallbacks
    {
        
        bool roundStarted = false;

        public PlayerManager playerManager;
        public CrownManager crownManager;
        
        void Start(){
        	// if(!photonView.IsMine)
                // return;
        }

        // [Command]
        public void AddEntityPlayers(GameObject go) {
            playerManager.AddEntityPlayers(go);
        }

        // [Command]
        public void ResetRound(){
            if(!PhotonNetwork.IsMasterClient)
                return;

        	StartCoroutine(CorResetRound());
        }

        IEnumerator CorResetRound(){

        	yield return new WaitForSeconds(1f);

            crownManager.DeleteKingCrown();

            crownManager.DeleteHats();

            playerManager.KillPlayersRemain();

        	roundStarted = false;

        	yield return new WaitForSeconds(1f);

        	StartRound();
        }

        // [Command]
        public void StartRound(){

        	if(roundStarted)
        		return;

        	roundStarted = true;
        	playerManager.ResetSpawnPoints();

            crownManager.DeleteKingCrown();    	
            playerManager.SpawnPlayers();

            crownManager.ResetSpawnPoints();
            crownManager.SpawnKingCrown();

            crownManager.SpawnHats();
        }

        // [Command]

        public void CmdThrowKingCrown(Vector3 position, Vector3 velocity){
            crownManager.CmdThrowKingCrown(position,velocity);
        }
        // [Command]
        public void ResetCrown(){
            crownManager.ResetCrown();
        }
    }
}