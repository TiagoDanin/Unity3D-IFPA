using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHero : MonoBehaviour {
	private static readonly float LayerGround = 6;
	private static readonly float LayerFlag = 7;
	private static readonly float LayerSpikes = 8;

	public float speedMove;
	public float speedJump;

	public string nextSceneName;

	private bool isJumping;
	private bool hasDoubleJump;

	private Rigidbody rigidBody;
	private float horizontalAxis;

	void Start() {
		rigidBody = GetComponent<Rigidbody>();
	}

	void Update() {
		CheckKeyboard();
	}

	void CheckKeyboard() {
		if (Input.GetButtonDown("Jump")) {
			JumpAction();
		}

		horizontalAxis = Input.GetAxis("Horizontal");
		MoveHorizontally();
	}

	void MoveHorizontally() {
		rigidBody.velocity = new Vector3(horizontalAxis * speedMove, rigidBody.velocity.y, 0);
	}

	void JumpAction() {
		if (!isJumping) {
			rigidBody.AddForce(Vector3.up * speedJump, ForceMode.Impulse);
			hasDoubleJump = true;
		} else if (hasDoubleJump) {
			rigidBody.AddForce(Vector3.up * speedJump * 1f, ForceMode.Impulse);
			hasDoubleJump = false;
		}
	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.layer == LayerGround) {
			Debug.Log("IsJump");
			isJumping = true;
		}
	}

	 void OnCollisionStay(Collision collision) {
		if (collision.gameObject.layer == LayerGround && isJumping) {
			Debug.Log("NotIsJump");
			isJumping = false;
			hasDoubleJump = false;
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.layer == LayerFlag) {
			NextLevel();
		} else if (collision.gameObject.layer == LayerSpikes) {
			ReloadLevel();
		}
	}

	void NextLevel() {
		SceneManager.LoadScene(nextSceneName);
	}

	void ReloadLevel () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
