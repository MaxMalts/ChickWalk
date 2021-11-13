using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;



public class GameInitializer : MonoBehaviour {

	[SerializeField] GameObject canvas;
	[SerializeField] GameObject touchscreenLooker;
	[SerializeField] GameObject walkScreenButtons;
	[SerializeField] GameObject cluckButton;


	void Awake() {
		Assert.IsNotNull(canvas, "Canvas was not assigned in inspector.");
		Assert.IsNotNull(touchscreenLooker, "Touchscreen Looker prefab was not assigned in inspector.");
		Assert.IsNotNull(walkScreenButtons, "Walk Screen Buttons prefab was not assigned in inspector.");
		Assert.IsNotNull(cluckButton, "Cluck Button prefab was not assigned in inspector.");

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
		Instantiate(touchscreenLooker, canvas.transform);
		Instantiate(walkScreenButtons, canvas.transform);
		Instantiate(cluckButton, canvas.transform);
#endif
	}
}
