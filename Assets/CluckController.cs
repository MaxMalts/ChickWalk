using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;



public class CluckController : MonoBehaviour {

	AudioSource audioSource;


	public void OnCluck(InputAction.CallbackContext context) {
		if (context.phase == InputActionPhase.Performed) {
			if (audioSource != null) {
				audioSource.Play();
			}
		}
	}


	void Awake() {
		audioSource = GetComponent<AudioSource>();
		Assert.IsNotNull(audioSource, "No AudioSource on cluck GameObject.");
	}
}
