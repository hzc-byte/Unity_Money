using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public static class Extension
{
    /// <summary>
    /// 打印机一样的输出字
    /// </summary>
    /// <param name="text"></param>
    /// <param name="content"></param>
    /// <param name="speed"></param>
    public static IEnumerator Print(this Text text, string content, float speed, Action end = null)
    {
        foreach (char letter in content.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(speed);
        }
        if (end != null)
        {
            end();
        }
    }

    /// <summary>
    /// 图片淡出
    /// </summary>
    public static void PictureFadeOut(this MaskableGraphic image, float timer, Action end = null)
    {
        image.DOColor(new Color(image.color.r, image.color.g, image.color.b, 0), timer).OnComplete(delegate
        {
            if (end != null)
            {
                end();
            }
        });
    }
}
