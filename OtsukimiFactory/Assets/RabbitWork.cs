using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class RabbitWork : MonoBehaviour {
	//---consts---
	//---fields---
	[SerializeField]
	private RabbitData _myData;
	private Animator _myAnimator;
	private RabbitState _currentState;
	private float _currentPower;
	private RabbitJob _currentJob;
	private GameManager _gameManager;
	private Action<float> _powerAction;
	private float _waitTime;
	private float _powerRate;
	
	//---propaties---
	public RabbitState CurrentState { get{ return _currentState; } set{ _currentState = value; }}
	public RabbitJob CurrentJob{ get{ return _currentJob; } set{ _currentJob = value; }}

	//---methods---
	// Use this for initialization
	void Start () {
		//変数の初期化
		_currentPower = _myData.Max_Power;
		_currentState = RabbitState.Break;
		_myAnimator = GetComponent<Animator>();
		_currentJob = RabbitJob.Box;
		_gameManager = GameObject.FindObjectOfType<GameManager>();
		_powerAction = (x) => {};
		_powerRate = 1;
		_waitTime = 1;
		//体力増減コルーチンの発動
		StartCoroutine(_powerCoroutine());
	}
	
	// Update is called once per frame
	void Update () {
		//状態によって行動の変更
		switch(_currentState){
			case RabbitState.Break: Break(); break;
			case RabbitState.Catch: break;
			case RabbitState.Work: Work(); break;
		}
	}

	//一秒毎に体力の計算を行うコルーチン
	private IEnumerator _powerCoroutine(){
		while(true){
			_powerAction(_powerRate);
			yield return new WaitForSeconds(_waitTime);
		}
	}

	//休憩メソッド
	private void Break(){
		//休憩
		_powerAction = (x) => {
			_currentPower = (_currentPower >= _myData.Max_Power) ? _myData.Max_Power : _currentPower + x;
		};
	}

	//労働用メソッド
	private void Work(){
		//体力の低下メソッドの登録
		_powerAction = (x) => {
			_currentPower = (_currentPower < 0) ? 0 : _currentPower - x;
		};
	}

	///<summary>
	///ウサギをキャッチした時に呼び出してね
	///</summary>
	public void Caught(){
		_currentState = RabbitState.Catch;
		_powerAction = (x) => {};
	}

	///<summary>
	///トリガーを離したときに呼び出すメソッド
	///</summary>
	public void ActionTrigger(Vector3 pos){
		Vector2 xzPosition = new Vector2(pos.x, pos.z);
		//エリア情報から労働状態と職種を設定
		WorkArea area = GetWorkArea(xzPosition);
		SetState(area);
		SetJob(area);
	}

	//ウサギの状態を変更するメソッド
	private void SetState(WorkArea area){
		_currentState = (area == null) ? RabbitState.Break : RabbitState.Work;
	}

	//ウサギの労働場所を変更する場所
	private void SetJob(WorkArea area){
		_currentJob = (area == null) ? RabbitJob.None : area.TheAreaJob;
	}

	//ワークエリアの選出
	private WorkArea GetWorkArea(Vector2 pos){
		WorkArea result = null;
		//位置から参照する
		Rect tmp;
		foreach(WorkArea area in _gameManager.WorkAreas){
			tmp = area.AreaRect;
			//エリア内にウサギがいたらそのエリアを返却
			if(tmp.x + tmp.width / 2 > pos.x && pos.x > tmp.x - tmp.width / 2){
				if(tmp.y + tmp.height / 2 > pos.y && pos.y > tmp.y - tmp.height / 2){
					result = area;
					break;
				}
			}
		}
		return result;
	}

}

//ウサギのデータ
[System.Serializable]
public class RabbitData : ScriptableObject{
	[HeaderAttribute("ニックネーム")]
	public string nickName;
	[HeaderAttribute("適正能力")]
	public RabbitJob Propare;
	[HeaderAttribute("最大体力")]
	public int Max_Power;
}

//うさぎの動作状態
public enum RabbitState{
	Work,
	Break,
	Catch
}

//適正能力
public enum RabbitJob{
	Dumpling,
	Silver_Grass,
	Box,
	None
}
