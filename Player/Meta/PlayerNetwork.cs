using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
    public class PlayerNetwork : MonoBehaviourPunCallbacks
    {
        public NetworkManagerPlayer nmp;
        Player player;
        CrownManagerPlayer cp;
        Rigidbody2D rb;
        SpriteRendererManager spriteR;
        LifeManager lm;
        MovimentPlayer movimentPlayer;

        UltManager ultManager;


        void Start()
        {
            player = GetComponent<Player>();
            cp = GetComponent<CrownManagerPlayer>();
            rb = GetComponent<Rigidbody2D>();
            spriteR = GetComponent<SpriteRendererManager>();
            lm = GetComponent<LifeManager>();

            ultManager = GetComponent<UltManager>();

            movimentPlayer = GetComponent<MovimentPlayer>();

            if(PhotonNetwork.IsMasterClient){
                lm.StartInvulnerable();
            }
        }

        public void EnableLocalPlayer(){
        	GetComponent<InputKeyboardPlayer>().enabled = true;
            GetComponent<FindCamera>().enabled = true; 
            GetComponent<FindCamera>().StartFindCamera();

            if(spriteR == null)
                spriteR = GetComponent<SpriteRendererManager>();
                
            spriteR.ShowLocalPlayer();
        }


        public void SetNetworkManagerPlayer(NetworkManagerPlayer m){
            nmp = m;
        }

        public void HitByFoot(GameObject hitter){

            if(!PhotonNetwork.IsMasterClient)
                return;

            hitter.GetComponent<PlayerNetwork>().TargetHitterEffect(); 
            

            if(lm.getInvulnerable())
                return;

            

            SetOtherCameraTarget(hitter.transform);//MUDA O ALVO DA CAMERA PRO AMIGUINHO Q TE MATO
            //TIRAR QUALQUER COROA QUE O MALUCO POSSUA


            lm.HitByFoot();

               
        }



        public void TargetHitterEffect(){
            RpcSendTargetHitterEffect();
        }

        public void RpcSendTargetHitterEffect(){
             photonView.RPC("RpcGetTargetHitterEffect",GetComponent<PhotonView>().Owner);
        }

        [PunRPC]
        public void RpcGetTargetHitterEffect(){
            player.HitterEffect();
        }


        public void KillAndRespawnPlayer(){
            if(cp.IsKing()){//|| ((ultManager.type == "Blob") && (ultManager.hasHatOnStomach()))
                cp.DisableCrown();
            }
            photonView.RPC("RpcKillAndRespawnPlayer", GetComponent<PhotonView>().Owner);
        }

        [PunRPC]
        public void RpcKillAndRespawnPlayer(){
            nmp.KillAndRespawnPlayer();

        }

        [PunRPC]
        public void ThrowKingCrown(){
            nmp.ThrowKingCrown(transform.position,rb.velocity);
        }


        



        void SetOtherCameraTarget(Transform t){
            GetComponent<FindCamera>().SetOtherCameraTarget(t);
        }

        public void Death(){
            RpcDeath();
            ultManager.Death();
        }

        [PunRPC]
        public void RpcDeath(){
            PhotonNetwork.Destroy(gameObject);
        }


        public void TurnLeft(){
            photonView.RPC("RpcTurnLeft", RpcTarget.Others);
        }

        [PunRPC]
        public void RpcTurnLeft(){
            if(nmp == null)
                spriteR.TurnLeft();
        }

        


        public void TurnRight(){
            photonView.RPC("RpcTurnRight", RpcTarget.Others);
        }

        [PunRPC]
        public void RpcTurnRight(){
            if(nmp == null)
                spriteR.TurnRight();
        }

        public void StartInvulnerableEffect(){
            photonView.RPC("RpcStartInvulnerableEffect", RpcTarget.All);
        }
        public void EndInvulnerableEffect(){
            photonView.RPC("RpcEndInvulnerableEffect", RpcTarget.All);
        }

        [PunRPC]
        void RpcStartInvulnerableEffect(){
            if(spriteR == null)
                spriteR = GetComponent<SpriteRendererManager>();
            spriteR.StartInvunerableEffect();
        }
        [PunRPC]
        void RpcEndInvulnerableEffect(){
            spriteR.EndInvunerableEffect();
        }

        public bool hasHat(){
            return cp.crownMode!=null;
        }

        public void RemoveLinkCrown(){
            cp.crownMode = null;
        }




        #region SideEffects
        
            public void Damage(int damage){
                lm.Damage(damage);           
            }

            public void CmdSlowOn(float time){
                // photonView.RPC("RpscSlowOn", GetComponent<PhotonView>().Owner);
                SlimeBallEffect(time);
            }



            public void CmdStunOn(float time){
                Stunned(time);   
            }

            //
            public void Stunned(float time){
                photonView.RPC("RpcStunned", GetComponent<PhotonView>().Owner, time);
            }

            [PunRPC]
            public void RpcStunned(float time){ 
                StopCoroutine(CorRpcStunned(time));
                StartCoroutine(CorRpcStunned(time));
            }

            IEnumerator CorRpcStunned(float time){
                player.StunOn();
                photonView.RPC("RpcStunOn", RpcTarget.All);
                yield return new WaitForSeconds(time);
                photonView.RPC("RpcStunOff", RpcTarget.All);
                player.StunOff(); 
            }

            [PunRPC]
            public void RpcStunOn(){
                spriteR.StunOn();
            }
            [PunRPC]
            public void RpcStunOff(){
                spriteR.StunOff();
            }


            public void SlimeBallEffect(float t){
                photonView.RPC("RpcSlimeBallEffect", GetComponent<PhotonView>().Owner, t);
            }

            [PunRPC]
            public void RpcSlimeBallEffect(float t){
                StopCoroutine(CorSlimeBallEffect(t));
                StartCoroutine(CorSlimeBallEffect(t));
            }

            IEnumerator CorSlimeBallEffect(float t){
                float originalVelocity = movimentPlayer.vel;
                movimentPlayer.vel = movimentPlayer.vel/2;
                photonView.RPC("RpcTurnGreen", RpcTarget.All);
                yield return new WaitForSeconds(t);
                movimentPlayer.vel = originalVelocity;
                photonView.RPC("RpcTurnWhite", RpcTarget.All);
            }

            [PunRPC]
            public void RpcTurnGreen(){
                player.TurnGreen();
            }
            [PunRPC]
            public void RpcTurnWhite(){
                player.TurnWhite();
            }



            public void JumpEffect(){
                photonView.RPC("RpcJumpEffect", GetComponent<PhotonView>().Owner);
            }
            [PunRPC]
            public void RpcJumpEffect(){
                player.JumpEffect();
            }


            public void ObserverBallEffect(float time){
                photonView.RPC("RpcObserverBallEffect", GetComponent<PhotonView>().Owner, time);
            }

            [PunRPC]
            public void RpcObserverBallEffect(float time){
                cp.Stun(time);
            }


            public void CmdLoseLifeOn(int t, int l){
                StartCoroutine(CorLoseLifeOn(t,l));
            }


            IEnumerator CorLoseLifeOn(int t, int l){
                for(int i = 0; i < t; i++){
                    lm.Damage(l);
                    yield return new WaitForSeconds(0.5f);
                }
            }


        #endregion

    }
}