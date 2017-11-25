using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	//---consts---
	public const float TIME_MAX = 60f;

	//---fields---
	private WorkArea[] _workAreas;
	private float _time;
	private float _score;
	private bool _finish;

	//---propaties---
	public WorkArea[] WorkAreas{ get{ return _workAreas; }}
	public bool Finish{ get{ return _finish; }}

	// Use this for initialization
	void Start () {
		_time = TIME_MAX;
		_score = 0;
		_finish = false;
		_workAreas = GameObject.FindObjectsOfType<WorkArea>();

		//コルーチンの再生
		StartCoroutine(TimeCounter());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//時間計算用コルーチン
	private IEnumerator TimeCounter(){
		while(_time > 1){
			yield return new WaitForSeconds(1);
			_time--;
		}
		_finish = true;
	}

	//スコアアップ
	public void ScoreUp(float score){
		_score += score;
	}
}
