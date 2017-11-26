using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
	private NavMeshAgent _agent;
	private Vector3 _originalPos;

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
		_agent = GetComponent<NavMeshAgent>();
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
				ResetDestination();
				_work = true;
				break;
			case RabbitState.Catch:
				_currentState = AnimationState.Catch;
				ResetDestination();
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
			_currentState = AnimationState.Walk;
			SetAnimation();
			Debug.Log(_originalPos);
			_originalPos.y = transform.position.y;
			_agent.SetDestination(_originalPos);
			_break = false;
		}
	}

	private void ResetDestination(){
		_agent.ResetPath();
	}


}
