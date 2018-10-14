using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Valve.VR.InteractionSystem
{

	[RequireComponent(typeof(AudioSource))]
    public class Shakeable : MonoBehaviour
    {
		InstantiateGhost ghostAppears; 
		public KeyUnlocked keyUnlockScript; 
		int shakecounter = 0;
        float objectspeed;
        public bool hasGhost;
        public AudioSource audioShake;

        public int numshakes;
        public bool shook = false;
        public float shakethreshold = 0.0f;
        public Rigidbody rb;
        bool isPlaying = false;

        // Use this for initialization
        void Start()
        {
            if (hasGhost)
			    ghostAppears = GetComponent<InstantiateGhost> (); 
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void HandAttachedUpdate(Hand hand)
        {
            if (!audioShake.isPlaying) {
                audioShake.Play();
            }

			objectspeed = Mathf.Abs(hand.GetTrackedObjectVelocity().x) + Mathf.Abs(hand.GetTrackedObjectVelocity().y) +  Mathf.Abs(hand.GetTrackedObjectVelocity().z);

            if (objectspeed > shakethreshold)
            {
                shakecounter++;
            }
            if(shakecounter > numshakes && shook == false)
            {
				Debug.Log ("SHAKE IT");
				shook = true;
                shakecounter = 0;
                
                rb.useGravity = true;
                rb.isKinematic = false;
                if (hasGhost) {
                    
                    keyUnlockScript.Increment(); 
                    ghostAppears.isShaken (); 
                }
				return;
            }
        }

        void OnDetachedFromHand(Hand hand) {
            audioShake.Stop();
        }
    }
}