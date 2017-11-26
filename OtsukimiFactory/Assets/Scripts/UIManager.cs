using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	[SerializeField]
	private Text _scoreText;
	[SerializeField]
	private Text _timeText;

	private GameManager _gameManager;

	// Use this for initialization
	void Start () {
		_gameManager = GameObject.FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		_scoreText.text = "Score : " + (int)_gameManager.Score;
		_timeText.text = "Time : " + (int)_gameManager.GameTime;
	}
}
