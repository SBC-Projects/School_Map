using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class CameraController : MonoBehaviour
{
    float camSens = 0.25f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
                                                         
    void Update()
    {
        Vector3 mouseVec = new Vector3();
        mouseVec = Input.mousePosition;
        Vector3 difMouse = mouseVec - lastMouse;
        Vector3 scaledAndCorrectedMouse = new Vector3(-difMouse.y * camSens, difMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + scaledAndCorrectedMouse.x, transform.eulerAngles.y + scaledAndCorrectedMouse.y, 0);
        transform.eulerAngles = lastMouse;

        Win32.SetCursorPos((int)255, (int)255);
        lastMouse = new Vector3(255, 255, 0);
    }
}

public class Win32
{
    [DllImport("User32.Dll")]
    public static extern long SetCursorPos(int x, int y);

}
