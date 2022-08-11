using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeUI : MonoBehaviour
{

    private bool isFaded = false;
    [SerializeField] private CanvasGroup canvasGroup;

    public void Fader()
    {
        isFaded = !isFaded;

        if (isFaded)
        {
            canvasGroup.DOFade(0, 1).OnComplete(() => {
                Debug.Log("canvasGroup.DOFade(0, 2)");
            });
        }
        else
        {
            canvasGroup.DOFade(1, 1).OnComplete(() => {
                Debug.Log("canvasGroup.DOFade(1, 2)");
            });
        }
    }

}
