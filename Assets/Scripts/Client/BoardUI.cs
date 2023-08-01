using System;
using System.Collections;
using System.Collections.Generic;
using TTT.Server.NetworkShared.Packets.ServerClient;
using UnityEngine;

public class BoardUI : MonoBehaviour
{
    [SerializeField] GameObject boardCellPrefab;

    private Dictionary<int, CellUI> cells = new Dictionary<int, CellUI>();

    void Start()
    {
        ResetBoard();
        OnMarkCellHandler.OnMarkCell += UpdateBoard;
    }
    private void OnDisable()
    {
        OnMarkCellHandler.OnMarkCell -= UpdateBoard;
    }

    private void UpdateBoard(NetOnMarkCell msg)
    {
        cells[msg.Index].UpdateUI(msg.Actor);
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
