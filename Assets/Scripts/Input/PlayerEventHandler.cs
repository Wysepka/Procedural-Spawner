using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventHandler
{
    static System.Action<MouseButton> MouseClickedEvent;

    static System.Action<MouseButton> MouseUnClickedEvent;

    public PlayerEventHandler()
    {

    }

    #region Invoking Mouse Click Events

    public void InvokeMouseClickedEvent(MouseButton mouseButton)
    {
        if(MouseClickedEvent != null) MouseClickedEvent(mouseButton);
    }

    public void InvokeMouseUnClickedEvent(MouseButton mouseButton)
    {
        if(MouseUnClickedEvent != null) MouseUnClickedEvent(mouseButton);
    }

    #endregion

    #region Registering Methods

    #region MouseClicked Register Methods

    public static void RegisterToMouseClicked(System.Action<MouseButton> action)
    {
        MouseClickedEvent += action;
    }

    public static void UnRegisterToMouseClicked(System.Action<MouseButton> action)
    {
        MouseClickedEvent -= action;
    }

    #endregion

    #region MouseUnclicked Registering methods

    public static void RegisterToMouseUnClicked(System.Action<MouseButton> action)
    {
        MouseUnClickedEvent += action;
    }

    public static void UnRegisterToMouseUnClicked(System.Action<MouseButton> action)
    {
        MouseUnClickedEvent -= action;
    }

    #endregion


    #endregion
}
