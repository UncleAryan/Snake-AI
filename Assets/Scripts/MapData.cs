using UnityEngine;

public class MapData : MonoBehaviour {
    public int row;
    public int col;

    public int[,] makeMap() {
        int[,] map = new int[row, col];

        for(int r = 0; r < row; r++) {
            for(int c = 0; c <  col; c++) {
                map[r, c] = (int)NodeState.OPEN;
            }
        }

        return map;
    }
}
