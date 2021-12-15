using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	private static readonly string TagPlayer = "Player";

	[SerializeField]
	private int score;
	[SerializeField]
	private GameController gameController;
	
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider2D;

    void OnTriggerEnter(Collider collider) {
		Debug.Log("00000");
        if (collider.gameObject.tag == TagPlayer) {
			Debug.Log("11111");
            gameController.UpdateScoreText(score);
            Destroy(gameObject, 0f);
        }
    }
}
