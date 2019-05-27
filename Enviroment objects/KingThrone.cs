using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;


namespace Com.Uata.KingAndCast
{
    public class KingThrone : MonoBehaviourPunCallbacks
    {
        public RoundManager rm;
        // Start is called before the first frame update
        void Start()
        {
            // rm = GameObject.Find("RoundManager").GetComponent<RoundManager>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnTriggerEnter2D(Collider2D coll){
        	if(coll.gameObject.tag != "Player" || !(coll.gameObject.GetComponent<CrownManagerPlayer>().IsKing() || !PhotonNetwork.IsMasterClient)) //TEM QUE ADD AQUI TBM QUE SÓ PODE PEGAR SE ELE FOR O SERVIDOR
        		return;

        	coll.gameObject.GetComponent<RigidbodyEffects>().Freeze();
        	coll.gameObject.transform.position = transform.position;

            rm.ResetRound();
        }
    }
}