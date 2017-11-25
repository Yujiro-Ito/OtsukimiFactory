using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RabbitAnimation : MonoBehaviour {
	//---enum---
	private enum AnimationState{
		Break = 0,
		Work = 1,
		Catch = 2
	}
	//---fields---
	private Animator _myAnimator;
	private AnimationState _currentState;
	private RabbitWork _myRabbitWork;
	private RabbitState _oldState;

	//---methods---
	// Use this for initialization
	void Start () {
		//---initialize variables---
		_myAnimator = GetComponent<Animator>();
		_currentState = AnimationState.Break;
		_myRabbitWork = GetComponent<RabbitWork>();
		SetAnimation();
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
		//アニメーションの種類を決定
		switch(state){
			case RabbitState.Break:
				_currentState = AnimationState.Break;
				break;
			case RabbitState.Work:
				_currentState = AnimationState.Work;
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


}
