using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Simple2dControler : MonoBehaviour {
	public float walkSpeed = 5.0f;
	public float runSpeed = 9.0f;
	private Animator animator;

	// Start is called before the first frame update
	void Start() {
		animator = this.GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update() {
		//Possible values for the variable Direction are:
		//   0: Idle
		//   1: Walk South
		//   2: Walk West
		//   3: Walk North
		//   4: Walk East
		animator.SetInteger("Direction", 0);
		float speed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? runSpeed : walkSpeed;
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			transform.position += Vector3.up * -speed * Time.deltaTime;
			animator.SetInteger("Direction", 1);
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
			animator.SetInteger("Direction", 4);
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.right * -speed * Time.deltaTime;
			animator.SetInteger("Direction", 2);
		}
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			transform.position += Vector3.up * speed * Time.deltaTime;
			animator.SetInteger("Direction", 3);
		}
	}
}
