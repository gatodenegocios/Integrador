using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.Uata.KingAndCast
{

	public class SoulInputKeyboard : MonoBehaviour
	{
		Soul soul;
	    // Start is called before the first frame update
	    void Start()
	    {
	        soul = GetComponent<Soul>();
	    }

	    // Update is called once per frame
	    void Update()
	    {
	        	if(Input.GetKey(KeyCode.A)){
					soul.MoveLeft();
				}
				if(Input.GetKey(KeyCode.D)){
					soul.MoveRight();
				}


				if(Input.GetKey(KeyCode.W)){
					soul.MoveUp();
				}
				if(Input.GetKey(KeyCode.S)){
					soul.MoveDown();
				}

				if(Input.GetKeyDown(KeyCode.Space)){
					soul.Possess();
				}


				if(Input.GetKeyDown(KeyCode.Q)){
					soul.Death();
				}
	    }
	}
}