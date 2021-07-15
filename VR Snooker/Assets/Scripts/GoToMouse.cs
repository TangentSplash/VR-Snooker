using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMouse : MonoBehaviour
{
    public Camera Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 MouseworldPosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = MouseworldPosition;//new Vector3(MouseworldPosition.x,2,MouseworldPosition.y);
     
    }
}
