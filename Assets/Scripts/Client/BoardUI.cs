using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardUI : MonoBehaviour
{
    [SerializeField] GameObject boardCellPrefab;

    private Dictionary<int, CellUI> cells = new Dictionary<int, CellUI>();

    void Start()
    {
        ResetBoard();
    }

    private void ResetBoard()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0));
        }

        for (int i = 0; i < 9; i++) 
        {
            CellUI cell = Instantiate(boardCellPrefab, transform).GetComponent<CellUI>();
            cell.Initialize((byte)i);
            cells.Add(i, cell);
        }
    }
}
