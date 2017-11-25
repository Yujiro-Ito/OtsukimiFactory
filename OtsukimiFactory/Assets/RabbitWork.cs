using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class RabbitWork : MonoBehaviour {
	//---consts---
	//---fields---
	private RabbitData _myData;
	private Animator _myAnimator;
	private RabbitState _currentState;
	private int _currentPower;
	private GameObject _myInstance;
	private RabbitJob _currentJob;

	//---methods---
	// Use this for initialization
	void Start () {
		//変数の初期化
		_currentPower = _myData.Max_Power;
		_currentState = RabbitState.Break;
		_myAnimator = GetComponent<Animator>();
		_currentJob = RabbitJob.Box;
		//オブジェクトの生成
		_myInstance = (GameObject)Instantiate(_myData.Model, transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//休憩メソッド
	private void Break(){
		//休憩
		_currentPower = (_currentPower >= _myData.Max_Power) ? _myData.Max_Power : _currentPower + 1;
	}

	//労働用メソッド
	private void Work(){
		//体力の低下
		_currentPower = (_currentPower < 0) ? 0 : _currentPower - 1;
	}

	///<summary>
	///トリガーを離したときに呼び出すメソッド
	///</summary>
	public void ActionTrigger(Vector3 pos){
		Vector2 xzPosition = new Vector2(pos.x, pos.z);

		SetState(xzPosition);
	}

	//ウサギの状態を変更するメソッド
	private void SetState(Vector2 pos){
		_currentState = RabbitState.Break;
	}

	//ウサギの労働場所を変更する場所
	private void SetJob(Vector2 pos){
		_currentJob = RabbitJob.None;
	}

}

//ウサギのデータ
[System.Serializable]
public class RabbitData : ScriptableObject{
	[HeaderAttribute("ニックネーム")]
	public string nickName;
	[HeaderAttribute("適正能力")]
	public RabbitJob Propare;
	[HeaderAttribute("モデルプレファブ")]
	public GameObject Model;
	[HeaderAttribute("最大体力")]
	public int Max_Power;
}

//うさぎの動作状態
public enum RabbitState{
	Work,
	Break
}

//適正能力
public enum RabbitJob{
	Dumpling,
	Silver_Grass,
	Box,
	None
}
