using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorToTextureHelper
{

    //This is not working

    public static Sprite ColorToSpriteConverter(Color color)
    {
        int spriteWidth = 50;
        int spriteHeight = 50;

        Texture2D sampleTexture = Texture2D.whiteTexture;
        Sprite spriteColor;

        Texture2D sampleTexture2 = new Texture2D(spriteWidth, spriteHeight);
        sampleTexture2.SetPixels(ReturnDesiredColorArray(color, spriteWidth * spriteHeight));
        sampleTexture2.Apply();

        Color[] sampleTextureColor = sampleTexture2.GetPixels();

        for (int i = 0; i < sampleTextureColor.Length; i++)
        {
            sampleTextureColor[i] = color;
        }

        sampleTexture2.SetPixels(sampleTextureColor);

        Rect spriteRect = new Rect(0f, 0f, sampleTexture2.width, sampleTexture2.height);

        spriteColor = Sprite.Create(sampleTexture2, spriteRect , new Vector2(0.5f , 0.5f));

        return spriteColor;
    }

    public static Color[] ReturnDesiredColorArray(Color color , int length)
    {
        Color[] colorArray = new Color[length];

        for (int i = 0; i < length; i++)
        {
            colorArray[i] = color;
        }

        return colorArray;
    }

}
