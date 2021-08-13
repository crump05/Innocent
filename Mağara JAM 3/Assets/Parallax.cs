using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;

    public GameObject cam;

    public float parallaxEffect;

    private void Awake()
    {
        startPos = transform.position.x;
        cam = Camera.main.gameObject;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update()
    {
        float distX = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * parallaxEffect);
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        transform.position = new Vector3(startPos + distX, distY, transform.position.z);
        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
