using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public enum rotationAxes
    {
        MouseXAnyY = 0,
        MouseX = 1,
        MouseY = 2
    }
    //enum just to make setting the x and y angles easier, since they're tied to numerical values in unity editor

    public rotationAxes axes = rotationAxes.MouseXAnyY;
    public float sensitivityX = 9.0f;
    public float sensitivityY = 9.0f;
    //rotationAxes provides a visible variable in the editor and is tested in the if statements, set to 0 by default.
    //sensitivities are self-explanatory

    public float minimumY = -45.0f;
    public float maximumY = 45.0f;
    //minimum and maximum values for Mathf.Clamp to use.

    private float yRot = 0;
    //variable for vertical rotation angle

    public Transform orientation; //represents the Player's orientation -- MM 10/10/24

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.rotation = Quaternion.identity;
    }

    void LateUpdate()
    {
        if (axes == rotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        //This one simply rotates the camera horizontally if the mouse is moved as such.

        else if (axes == rotationAxes.MouseY)
        {
            yRot -= Input.GetAxis("Mouse Y") * sensitivityY;
            yRot = Mathf.Clamp(yRot, minimumY, maximumY);

            float xRot = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(yRot, xRot, 0);
        }
        //This reads a vertical input from the mouse, keeps it between two values, keeps horizontal rotation locked, and creates a new Vector3 for the x rotation's angle.
        else
        {
            yRot -= Input.GetAxis("Mouse Y") * sensitivityY;
            yRot = Mathf.Clamp(yRot, minimumY, maximumY);

            float delta = Input.GetAxis("Mouse X") * sensitivityX;
            float xRot = transform.localEulerAngles.y + delta;

            //transform.localEulerAngles = new Vector3(yRot, xRot, 0); //-- leaving this out for now becuase I think transform.rotation is smoother?? -- MM 10/10/24
            transform.rotation = Quaternion.Euler(yRot, xRot, 0); //Rotates the camera.

            orientation.rotation = Quaternion.Euler(0, xRot, 0); //Updates the player's orientation to match camera's rotation on the y axis. -- MM 10/10/24
        }
        //This essentially takes the same idea of the else if statement above, but instead of locking horizontal rotation,
        //it reads horizontal rotation to a new float and adds that rotation to the horizontal rotation.
    }
}
