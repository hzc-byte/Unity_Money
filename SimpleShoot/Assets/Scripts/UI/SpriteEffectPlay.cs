
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RenderModel
{
    Image,
    SpriteRenderer,
}
public class SpriteEffectPlay : MonoBehaviour
{
    public string effectName = "loading/";
    public float changeTime = 0.1f;
    public int lastIndex = 0;
    public RenderModel renderModel;
    private Image image;
    private SpriteRenderer renderer;
    public static event Action SpriteEffectClose;

    private int currentIndex = 1;
    private float leastTime = 0.1f;

    void Awake()
    {
        currentIndex = 1;
       
        image = this.GetComponent<Image>();
        renderer = this.GetComponent<SpriteRenderer>();
    }
}
