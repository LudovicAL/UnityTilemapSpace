using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Simple2dControler : MonoBehaviour {
	[SerializeField]
	private float walkSpeed = 5.0f;
	[SerializeField]
	private float runSpeed = 9.0f;
	private Animator animator;
	private InputAction inputActionRun;
	private Vector2 m_Move;
	private CanvasBehavior canvasBehavior;

	// Start is called before the first frame update
	void Awake() {
		animator = this.GetComponent<Animator>();
		inputActionRun = this.GetComponent<PlayerInput>().currentActionMap.FindAction("Run");
		this.GetComponent<PlayerInput>().currentActionMap.FindAction("Menu").started += OnMenuStarted;
		this.GetComponent<PlayerInput>().currentActionMap.FindAction("Menu").canceled += OnMenuStarted;
	}

	private void Start() {
		canvasBehavior = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasBehavior>();
	}

	void Update() {
		Move(m_Move);
	}

	//When a move action is triggered with the keyboard or a controller
	public void OnMove(InputAction.CallbackContext context) {
		m_Move = context.ReadValue<Vector2>();
	}

	//When a menu action is triggered
	public void OnMenuStarted(InputAction.CallbackContext context) {
		if (canvasBehavior) {
			canvasBehavior.ShowAboutPanel(context.started);
		} else {
			Debug.Log("This Character's reference to the canvasBehavior component is null.");
		}
	}

	//Moves the character
	//Possible values for the animator are:
	//   0: Idle
	//   1: Walk South
	//   2: Walk West
	//   3: Walk North
	//   4: Walk East
	private void Move(Vector2 direction) {
		animator.SetInteger("Direction", 0);
		if (direction.magnitude > 0.01f) {
			float speed = inputActionRun.ReadValue<float>() > 0.01f ? runSpeed : walkSpeed;
			transform.position += new Vector3(direction.x, direction.y) * speed * Time.deltaTime;
			if (direction.y < -0.01) {
				animator.SetInteger("Direction", 1);
			}
			if (direction.x < -0.01) {
				animator.SetInteger("Direction", 2);
			}
			if (direction.x > 0.01) {
				animator.SetInteger("Direction", 4);
			}
			if (direction.y > 0.01) {
				animator.SetInteger("Direction", 3);
			}
		}
	}
}
