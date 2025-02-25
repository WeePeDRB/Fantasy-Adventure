using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void PrintMessage(string message)
    {
        Debug.Log("Người chơi bấm nút: " + message);
    }

}
