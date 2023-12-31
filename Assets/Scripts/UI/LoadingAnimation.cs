using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField] private float delay;
    private void OnEnable()
    {
        transform.localScale = Vector3.one;
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, new Vector3(0.3f, 0.3f, 0.3f), 0.5f)
            .setDelay(delay)
            .setEase(LeanTweenType.easeInOutCirc)
            .setLoopPingPong();
    }
}
