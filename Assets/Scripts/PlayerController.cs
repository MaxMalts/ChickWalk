using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Assertions;



public class PlayerController : MonoBehaviour {

	public float walkForceCoefficient = 100;
	public float maxSpeed = 10;
	public float rotationCoefficient = 0.5f;
	public float jumpForce = 10;

	const string jumpStateName = "Jump In Place";

	[SerializeField] new Camera camera;

	new Rigidbody rigidbody;
	Animator animator;
	int jumpStateNameHash;

	bool isWalking = false;
	Vector3 walkDirection;
	bool canJump = true;

	Vector3 camRotation;
	Vector3 camOffset;


	public void OnMove(InputAction.CallbackContext context) {
		switch (context.phase) {
			case InputActionPhase.Performed:
				//Debug.Log(context.control);

				isWalking = true;

				walkDirection = context.action.ReadValue<Vector2>();
				//Debug.Log(walkDirection);
				walkDirection.z = walkDirection.y;
				walkDirection.y = 0;

				animator.SetBool("Walk", true);
				break;

			default:
				isWalking = false;
				animator.SetBool("Walk", false);
				break;
		}
	}


	public void OnRotate(InputAction.CallbackContext context) {
		const float topRotationX = 80;
		const float bottomRotationX = -20;

		if (context.phase == InputActionPhase.Performed) {
			Vector2 delta = context.ReadValue<Vector2>();
			//Debug.Log(delta);
			delta *= rotationCoefficient;
			delta.y = -delta.y;

			float futureRotationX = camRotation.x + delta.y;

			if (futureRotationX > topRotationX) {
				delta.y = topRotationX - camRotation.x;

			} else if (futureRotationX < bottomRotationX) {
				delta.y = bottomRotationX - camRotation.x;
			}

			camRotation.x += delta.y;
			camera.transform.RotateAround(transform.position, camera.transform.right, delta.y);
			camera.transform.RotateAround(transform.position, Vector3.up, delta.x);
		}
	}

	
	public void OnJump(InputAction.CallbackContext context) {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
		InputActionPhase neededPhase = InputActionPhase.Started;
#else
		InputActionPhase neededPhase = InputActionPhase.Performed;
#endif

		Debug.Log(context.phase);
		if (context.phase == neededPhase && canJump &&
			(animator.GetCurrentAnimatorStateInfo(0).shortNameHash != jumpStateNameHash ||
			animator.IsInTransition(0))) {

			rigidbody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
			canJump = false;
			animator.SetTrigger("Jump");
		}
	}


	void OnCollisionEnter(Collision collistionInfo) {
		canJump = true;
	}


	void Awake() {
		Assert.IsNotNull(camera, "Camera was not assigned in inspector.");
		//Assert.IsNotNull(inputActions, "Input Actions was not assigned in inspector.");

		rigidbody = GetComponent<Rigidbody>();
		Assert.IsNotNull(rigidbody, "No Rigidbody component on player.");

		animator = GetComponent<Animator>();
		Assert.IsNotNull(animator, "No Animator component on player.");

		jumpStateNameHash = Animator.StringToHash(jumpStateName);
		
		//pointerPosAction = inputActions.FindAction("UI/Point");

		camRotation = camera.transform.rotation.eulerAngles;
		camOffset = camera.transform.position - transform.position;

		// Auto Switch setting is ignored >:(
		GetComponent<PlayerInput>().neverAutoSwitchControlSchemes = true;
	}


	void Update() {
		camera.transform.position = transform.position + camera.transform.rotation * camOffset;

		Debug.Log(animator.GetCurrentAnimatorStateInfo(0).shortNameHash == jumpStateNameHash);
	}


	void FixedUpdate() {
		if (isWalking) {
			transform.rotation =
				Quaternion.LookRotation(new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z));

			if (new Vector2(rigidbody.velocity.x, rigidbody.velocity.z).magnitude < maxSpeed) {
				rigidbody.AddRelativeForce(walkDirection * walkForceCoefficient);
				rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
			}
		}
	}
}
