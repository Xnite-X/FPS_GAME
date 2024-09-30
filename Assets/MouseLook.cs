using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseY = 1,
        MouseX = 2,
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHorz = 1.0f;
    public float sensitivityVert = 1.0f; // Nama variabel diperbaiki

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float verticalRotate = 0;

    private void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }

    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            // Pergerakan horizontal
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHorz, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            // Pergerakan vertikal
            verticalRotate += Input.GetAxis("Mouse Y") * sensitivityVert; // Tanda diubah dari -= menjadi +=
            verticalRotate = Mathf.Clamp(verticalRotate, minimumVert, maximumVert);

            float horizontalRotate = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(verticalRotate, horizontalRotate, 0);
        }
        else
        {
            // Pergerakan horizontal dan vertikal
            verticalRotate += Input.GetAxis("Mouse Y") * sensitivityVert; // Tanda diubah dari -= menjadi +=
            verticalRotate = Mathf.Clamp(verticalRotate, minimumVert, maximumVert);

            float delta = Input.GetAxis("Mouse X") * sensitivityHorz;
            float horizontalRotate = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(verticalRotate, horizontalRotate, 0);
        }
    }
}
