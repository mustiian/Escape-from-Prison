using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItemUI : MonoBehaviour
{
    public Image CrosshairImage;

    public Sprite DefaultSprite;

    public Sprite ChangedSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseEnter()
    {
        CrosshairImage.sprite = ChangedSprite;
    }

    private void OnMouseExit()
    {
        CrosshairImage.sprite = DefaultSprite;
    }
}
