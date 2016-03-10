using UnityEngine;
using System.Collections;

public class Processor : MonoBehaviour {
	public float Basic(LifeCell cell, float sum) {
		float nextstate;
		if (cell.isLive) {
			if (2 <= sum && sum <= 3) {
				nextstate = 1f;
				cell.isLive = true;
			} else {
				nextstate = 0f;
				cell.isLive = false;
			}
		} else {
			if (3 == sum) {
				nextstate = 1f;
				cell.isLive = true;
			} else {
				nextstate = 0f;
				cell.isLive = false;
			}
		}
		return nextstate;
	}

	public float VichniacVote(LifeCell cell, float sum) {
		float nextstate;
		if (cell.isLive) {
			sum++;
		}
		if (sum <= 4) {
			cell.isLive = false;
		} else {
			cell.isLive = true;
		}
		if (sum == 4 || sum == 5) {
			cell.isLive = !cell.isLive;
		}
		nextstate = cell.isLive ? 1f : 0f;
		return nextstate;
	}

	public float RandomBinary(LifeCell cell, float sum) {
		float nextstate;
		cell.isLive = UnityEngine.Random.value > 0.5f;
		nextstate = cell.isLive ? 1f : 0f;
		return nextstate;
	}

	public float Continuous(LifeCell cell, float sum) {
		float nextstate;
		float avg = sum / 8f;
		if (avg == 1f) {
			nextstate = 0f;
			cell.isLive = false;
		} else if (avg == 0f) {
			nextstate = 1f;
			cell.isLive = true;
		} else {
			nextstate = cell.state + avg - cell.preState;
			cell.isLive = nextstate > 0.5f;
		}
		nextstate = Mathf.Clamp(nextstate, 0f, 1f);
		return nextstate;
	}
}
