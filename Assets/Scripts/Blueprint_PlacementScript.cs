using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint_PlacementScript : MonoBehaviour
{
    //Code is taken from this tutorial: https://youtu.be/Omu0A4Mk5pE

    RaycastHit hit;
    Vector3 movePoint;
    public GameObject RealObject;

    // Start is called before the first frame update
    void Start()
    {

        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 7)))
        {
            transform.position = hit.point;
        }
    }

    // Update is called once per frames
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 7)))
        {
            transform.position = hit.point;
        }

        if (Input.GetMouseButton(0))
        {
            Instantiate(RealObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
