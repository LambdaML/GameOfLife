using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class GoLController : MonoBehaviour {
	public enum GoLRule {
		Basic,
		Continuous,
		RandomBinary,
		VichniacVote
	}

	public GameObject cellPrefab;
	public int row;
	public int col;
	public float margin;
	internal LifeCell[,] cells;
	public float span;
	public float duration;
	public bool continuous = false;

	public GoLRule rule = GoLRule.Basic;
	private Initializer initializer = new Initializer();
	private Processor processor = new Processor();

	public Initializer.Pattern pattern;

	// Singleton
	private static GoLController _instance;
	public static GoLController instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<GoLController> ();
			}
			return _instance;
		}
	}

	void Start ()
	{
		Prepare();
		CreateCells();

		Invoke("ChangeState", span);
	}

	internal virtual void Prepare()
	{
	}

	internal virtual void CreateCells()
	{
		initializer.Prepare(pattern, col, row);

		cells = new LifeCell[col, row];
		float offsetX = margin * 0.5f * (float)-col;
		float offsetY = margin * 0.5f * (float)-row;

		for (int x = 0; x < col; x++) {
			for (int y = 0; y < row; y++) {
				GameObject cell = Instantiate(cellPrefab);
				cell.transform.parent = transform;
				cell.transform.localPosition = new Vector3(offsetX + margin * (float)x, 0f, offsetY + margin * (float)y);
				LifeCell lifeCell = cell.GetComponent<LifeCell>();
				lifeCell.y = y;
				lifeCell.x = x;
				cells[x,y] = lifeCell;

				switch(GoLController.instance.rule) {
				case GoLController.GoLRule.Basic:
				case GoLController.GoLRule.RandomBinary:
				case GoLController.GoLRule.VichniacVote:
					bool isLive = this.initializer.IsLive(x, y);
					lifeCell.Init(isLive, isLive ? 1.0f : 0f);
					break;

				case GoLController.GoLRule.Continuous:
					lifeCell.Init(true, UnityEngine.Random.value);
					break;
				}
				SetUpCell(lifeCell);

				lifeCell.UpdateState(lifeCell.state);
			}
		}
	}

	internal virtual void SetUpCell(LifeCell cell)
	{
	}

	internal virtual void ChangeState()
	{
		for (int x = 0; x < col; x++) {
			for (int y = 0; y < row; y++) {
				LifeCell cell = cells[x,y];

				float sum = 0f;
				for (int dx = -1; dx <= 1; dx++) {
					for (int dy = -1; dy <= 1; dy++) {
						if (dx == 0 && dy == 0) {
							continue;
						}
						LifeCell c = getCellByIndex(x + dx, y + dy);

						switch(rule) {
						case GoLRule.VichniacVote:
							sum += c.isLive ? 1 : 0;
							break;
						default:
							sum += c.state;
							break;
						}
					}
				}

				MethodInfo method = processor.GetType().GetMethod(rule.ToString());
				if (method == null) {
					Debug.LogWarning(string.Format("undefined method: {0}", rule.ToString()));
					return;
				}
				float nextstate = (float)method.Invoke(processor, new object[]{cell, sum});
				cell.nextState = nextstate;

				if (continuous || rule == GoLRule.Continuous) {
					cell.ChangeValue(cell.state, cell.nextState);
				} else {
					cell.UpdateState(cell.nextState);
				}
			}
		}

		for (int x = 0; x < col; x++) {
			for (int y = 0; y < row; y++) {
				LifeCell cell = cells[x,y];
				cell.preState = cell.state;
				cell.state = cell.nextState;
			}
		}

		Invoke("ChangeState", span);
	}

	public int GetIndexFromPosition(int x, int y)
	{
		return x + col * y;
	}

	public LifeCell getCellByIndex(int x, int y)
	{
		if (x < 0) {
			x = col + x;
		}
		if (x >= col) {
			x -= col;
		}
		if (y < 0) {
			y = row + y;
		}
		if (y >= row) {
			y -= row; 
		}
		return cells[x,y];
	}
}