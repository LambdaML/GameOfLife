using UnityEngine;
using System.Collections;
using System.Reflection;

public class Initializer {
	public enum Pattern {
		Random,
		Blinker,
		Octagon
	}

	public string Blinker(int row, int col) {
		return Padding("111", row - 1, col);
	}

	public string Octagon(int row, int col) {
		int _row = row - 3;
		int _col = col - 3;
		return Padding("00011", _row, _col) + Padding("001001", _row, 1) + Padding("0100001", _row, 1)
			+ Padding("10000001", _row, 1) + Padding("10000001", _row, 1)
			+ Padding("0100001", _row, 1) + Padding("001001", _row, 1) + Padding("00011", _row, 1);
	}

	public string Padding(string tmp, int row, int col) {
		tmp = tmp.PadLeft(tmp.Length + row, '0');
		tmp = tmp.PadLeft(tmp.Length + col, '\n');
		return tmp;
	}

	private string[] initRows;
	private Pattern _pattern;
	public void Prepare(Pattern pattern, int col, int row) {
		_pattern = pattern;

		if (pattern == Initializer.Pattern.Random) {
			return;
		}
		MethodInfo method = GetType().GetMethod(pattern.ToString());
		if (method == null) {
			Debug.LogWarning(string.Format("undefined pattern: {0}", pattern.ToString()));
			return;
		}
		string initizlizeState = (string)method.Invoke(this, new object[]{col/2, row/2});
		initRows = initizlizeState.Split('\n');
	}

	public bool IsLive(int x, int y) {
		if (_pattern == Initializer.Pattern.Random) {
			return Random.value > 0.5f;
		}
		string initCols = "";
		if (initRows.Length > y) {
			initCols = initRows[y];
		}
		if (initCols.Length > x) {
			return initCols.Substring(x, 1) == "1";
		} else {
			return false;
		}
	}
}
