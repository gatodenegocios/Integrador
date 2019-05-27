using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Photon.Pun;
using Photon.Realtime;



// namespace Com.Uata.KingAndCast
// {
    public class KingCrown : MonoBehaviourPunCallbacks
    {
        Rigidbody2D rb;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }



        [PunRPC]
        void RpcDesable(){
            gameObject.active = false;
            transform.position = Vector2.zero;
        }

        // [Command]
        [PunRPC]
        public void CmdEnable(Vector3 position, Vector3 velocity){
            // gameObject.active = true;
            transform.position = position;
            transform.rotation = Quaternion.identity;
            rb.velocity = velocity;
            photonView.RPC("RpcEnable",RpcTarget.All);
            // RpcEnable(position,velocity);
        }
        [PunRPC]
        void RpcEnable(){
            gameObject.active = true;
        //     transform.position = position;
        //     rb.velocity = velocity;
        //     //gameObject.active = true;   
        }


        void OnCollisionEnter2D(Collision2D col) {
            if(col.gameObject.tag != "Player" || !PhotonNetwork.IsMasterClient)
                return;
            
            // GameObject player = col.gameObject;

            // if(!player.GetComponent<CrownManagerPlayer>().IsKing()){
                // player.GetComponent<CrownManagerPlayer>().SetCrown(gameObject);
            // }
            // player.GetComponent<CrownManagerPlayer>().SetKingMode();
            // photonView.RPC("RpcDesable",RpcTarget.All);
        }


        
    }
// }