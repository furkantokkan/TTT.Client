using System;
using System.Collections;
using System.Collections.Generic;
using TTT.Server.NetworkShared.Models;
using TTT.Server.NetworkShared.Packets.ServerClient;
using UnityEngine;
using UnityEngine.UI;

public class WinLineUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Color xColor;
    [SerializeField] Color oColor;
    [Header("Referances")]
    [SerializeField] Image lineImage;
    private RectTransform rt;
    private Dictionary<WinLineType, LineConfig> lineConfig;

    private void Start()
    {
        lineImage.enabled = false;
        rt = GetComponent<RectTransform>();
        OnMarkCellHandler.OnMarkCell += HandleMarkCell;
        lineConfig = InitLineConfigs();
    }

    private void OnDestroy()
    {
        OnMarkCellHandler.OnMarkCell -= HandleMarkCell;
    }

    private void HandleMarkCell(NetOnMarkCell msg)
    {
        if (msg.Outcome == MarkOutcome.Win)
        {
            lineImage.enabled = true;
            var config = lineConfig[msg.WinLineType];
            SetupLine(config, msg.Actor);
            StopCoroutine(AnimateLine());
            StartCoroutine(AnimateLine());
        }
    }

    private IEnumerator AnimateLine()
    {
        yield return new WaitForSeconds(0.5f);

        while (lineImage.fillAmount < 1f)
        {
            lineImage.fillAmount += 2f * Time.deltaTime;
            yield return null;
        }
    }

    private void SetupLine(LineConfig config, string actor)
    {
        Color color = Color.gray;

        if (!string.IsNullOrEmpty(actor))
        {
            var type = GameManager.instance.ActiveGame.GetPlayerType(actor);
            color = type == MarkType.X ? xColor : oColor;
        }

        rt.localPosition = new Vector2(config.X, config.Y);
        rt.localRotation = Quaternion.Euler(0, 0, config.ZRotation);
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, config.Height);
        lineImage.color = color;
    }

    private Dictionary<WinLineType, LineConfig> InitLineConfigs()
    {
        return new Dictionary<WinLineType, LineConfig>
    {
        { WinLineType.None, new LineConfig() },
        { WinLineType.Diagonal, new LineConfig() { Height = 985f,  ZRotation = 45f, X = 0f, Y = 0f } },
        { WinLineType.AntiDiagonal, new LineConfig() { Height = 985f,  ZRotation = -45f, X = 0f, Y = 0f } },
        { WinLineType.ColLeft, new LineConfig() { Height = 740f,  ZRotation = 0f, X = -270, Y = 0f } },
        { WinLineType.ColMid, new LineConfig() {  Height = 740f,  ZRotation = 0f, X = 0f, Y = 0f } },
        { WinLineType.ColRight, new LineConfig() {  Height = 740f,  ZRotation = 0f, X = 270, Y = 0f } },
        { WinLineType.RowTop, new LineConfig() {  Height = 740f,  ZRotation = 90f, X = 0, Y = 270f } },
        { WinLineType.RowMiddle, new LineConfig() {  Height = 740f,  ZRotation = 90f, X = 0, Y = 0f } },
        { WinLineType.RowBottom, new LineConfig() {  Height = 740f,  ZRotation = 90f, X = 0, Y = -270f } },
    };
    }
}

public class LineConfig
{
    public float X { get; set; }

    public float Y { get; set; }

    public float Height { get; set; }

    public float ZRotation { get; set; }
}