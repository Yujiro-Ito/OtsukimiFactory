using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkArea : MonoBehaviour {
	//---fields----
	[SerializeField]
	private RabbitJob _theAreaJob;

	//---propaties---
	public Rect AreaRect { 
		get{
			return new Rect(transform.position.x, transform.position.z, transform.localScale.x, transform.localScale.z);
		}
	}

	public RabbitJob TheAreaJob{ get{ return _theAreaJob; }}
}
