using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkArea : MonoBehaviour {
	//---fields----
	[SerializeField]
	private RabbitJob _theAreaJob;
	private Rect _rect;

	//---propaties---
	public Rect AreaRect { 
		get{
			if(_rect == null) _rect =  new Rect(transform.position.x, transform.position.z, transform.localScale.x, transform.localScale.z);
			return _rect;
		}
	}

	public RabbitJob TheAreaJob{ get{ return _theAreaJob; }}
}
