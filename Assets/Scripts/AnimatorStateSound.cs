using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimatorStateSound : StateMachineBehaviour {

	public AudioClip audioClip;
	public bool loop = true;

	[SerializeField]
	[Range(0.0f, 1.0f)]
	float volume = 1.0f;
	public float Volume {
		get {
			return volume;
		}

		set {
			if (value > 1.0f) {
				value = 1.0f;
			} else if (value < 0.0f) {
				value = 0.0f;
			}
			volume = value;
		}
	}

	//bool playedOneShot;


	// To-do:
	//[SerializeField]
	//[Min(0.0f)]
	//[Tooltip("Time in seconds to fade in audio volume.")]
	//float fadeIn = 0.0f;
	//public float FadeIn {
	//	get {
	//		return volume;
	//	}

	//	set {
	//		volume = (value < 0.0f ? 0.0f : value);
	//	}
	//}


	//[SerializeField]
	//[Min(0.0f)]
	//[Tooltip("Time in seconds to fade out audio volume.")]
	//float fadeOut = 0.0f;
	//public float FadeOut {
	//	get {
	//		return volume;
	//	}

	//	set {
	//		volume = (value < 0.0f ? 0.0f : value);
	//	}
	//}


	override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo,
		int layerIndex) {

		AudioSource audioSource = animator.GetComponent<AudioSource>();
		if (audioSource != null) {
			// I need independent volume for all clips.
			//PlayOneShot depends on AudioSource volume.
			//if (!loop) {
			//	audioSource.PlayOneShot(audioClip);
			//	playedOneShot = true;
			//} else {

			audioSource.clip = audioClip;
			audioSource.loop = loop;
			audioSource.volume = Volume;
			audioSource.Play();
			//playedOneShot = false;

			//}
		}
	}


	override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo,
		int layerIndex) { 

		AudioSource audioSource = animator.GetComponent<AudioSource>();
		//if (audioSource != null && audioSource.clip == audioClip && !playedOneShot) {
		if (audioSource != null && audioSource.clip == audioClip) {
			audioSource.Stop();
		}
	}
}