using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPlayerScript : Singleton<MaterialPlayerScript>
{
    public int m_MaterialsGathered = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckForMaterialPC();
        }

        CheckForMaterialTouch();
    }

    public void CheckForMaterialTouch()
    {
        // Touch Controls
        if (Input.touchCount > 0)
        {
            // gets current mouse position
            // Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int latestTouch = Input.touches.Length - 1;

            Collider2D collider = Physics2D.OverlapPoint(new Vector2(Input.touches[latestTouch].position.x, Input.touches[latestTouch].position.y));

            if (collider == null)
            {
                return;
            }
            else if (collider.tag == "Material")
            {
                collider.GetComponent<MaterialScript>().DestroyMaterial();
            }
        }
    }

    public void CheckForMaterialPC()
    {
        // gets current mouse position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D collider = Physics2D.OverlapPoint(new Vector2(mouseWorldPos.x, mouseWorldPos.y));

        if (collider == null)
        {
            return;
        }
        else if (collider.tag == "Material")
        {
            collider.GetComponent<MaterialScript>().DestroyMaterial();
        }
    }
}
