using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scroll_Speed = 0.1f;
    private MeshRenderer m_Renderer;
    private float x_Scroll;
    void Awake()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Scroll();
    }

    private void Scroll()
    {
        x_Scroll = Time.time * scroll_Speed;
        Vector2 offset = new Vector2(x_Scroll, 0f);
        m_Renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
