using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	
	public AudioClip[] sounds;
	
	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().clip=sounds[Random.Range(0,sounds.Length)];
		GetComponent<AudioSource>().Play();
	}
	
	
}
