using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class CameraController : MonoBehaviour
{
    float camSens = 0.25f; //How sensitive it with mouse

    bool isSecondPass = false;
    //lastMouse is relative to the whole screen
    private Vector3 lastMouse;
    private bool isFirstTwoFrames = true;
    private bool isSecondFrame = false;

    //private float totalRun = 1.0f;

    void Start()
    {
        //Setting lastMouse so it is relative to the screen position

        Win32.SetCursorPos((int)255, (int)255);
        Cursor.visible = false;
        
    }
    
    void Update()
    {
        //On the first frame the mouse is set to the screen position
        //Input.mousePosition is only updated on the second frame
        if (isFirstTwoFrames)
        {
            if (isSecondFrame)
            {
                lastMouse = Input.mousePosition;
                isFirstTwoFrames = false;
            }
            else { isSecondFrame = true; }
        }
        

        
        Vector3 mouseVec = new Vector3();
        mouseVec = Input.mousePosition;
        Vector3 difMouse = mouseVec - lastMouse;
        Vector3 scaledAndCorrectedMouse = new Vector3(-difMouse.y * camSens, difMouse.x * camSens, 0);
        
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x + scaledAndCorrectedMouse.x, transform.eulerAngles.y + scaledAndCorrectedMouse.y, 0);
        
        Win32.SetCursorPos((int)255, (int)255);

        lastMouse = new Vector3(255, 255, 0);
    }
}

public class Win32
{
    [DllImport("User32.Dll")]
    public static extern long SetCursorPos(int x, int y);

}
