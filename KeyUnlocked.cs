using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem; 

public class KeyUnlocked : MonoBehaviour {

	public GameObject tapir; 
	public int numOfPossessedGameObjects; 
	public ChangeSpeech changeSpeechScript;

	public CompanionSpeech companionSpeechScript;
	public ClipHolder[] specializedClips;

	public Transform room2Pos, room2PosC, player, companion;
	public SteamVR_Fade fade;

	//public Shakeable[] shakeableObjects; 
	//private bool[] objectIsShaken; 

	int numberShaken = 0;
	Material tapirMat;
	float colorIncrement;
	IEnumerator coroutine;

	/*  Initializes objects to shake array with length number of possessed objects
		Initializes array of objects with shakeable scripts attached	 
	 	Iterates through number of possessed objects and sets their shook bool values 
	 	to the corresponding array index of objectIsShaken array
	*/

	void Start () {
		tapirMat = tapir.GetComponent<Renderer>().material;
		colorIncrement = 1.0f/(float)numOfPossessedGameObjects;
	}

	void Update() {
		
		if (Input.GetKeyDown (KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {
			Debug.Log ("Skipping 1");
			StartCoroutine (skip ());
		}
	}

	public void Increment()
	{
		Debug.Log("in increment");
		//play first clip of stage
		companionSpeechScript.audioSource.clip = specializedClips[numberShaken].clips[0];
		companionSpeechScript.audioSource.Play();

		//switch to specialized
		companionSpeechScript.SetClips(1,specializedClips[numberShaken].clips);

        numberShaken++;
		changeSpeechScript.setStage (numberShaken);
		//change color of tapir with each shake going from black to rainbow
		tapirMat.color = Color.HSVToRGB(0,0,colorIncrement*numberShaken);

        Debug.Log("Increment was called");
		if (numberShaken >= numOfPossessedGameObjects) {
			Debug.Log ("You saved the room!"); 
			numberShaken = 0; 
			coroutine = rainbow (tapirMat);
			StartCoroutine(coroutine);
		} 
	}

	public int getNumberShaken() {
		return numberShaken;
	}

	IEnumerator rainbow (Material mat) {
		float inc = 1.0f/255.0f;
		float iF;
		while(true) {
			for (int i = 0; i < 255; i++) {
				iF = i;
				mat.color = Color.HSVToRGB(inc*i, 0.3f, 1.0f);
				yield return new WaitForFixedUpdate();
			}
		}
	}

	void HandAttachedUpdate (Hand hand) {
		StartCoroutine (teleport (hand));
	}

	IEnumerator teleport(Hand hand)
	{
		SteamVR_Fade.View(Color.black, 1);
		yield return new WaitForSeconds (1f);
		
		companionSpeechScript.SetClips(1,specializedClips[3].clips); //set to room2
        player.position = room2Pos.position;
        companion.position = room2PosC.position;
		companion.rotation = room2PosC.rotation;
        hand.DetachObject(this.gameObject);
		SteamVR_Fade.View(Color.clear, 1);
		yield return new WaitForSeconds (1f);
           
        Destroy (gameObject);
	}

	IEnumerator skip() {
		SteamVR_Fade.View(Color.black, 1);
		yield return new WaitForSeconds (1f);
		//if (room1)
		//tapir.position = roomPosT.position;
		companionSpeechScript.SetClips(1,specializedClips[3].clips); //set to room2
		player.position = room2Pos.position;
		companion.position = room2PosC.position;
		companion.rotation = room2PosC.rotation;
		SteamVR_Fade.View(Color.clear, 1);
		yield return new WaitForSeconds (1f);
	}
}
