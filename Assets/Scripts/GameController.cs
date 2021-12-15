using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {
	[SerializeField]
	private int totalScore;
	[SerializeField]
	private Text scoreText;

	public void UpdateScoreText(int incrementScore) {
		totalScore += incrementScore;
		scoreText.text = "Score: " + totalScore.ToString();
	}
}
