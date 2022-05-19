using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMousePos : MonoBehaviour
{
    Vector3 cv = new Vector3(0, 1, -5);
    Rigidbody rb = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var sv = transform.position;
        sv.y = 1;
        Camera.main.transform.position = sv+cv;

        var mp=Input.mousePosition;
        var x = (int)(mp.x / (Screen.width / 3));
        var y= (int)(mp.y / (Screen.height / 3));

        var vx = Vector3.zero;
        var vy = Vector3.zero;
        var vz = Vector3.zero;
        switch(x)
        {
            case 0:
            vx=new Vector3(-1,0,0);
            break;
            case 2:
            vx=new Vector3(1,0,0);
            break ;
        }
        switch(y)
        {
            case 0:
            vy=new Vector3(0,0,-1);
            break;
            case 2:
            vy=new Vector3(0,0,1);
            break;
        }
        if(Input.GetMouseButtonDown(0))
        {
            vz=new Vector3(0,1000,0);
        }
        rb.AddForce(vx+vy+vz);
    }
}
