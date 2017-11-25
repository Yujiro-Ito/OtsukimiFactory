using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	//---consts---
	public const float TIME_MAX = 60f;

	//---fields---
	private WorkArea[] _workAreas;
	private float _time;
	private int _score;

	//---propaties---
	public WorkArea[] WorkAreas{ get{ return _workAreas; }}

	// Use this for initialization
	void Start () {
		_time = TIME_MAX;
		_score = 0;
		_workAreas = GameObject.FindObjectsOfType<WorkArea>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
