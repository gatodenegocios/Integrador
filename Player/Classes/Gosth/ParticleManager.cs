using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Photon.Pun;
using Photon.Realtime;

public class ParticleManager : MonoBehaviourPunCallbacks
{
	ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stop(){
    	StartCoroutine(StopCor());
    }

    IEnumerator StopCor(){
    	particleSystem.Stop();
    	yield return new WaitForSeconds(1f);
    	Destroy(gameObject);
    }

}
