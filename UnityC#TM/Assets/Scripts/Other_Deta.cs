using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other_Deta : MonoBehaviour
{
    Dictionary<string, int> param = new Dictionary<string, int>()
    {
        {"power",100 },
        {"level",2 },
        {"exp",0 }
    };
    Color[] levelColor =
    {
        new Color(0f,0f,0.75f),
        new Color(0f,0f,1f),
        new Color(0.25f,0.25f,1f),
        new Color(0.5f,0.5f,1f),
        new Color(0.75f,0.75f,1f),
        new Color(1f,1f,1f),
    };
    public float speed;
    float dv;
    GameObject player=null;
    Sample sss = null;

    void Start()
    {
        dv = 1f * speed;
        player = GameObject.Find("Player");
        sss=player.GetComponent<Sample>();
    }
    public int Level()
    {
        return param["level"];
    }
    public void Addexp(int n)
    {
        param["exp"] += n;
        if (param["exp"] >= 10)
        {
            levelUp();
        }
    }
    void levelUp()
    {
        if (param["level"] == 5) { return; }
        param["level"]++;
        param["exp"] = 0;
        var r=gameObject.GetComponent<Renderer>();
        r.material.color=levelColor[param["level"]];
        dv = param["level"];
    }
    void Fight()
    {
        var pr=player.GetComponent<Rigidbody>();
        var mr = GetComponent<Rigidbody>();
        if (pr.velocity.magnitude > mr.velocity.magnitude)
        {
            Debug.Log(gameObject.name + " lose");
            param["power"] -= sss.Level();
            Addexp(sss.Level()/2);
            if (param["power"] <= 0)
        {
            sss.Flag(this.gameObject.name);
            Destroy(this.gameObject);
        }
        }
        else { Debug.Log(gameObject.name + " Win");
            Addexp(sss.Level());
        }
    }
    private void FixedUpdate()
    {
       if (sss.Finish()) { return; }
       var dp=player.transform.position-transform.position;
        var r=GetComponent<Rigidbody>();
        r.AddForce(dp/10*dv);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (sss.Finish()) { return; }
        if(collision.gameObject.name =="Player")
        {
            var helo = (Behaviour)GetComponent("Halo");
            helo.enabled = true;
            Fight();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (sss.Finish()) { return; }
        if (collision.gameObject.name == "Player")
        {
            var helo = (Behaviour)GetComponent("Halo");
            helo.enabled = false;
        }
    }
    //int _value = 0;
    //public int Value
    //{
    //    get { return _value; }
    //    set { _value = value; }
    //}
    //Color _color = new Color(1, 1, 1);
    //public Color Color
    //{
    //    get
    //    {
    //        return _color;
    //    }
    //    set
    //    {
    //        _color = value;
    //    }
    //}
    //public void AddValue()
    //{
    //    _value += 1;
    //}
}
