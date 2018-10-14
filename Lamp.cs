using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	public class Lamp : MonoBehaviour {
    	public AudioClip lampsfx;
    	public GameObject lightbulb;
		public GameObject[] otherlamps;
		AudioSource lampsound;
		public bool isOn = false;
		public bool once = false;
		Interactable touch;
		public bool cornerflag = true;
		Lamp temp1;
		Lamp temp2;
        public bool debugMode = false;
        public bool debugOn = false;
        GameObject playerpos;
        Room2Win lampson;
		//float timer;
		//float timeBetweenTurnons = 1f;
    	// Use this for initialization
   		void Start () {
        	lampsound = GetComponent<AudioSource>();
        	touch = GetComponent<Interactable>();
            playerpos = GameObject.FindGameObjectWithTag("Room2Pos");
            lampson = playerpos.GetComponent<Room2Win>();
            lampsound.clip = lampsfx;
			temp1 = otherlamps [0].GetComponent<Lamp>();
			if (otherlamps.Length > 1) {
				cornerflag = false;
				temp2 = otherlamps [1].GetComponent<Lamp>();
			}
		}

		void HandHoverUpdate(Hand hand)
    	{
			//Debug.Log("Yo");
			if (hand.GetStandardInteractionButtonDown()) 
			{
				turnOnOrOff ();
				temp1.turnOnOrOff ();
				if (!cornerflag)
					temp2.turnOnOrOff();
			}
    	}

        void Update()
        {
            if (debugMode && debugOn)
            {
                turnOnOrOff();
                temp1.turnOnOrOff();
                if (!cornerflag)
                    temp2.turnOnOrOff();
                debugOn = false;
            }
        }

        public void turnOnOrOff()
    	{
        	if (isOn)
        	{
            	lampsound.Play();
                lampson.numLamps(-1);
				Debug.Log("I'm Off");
            	isOn = false;
				once = true;
            	lightbulb.SetActive(false);
        	}
        	else
        	{
				lampsound.Play();
                lampson.numLamps(1);
                Debug.Log("I'm On");
            	isOn = true;
				once = true;
            	lightbulb.SetActive(true);
        	}
    	}
	}
}

