using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour {

	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

	void FixedUpdate ()
	{
		if(target==null)
			return;


		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = new Vector3(smoothedPosition.x,smoothedPosition.y,transform.position.z);

		// transform.LookAt(target);
	}

	public void SetTarget(Transform t){
		target = t;
	}
	public void SetTargetWithTime(Transform t,float time){
		//target = t;
		StartCoroutine(CorSetTargetWithTime(t,time));
	}

	private IEnumerator CorSetTargetWithTime(Transform t,float time){
		yield return new WaitForSeconds(time);
		target = t;
	}

}