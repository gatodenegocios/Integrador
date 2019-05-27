using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Photon.Pun;
using Photon.Realtime;


namespace Com.Uata.KingAndCast
{
    public class Crown : MonoBehaviourPunCallbacks
    {
        
        public GameObject player;
        public List<MonoBehaviour> WhenOn;
        public GameObject soul;
        public float lifeTime;
        bool active = false;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void Stun(float time){
            StartCoroutine(CorStun(time));
        }

        IEnumerator CorStun(float time){
            DisableSkill();
            photonView.RPC("StunColorOn",RpcTarget.All);   
            yield return new WaitForSeconds(time);
            EnableSkill();
            photonView.RPC("StunColorOff",RpcTarget.All);   
        }

        [PunRPC]
        public void StunColorOn(){
            GetComponent<SpriteRenderer>().color = Color.grey;
        }
        [PunRPC]
        public void StunColorOff(){
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        public void SetPlayer(GameObject p){
            player = p;
        }

        public void Destroy(float t){
            StartCoroutine(CorDestroy(t));
        }

        IEnumerator CorDestroy(float t){
            yield return new WaitForSeconds(t);
            Destroy();
        }

        public void Destroy(){
            RpcDestroy();
        }


        public void SetSoul(GameObject s){
            soul = s;
        }

        [PunRPC]
        public void RpcDestroy(){
            player.GetComponent<PlayerNetwork>().RemoveLinkCrown();
        	PhotonNetwork.Destroy(gameObject);
        }

        public void Enable(){

            transform.parent = null;
            GetComponent<Collider2D>().enabled = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            DisableSkill();
        }

        public void Disable(GameObject p){
            if(soul!= null){
                soul.GetComponent<Soul>().Death();
            }
            
            SetPlayer(p);

            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            if(player.GetComponent<PhotonView>().IsMine)
                EnableSkill();
        }

        void EnableSkill(){
            for(int i = 0 ; i < WhenOn.Count; i++){
                WhenOn[i].enabled = true;
            }
        }
        public void DisableSkill(){
            for(int i = 0 ; i < WhenOn.Count; i++){
                WhenOn[i].enabled = false;
            }
        }

        public void Active(){
            active = true;
        }

        void OnCollisionEnter2D(Collision2D col) {
                if(col.gameObject.tag != "Player" || !PhotonNetwork.IsMasterClient || (gameObject.tag!="Hat" && gameObject.tag!="KingCrown"))
                    return;

                
                player = col.gameObject;


                if(!player.GetComponent<CrownManagerPlayer>().IsKing() || GetComponent<DeathCrown>() != null){
                    player.GetComponent<CrownManagerPlayer>().SetCrown(gameObject);
                }
        }




        void OnTriggerEnter2D(Collider2D col) {
                if(col.gameObject.tag != "Player" || !PhotonNetwork.IsMasterClient || !active)
                    return;

                
                player = col.gameObject;


                // if(!player.GetComponent<CrownManagerPlayer>().IsKing()){
                player.GetComponent<CrownManagerPlayer>().SetCrown(gameObject);
                Destroy(0.1f);
                // }
        }

        void OnTriggerExit2D(Collider2D col) {
                if(col.gameObject.tag != "Player" || !PhotonNetwork.IsMasterClient)
                    return;

                active = true;
        }
    }
}