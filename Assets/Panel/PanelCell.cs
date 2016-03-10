using UnityEngine;
using System.Collections;

public class PanelCell : LifeCell {
	override public void UpdateState(float value)
	{
		int index = GoLController.instance.GetIndexFromPosition(x, y);
		transform.localRotation = Quaternion.Euler(new Vector3(value * 180f, 0f, 0f));
	}
}
