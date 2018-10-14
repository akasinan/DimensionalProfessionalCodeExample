using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2Win : MonoBehaviour {
    public int lampsOn = 0;
    public GameObject key;

    public CompanionSpeech companionSpeechScript;
    public AudioClip congrats;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(lampsOn == 7 && key != null)
        {
            if (!key.active) {
                companionSpeechScript.audioSource.clip = congrats;
                companionSpeechScript.audioSource.Play();
                key.SetActive(true);
            }
        }
	}

    public void numLamps(int i)
    {
        lampsOn += i;
    }
}
