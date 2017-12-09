using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienticSounds : MonoBehaviour {

    public AudioClip ambientMusic;
    private bool isAmbientPlaying = false;
    private bool ambientSFXready = false;
    public AudioClip[] ambientSFX;
    private int ambientSFXid = 0;
    private int repeatAmbientSFXtimer;


    // Use this for initialization
    void Start () {
        if (!isAmbientPlaying) {
            isAmbientPlaying = true;
            Camera.main.GetComponent<AudioSource>().PlayOneShot(ambientMusic);
            Invoke("RepeatAmbientMusic", 205);
            Invoke("RepeatAmbientSFX", 15);
            Invoke("RepeatBreathSFX", 5);
        }
        //start ambient sounds
        ////ambient sound every 15 seconds


    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isAmbientPlaying)
        {
            isAmbientPlaying = true;
            Camera.main.GetComponent<AudioSource>().PlayOneShot(ambientMusic);
            Invoke("RepeatAmbientMusic", 205);

        }
        
        if(ambientSFXready){
            ambientSFXready = false;
            repeatAmbientSFXtimer = Random.Range(10, 25);
            Invoke("RepeatAmbientSFX", repeatAmbientSFXtimer);

            ambientSFXid = Random.Range(0, ambientSFX.Length);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(ambientSFX[ambientSFXid]);
        }


    }

    void RepeatAmbientMusic()
    {
        isAmbientPlaying = false;
    }
    void RepeatAmbientSFX()
    {
        ambientSFXready = true;
    }
}
