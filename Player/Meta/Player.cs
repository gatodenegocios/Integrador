using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.Uata.KingAndCast
{
    public class Player : MonoBehaviour
    {
    	MovimentPlayer moviment;
    	Jump jump;
        RigidbodyEffects rbe;
        SpriteRendererManager spriteR;
        PlayerNetwork playerNetwork;
        public bool left = false;
        FindCamera findCamera;

        InputKeyboardPlayer inputKeyboard;

        UltManager ultManager;

        public Transform leftPivo;
        public Transform rightPivo;

        // bool dashing = false;
        // Start is called before the first frame update
        void Start()
        {
            moviment = GetComponent<MovimentPlayer>();
            jump = GetComponent<Jump>();
            rbe = GetComponent<RigidbodyEffects>();
            spriteR = GetComponent<SpriteRendererManager>();
            ultManager = GetComponent<UltManager>();

            playerNetwork = GetComponent<PlayerNetwork>();

            findCamera = GetComponent<FindCamera>();

            inputKeyboard = GetComponent<InputKeyboardPlayer>();

        }




        public void MoveLeft(){
            if(rbe.dashing)
                return;

        	moviment.MoveLeft();
            spriteR.TurnLeft();
            if(!left){
                left = true;
                playerNetwork.TurnLeft();
            }
        }

        public void MoveRight(){
            if(rbe.dashing)
                return;

        	moviment.MoveRight();
            spriteR.TurnRight();
            if(left){
                left = false;
                playerNetwork.TurnRight();
            }
        }


        public void JumpPress(){
        	jump.JumpPress();
        }
    	public void JumpRelease(){
    		jump.JumpRelease();
        }

        public void JumpClient(){
            jump.JumpPress();
        }

        public void Dash(){
            rbe.Dash(left);
        }

        //
        public void HitterEffect(){
            rbe.HitterEffect();
        }

        public void StunOn(){
            spriteR.StunOn();
            rbe.Freeze();
        }
        public void StunOff(){
            spriteR.StunOff();
            rbe.Defreeze();   
        }

        public void TurnGreen(){
            spriteR.TurnGreen();
        }
        public void TurnWhite(){
            spriteR.StunOff();
        }


        public void JumpEffect(){
            rbe.JumpEffect();   
        }



        public void Ult(){
            //IDENTIFICA QUE TIPO DE BIXO EU SOU E CHAMA DETERMINADA FUNÇÃO
            ultManager.Active();
        }

        public Transform GetPivo(){
            return left?leftPivo:rightPivo;
        }

        public Transform GetPivo(bool l){
            return l?leftPivo:rightPivo;   
        }


        public void SuicideButton(){
            playerNetwork.KillAndRespawnPlayer();
        }


    }
}