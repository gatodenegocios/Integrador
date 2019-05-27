using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.Uata.KingAndCast
{
	public class InputKeyboardPlayer : MonoBehaviour
	{
		Player player;
	    // Start is called before the first frame update
	    void Start()
	    {
	        player = GetComponent<Player>();
	    }

		void Update () {
			DetectKey();		
		}

		void DetectKey(){
			if(Input.GetKey(KeyCode.A)){
				player.MoveLeft();
			}
			if(Input.GetKey(KeyCode.D)){
				player.MoveRight();
			}


			if(Input.GetKeyDown(KeyCode.W)){
				player.JumpPress();
			}
			if(Input.GetKeyUp(KeyCode.W)){
				player.JumpRelease();
			}

			if(Input.GetKeyDown(KeyCode.LeftShift)){
				player.Dash();
			}

			if(Input.GetKeyUp(KeyCode.Q)){
				player.Ult();
				// player.ExtracorporealActive();
			}

			if(Input.GetKeyDown(KeyCode.P)){
				player.SuicideButton();
			}

			// if(Input.GetKey(KeyCode.A)){}
			// if(Input.GetKey(KeyCode.A)){}
		}
	}
}