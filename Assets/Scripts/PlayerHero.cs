using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHero : MonoBehaviour {
	private static readonly float LayerGround = 6;
	private static readonly float LayerFlag = 7;
	private static readonly float LayerSpikes = 8;

	[SerializeField]
	private float speedMove;
	[SerializeField]
	private float speedJump;

	[SerializeField]
	private string nextSceneName;

	private bool isJumping;
	private bool hasJump;
	private bool hasDoubleJump;

	private Rigidbody rigidBody;
	private float horizontalAxis;

	void Start() {
		rigidBody = GetComponent<Rigidbody>();
	}

	void Update() {
		CheckKeyboard();
	}

	void FixedUpdate() {
		MoveHorizontally();
		JumpAction();
	}

	void CheckKeyboard() {
		if (Input.GetButtonDown("Jump")) {
			isJumping = true;
		}

		horizontalAxis = Input.GetAxis("Horizontal");
	}

	void MoveHorizontally() {
		rigidBody.velocity = new Vector3(horizontalAxis * speedMove, rigidBody.velocity.y, 0);
	}

	void JumpAction() {
		if (isJumping && hasJump) {
			Debug.Log("IsJump");
			rigidBody.AddForce(Vector3.up * speedJump, ForceMode.Impulse);
			hasDoubleJump = true;
			hasJump = false;
		} else if (isJumping && hasDoubleJump) {
			Debug.Log("IsDoubleJump");
			rigidBody.AddForce(Vector3.up * speedJump * 0.5f, ForceMode.Impulse);
			hasDoubleJump = false;
		}
	}

	 void OnCollisionStay(Collision collision) {
		if (collision.gameObject.layer == LayerGround && isJumping) {
			Debug.Log("NotIsJump");
			isJumping = false;
			hasDoubleJump = false;
			hasJump = true;
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
