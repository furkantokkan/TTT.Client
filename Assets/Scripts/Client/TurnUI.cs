using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnUI : MonoBehaviour
{
    [SerializeField] Color xColor;
    [SerializeField] Color oColor;
    [SerializeField] private TextMeshProUGUI playerText;

    Color clientColor;
    Color opponentColor;

    private void OnEnable()
    {
        if (GameManager.instance.GetClientType == TTT.Server.NetworkShared.Models.MarkType.X)
        {
            clientColor = xColor;
            opponentColor = oColor;
        }
        else 
        {
            clientColor = oColor;
            opponentColor = xColor;
        }

        if (GameManager.instance.IsClientTurn)
        {
            playerText.text = "your";
            playerText.color = clientColor;
            var rt = playerText.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(250, rt.sizeDelta.y);
        }
        else
        {
            playerText.text = "opponent";
            playerText.color = opponentColor;
            var rt = playerText.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(495, rt.sizeDelta.y);
        }

        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.3f)
            .setEase(LeanTweenType.easeOutBounce)
            .setOnComplete(() =>
            {
                LeanTween.scale(gameObject, new Vector3(1.0f, 1.0f, 1.0f), 0.3f)
                .setEase(LeanTweenType.easeOutBounce);
            });
    }
}
