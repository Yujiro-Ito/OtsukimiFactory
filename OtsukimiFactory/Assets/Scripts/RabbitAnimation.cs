using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class RabbitAnimation : MonoBehaviour {
	//---enum---
	private enum AnimationState{
		Break = 0,
		Work = 1,
		Catch = 2,
		Walk = 3
	}
	//---fields---
	private Animator _myAnimator;
	private AnimationState _currentState;
	private RabbitWork _myRabbitWork;
	private RabbitState _oldState;
	private bool _work;
	private bool _break;
	private Vector3 _originalPos;
	private IEnumerator _moveDestination;

	//---methods---
	// Use this for initialization
	void Start () {
		//---initialize variables---
		_myAnimator = GetComponent<Animator>();
		_currentState = AnimationState.Break;
		_myRabbitWork = GetComponent<RabbitWork>();
		_originalPos = transform.position;
		SetAnimation();
		_work = false;
		_break = true;
	}
	
	// Update is called once per frame
	void Update () {
		CheckJobState();
	}

	//労働状態の更新を監視
	public void CheckJobState(){
		//労働状態に更新があれば、アニメーションを変更
		if(_myRabbitWork.CurrentState != _oldState){
			ChangeAnimation(_myRabbitWork.CurrentState);	//アニメーションの変更
			_oldState = _myRabbitWork.CurrentState;
		}
	}

	//アニメーションを変更
	private void ChangeAnimation(RabbitState state){
		_work = false;
		_break = false;
		//アニメーションの種類を決定
		switch(state){
			case RabbitState.Break:
				//_currentState = AnimationState.Break;
				_break = true;
				break;
			case RabbitState.Work:
				_currentState = AnimationState.Work;
				_work = true;
				break;
			case RabbitState.Catch:
				_currentState = AnimationState.Catch;
				break;
		}
		SetAnimation();
	}

	//アニメーションの値をセット
	private void SetAnimation(){
		_myAnimator.SetInteger("State", (int)_currentState);
	}

	private void OnCollisionEnter(Collision col){
		if(col.transform.tag == "Floor" && _break){
			Debug.Log("当たった！");
			//歩くアニメーションに変更
			_currentState = AnimationState.Walk;
			SetAnimation();
			//移動処理の実行
			if(_moveDestination != null) _moveDestination.Reset();
			_moveDestination = MoveDestination(_originalPos, () => {
				//アニメーションの終了コールバック
				_currentState = AnimationState.Break;
				SetAnimation();
			});
			StartCoroutine(_moveDestination);
			_break = false;
		}
	}

	//元の位置に戻るための処理
	private IEnumerator MoveDestination(Vector3 destination, Action callback = null){
		//初期設定
		destination.y = transform.position.y;
		Vector3 moveDir = Vector3.Normalize(destination - transform.position);

		//まずは回転
		float t = 0;
		float speed = 1f;
		Quaternion from = transform.rotation;
		Quaternion to = Quaternion.LookRotation(destination - transform.position);
		while(t < 1){
			t += Time.deltaTime * speed;
			transform.rotation = Quaternion.Slerp(from, to, t);
			yield return new WaitForEndOfFrame();
		}

		//次に移動
		while(Vector3.Magnitude(destination - transform.position) < 0.05f){
			transform.Translate(moveDir);
			moveDir = Vector3.Normalize(destination - transform.position);
		}

		//コールバックの呼び出し
		if(callback != null) callback();
		Debug.Log("着いた！");
	}


}
