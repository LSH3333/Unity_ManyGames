using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventManager : MonoBehaviour
{
    public AudioSource hoverSnd;
    public AudioSource clickSnd;

    // cursor texture 
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public void HoverSnd()
    {
        hoverSnd.Play();
    }

    public void ClickSnd()
    {
        clickSnd.Play();
    }

    // cursor texture 변경 
    public void ChangePointerTexture()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    public void ReturnPointerTexture()
    {
        Cursor.SetCursor(null, hotSpot, cursorMode);
    }
}
