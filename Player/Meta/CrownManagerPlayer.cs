using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Uata.KingAndCast
{
    public class CrownManagerPlayer : MonoBehaviourPunCallbacks
    {
    	//public bool kingMode = false;
        // [SyncVar]
        // public string hatMode = "";
        public GameObject crownPivo;
        public GameObject crownMode;
        // PlayerNetwork pn;
        // Start is called before the first frame update
        void Start()
        {
            // pn = GetComponent<PlayerNetwork>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void Stun(float time){
            if(crownMode!=null){
                crownMode.GetComponent<Crown>().Stun(time);
            }
            // StartCoroutine(CorSturn());
        }

        // IEnumerator CorSturn(){
            // yield return new WaitForSeconds(0f);

        // }


        public bool IsKing(){
            return crownMode != null && crownMode.GetComponent<KingCrown>()!= null;
        }
        public void DisableCrown(){
            crownMode.GetComponent<Crown>().Enable();
        }

        public void SetCrown(GameObject crown){
                if(crownMode!=null && crownMode != crown){
                    if(crownMode.gameObject.tag == "KingCrown"){
                        DisableCrown();
                    }else{
                        crownMode.GetComponent<Crown>().Destroy();
                    }
                }
                if(crown.GetComponent<Crown>().soul!=null){
                	crown.GetComponent<Crown>().soul.GetComponent<Soul>().Death();
                }
                
            photonView.RPC("RpcSetCrown",RpcTarget.All,crown.GetComponent<PhotonView>().ViewID);   
        }

        [PunRPC]
        public void RpcSetCrown(int ViewID){

                GameObject crown = PhotonView.Find(ViewID).gameObject;

                // GameObject crown = PhotonView.Find(ViewID).gameObject;

                crownMode = crown;

                crownMode.transform.parent = crownPivo.transform;
                crownMode.transform.position = crownPivo.transform.position;
                crownMode.transform.rotation = crownPivo.transform.rotation;

                crownMode.GetComponent<Crown>().Disable(gameObject);
        }


    }
}
