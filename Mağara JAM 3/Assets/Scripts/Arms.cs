using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{

    private Vector2 lookDir;

    private HaloProjectile halo;
    public bool canShoot;

    public GameObject haloVisual;
    public GameObject crosshair;
    private void Awake()
    {
        halo = GetComponentInChildren<HaloProjectile>();
        haloVisual = transform.GetChild(1).gameObject;
        crosshair = GameObject.Find("Crosshair");
    }

    private void Start()
    {
        Cursor.visible = false;
        halo.gameObject.transform.SetParent(null);
        halo.gameObject.SetActive(false);
    }

    private void Update()
    {
        crosshair.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        if (!canShoot)
            return;
        lookDir = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg-90f;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));


        if (Input.GetMouseButtonDown(0) && !halo.move && canShoot)
        {
            halo.gameObject.SetActive(true);
            haloVisual.SetActive(false);
            halo.RotateOnStart(angle);
        }
        



    }
}
