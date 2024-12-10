using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public int row;
    public int column;

    public void Initialize(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    private void OnMouseDown()
    {
        GameManager.Instance.OnColumnSelected(column);
    }
}
