using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class CameraController : MonoBehaviour
{


    float mainSpeed = 100.0f; //regular speed
    float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 1000.0f; //Maximum speed when holdin gshift
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

        /*
        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse = Input.mousePosition;*/

        //Mouse  camera angle done.  

        //Keyboard commands
        /*
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }

        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;
        if (Input.GetKey(KeyCode.Space))
        { //If player wants to move on X and Z axis only
            transform.Translate(p);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
        else
        {
            transform.Translate(p);
        }*/

    }

    /*
    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }*/


}

public class Win32
{
    [DllImport("User32.Dll")]
    public static extern long SetCursorPos(int x, int y);
    /*
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }*/
}
