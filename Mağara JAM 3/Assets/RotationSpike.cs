using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSpike : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 360f * Time.deltaTime));
    }
}
