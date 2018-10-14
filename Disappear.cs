using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
public class Disappear : MonoBehaviour {
    AudioSource audiosrc;
    public AudioClip disappearsfx;
	public GameObject attachedLamp;
    Lamp lamp;
    Animator anim;
    bool isThere = false;

	// Use this for initialization
	void Start () {
        audiosrc = GetComponent<AudioSource>();
        audiosrc.clip = disappearsfx;
        anim = GetComponent<Animator>();
		lamp = attachedLamp.GetComponent<Lamp> ();
	}

	// Update is called once per frame
	void Update () {
		if (lamp.once) 
		{
			lamp.once = false;
			if (!lamp.isOn) {
				audiosrc.Play ();
				Debug.Log ("DISAPPEAR");
				anim.SetTrigger ("Disappear");
			}
			if (lamp.isOn) {
				audiosrc.Play ();
				Debug.Log ("REAPPEAR");
				anim.SetTrigger ("Reappear");
			}
		}
	}
}
}
