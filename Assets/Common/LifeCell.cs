using UnityEngine;
using System.Collections;

public class LifeCell : MonoBehaviour {
	public int y;
	public int x;

	public float preState;
	public float state;
	public float nextState;

	public bool isLive;

	// Use this for initialization
	void Start () {
	}

	public void Init(bool live, float value) {
		isLive = live;
		preState = state = value;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeValue(float from, float val)
	{
		UpdateState(from);
		iTween.ValueTo(gameObject, iTween.Hash("from", from, "to", val, "time", GoLController.instance.duration, "onupdate", "UpdateState"));
	}

	virtual public void UpdateState(float value)
	{
	}
}
