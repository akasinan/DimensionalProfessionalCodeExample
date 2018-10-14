using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	public class KeyOne : MonoBehaviour {
        Transform roomPos;
        Transform roomPosC;
		//Transform roomPosT;
        Transform player;
        Transform companion;
		public CompanionSpeech companionSpeechScript;
		public ClipHolder specializedClip;
		GameObject wallWithIntroAudio; 
        public GameObject soundtrack;
        //Transform tapir;

        //CHECK ONE TO INDICATE THE ROOM THE PLAYER IS CURRENTLY IN
        public bool intro;
        public bool room1;
        public bool room2;
        public bool room3;
		public SteamVR_Fade fade;
		// Use this for initialization
		void Start () {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
            companion = GameObject.FindGameObjectWithTag("Companion").GetComponent<Transform>();
			//tapir = GameObject.FindGameObjectWithTag ("Tapir").GetComponent<Transform> ();
			//roomPosT = GameObject.FindGameObjectWithTag ("VictoryTPos").GetComponent<Transform> ();
            if (intro)
            {
                roomPos = GameObject.FindGameObjectWithTag("Room1Pos").GetComponent<Transform>();
                roomPosC = GameObject.FindGameObjectWithTag("Room1PosC").GetComponent<Transform>();
				wallWithIntroAudio = GameObject.FindGameObjectWithTag ("IntroVidWithAudio"); 
				StartCoroutine (playSoundtrackOnVideoEnd ()); 
            }
            if (room1)
            {
                roomPos = GameObject.FindGameObjectWithTag("Room2Pos").GetComponent<Transform>();
                roomPosC = GameObject.FindGameObjectWithTag("Room2PosC").GetComponent<Transform>();
            }
            if (room2)
            {
                roomPos = GameObject.FindGameObjectWithTag("Room3Pos").GetComponent<Transform>();
                roomPosC = GameObject.FindGameObjectWithTag("Room3PosC").GetComponent<Transform>();
            }
            if (room3)
            {
                roomPos = GameObject.FindGameObjectWithTag("VictoryPos").GetComponent<Transform>();
                roomPosC = GameObject.FindGameObjectWithTag("VictoryPosC").GetComponent<Transform>();
            }
        }
	
		void Update() {
			
			if (intro)
			{
				if (Input.GetKeyDown (KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
					Debug.Log ("Skipping intro");
					StartCoroutine (skip ());
					wallWithIntroAudio.SetActive (false); 
					if (soundtrack.activeSelf == false) {
						soundtrack.SetActive (true); 
					}
				} 
			}	
			if (room1) {
				if (Input.GetKeyDown (KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {
					Debug.Log ("Skipping 1");
					StartCoroutine (skip ());
				}
			}
			if (room2) {
				if (Input.GetKeyDown (KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) {
					Debug.Log ("Skipping 2");
					StartCoroutine (skip ());
				}
			}
			if (room3) {
				if (Input.GetKeyDown (KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) {
					Debug.Log ("Skipping 3");
					StartCoroutine (skip ());
				}
			}
		}

		// Update is called once per frame
		void HandAttachedUpdate (Hand hand) {
			
			StartCoroutine (teleport (hand));
            if (intro)
            {
				wallWithIntroAudio.SetActive(false); 
				if (soundtrack.activeSelf == false) {
					soundtrack.SetActive (true); 
				}
            }
        }

		IEnumerator teleport(Hand hand)
		{
            //if (intro)
            //{
                //playSoundtrack.playSoundtrack(); 
            //}
			companionSpeechScript.SetClips(1,specializedClip.clips);
			SteamVR_Fade.View(Color.black, 1);
			yield return new WaitForSeconds (1f);
			//if (room1)
				//tapir.position = roomPosT.position;
            player.position = roomPos.position;
            companion.position = roomPosC.position;
			companion.rotation = roomPosC.rotation;
            
			SteamVR_Fade.View(Color.clear, 1);
			yield return new WaitForSeconds (1f);
            hand.DetachObject(this.gameObject);
            Destroy (gameObject);

		}

		IEnumerator skip()
		{
			companionSpeechScript.SetClips(1,specializedClip.clips);
			SteamVR_Fade.View(Color.black, 1);
			yield return new WaitForSeconds (1f);

			player.position = roomPos.position;
			companion.position = roomPosC.position;
			companion.rotation = roomPosC.rotation;
			SteamVR_Fade.View(Color.clear, 1);
			yield return new WaitForSeconds (1f);

		}

		IEnumerator playSoundtrackOnVideoEnd()
		{
			yield return new WaitForSeconds(35.0f); 
			soundtrack.SetActive (true); 
		}
	}
}

