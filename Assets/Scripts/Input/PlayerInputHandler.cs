using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    //==============================================//

    PlayerEventHandler playerEventHandler;

    //==============================================//

    // Start is called before the first frame update
    void Start()
    {
        playerEventHandler = new PlayerEventHandler();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerEventHandler.InvokeMouseClickedEvent(MouseButton.Left);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            playerEventHandler.InvokeMouseUnClickedEvent(MouseButton.Left);
        }

    }
}
