using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThanhLua2 : MonoBehaviour
{
    private float rotZ;
    public float RotationSpeed;
    public bool ClockWiseRotation;

    //Update thanh lửa quay theo chiều ngược chiều kim đồng hồ
    void Update()
    {
        if (ClockWiseRotation == false)
        {
            rotZ += Time.deltaTime * RotationSpeed;
        }
        else
        {
            rotZ += -Time.deltaTime * RotationSpeed;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

}
