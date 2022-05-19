using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using TMPro;

public class Sample : MonoBehaviour
{
    public GameObject vrcam;
    Vector3 cv = new Vector3(0, 1, -5);
    Rigidbody rb=null;
    public float speed;
    Color[] cdeta = {
    Color.white,Color.black,Color.gray,
    Color.red,Color.green,Color.blue,
    Color.cyan,Color.magenta,Color.yellow,
    new Color(1,1,0,1)
    };
    Dictionary<string, bool> flag = new Dictionary<string, bool>()
    {
        {"Sphere0",false },
        {"Sphere1",false },
        {"Sphere2",false },
        {"Sphere3",false },
    };
    Dictionary<string, int> param = new Dictionary<string, int>()
    {
        {"power",10},
        {"level",1},
        {"exp",0 }
    };
    bool finish = false;
    System.Random r=new System.Random();
    TextMeshProUGUI message=null;
    TextMeshProUGUI Score=null;
    TextMeshProUGUI High=null;
    int endTime=1000;
    int score = 0;
    int high = 0;


    // Start is called before the first frame update
    void Start()
    { 
        
        rb = GetComponent<Rigidbody>();
        message=GameObject.Find("Massage").GetComponent<TextMeshProUGUI>();
        Score=GameObject.Find("Massage").GetComponent<TextMeshProUGUI>();
        High=GameObject.Find("Massage").GetComponent<TextMeshProUGUI>();
        endTime=(int)Time.time+1000;
        high = PlayerPrefs.GetInt("high");
        if (message)
        {
            Debug.Log("取得できてる");
        }
    }
    public int Level()
    {
        return param["level"];
    }
    public bool Finish()
    {
        return finish;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Finish()) { return; }
        var sv = transform.position;
        sv.y = 1;
        vrcam.transform.position = sv + cv;
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var v=new Vector3((x*param["level"])*speed,0,(y*param["level"])*speed);
        rb.AddForce(v);
    }
    void Fight(GameObject go)
    {
        var er=go.GetComponent<Rigidbody>();
        var pr = GetComponent<Rigidbody>();
        var od=er.GetComponent<Other_Deta>();
        if(er.velocity.magnitude>pr.velocity.magnitude)
        {
            Debug.Log("Player lose");
            param["power"] -= od.Level();
            Addexp(od.Level() / 2);
            if(param["power"]<=0)
            {
                Loss();
            }
        }
        else
        {
            Debug.Log("Player win");
            Addexp(od.Level());
        }
    }
    public void Addexp(int n)
    {
        param["exp"] += n;
        if(param["exp"]>=10)
        {
            Levelup();
        }
    }
    void Levelup()
    {
        if (param["level"] == 5) { return; }
        param["level"]++;
        param["exp"] = 0;
        message.text = "Level" + param["level"];
        TimerStart();
    }
    void TimerStart()
    {
        StartCoroutine(TimerEnd());
    }
    IEnumerator TimerEnd()
    {
        yield return new WaitForSeconds(3);
        message.text = "";
    }
    private void OnCollisionEnter(Collision collision)
    { 
        if (Finish()) { return; }
        if(collision.gameObject.tag=="Other")
        {
            var deta=collision.gameObject.GetComponent<Other_Deta> ();
            Fight(collision.gameObject);
        }
    }
    public void Flag(string flg)
    {
        flag[flg] = true;
        if(Checkflag())
        {
            Win();
        }
    }
    bool Checkflag()
    {
        var f = true;
        foreach(var item in flag)
        {
            if (item.Value == false)
            {
                f=false;
            }
        }
        return f;//フラグをチェックしてすべてtrueかどうか
    }
    void Loss()
    {
        message.text = "GAMEOVER";
        finish = true;
    }
    void Win()
    {
        message.text = "WIN";
        finish=true;
    }
    //void ChengeOther(GameObject ob)
    //{
    //    var deta=ob.GetComponent<Other_Deta>();
    //    var rd=ob.GetComponent<Renderer>();
    //    var d = 1.0f - deta.Value * 0.1f;
    //    var c = deta.Color;
    //    c.a = d;
    //    rd.material.color = c;
    //    rd.material.SetFloat("_Metallic",d);
    //}
    //private void OnTriggerExit(Collider collider)
    //{
    //    if(collider.gameObject.tag=="Other")
    //    {
    //        var go = collider.gameObject;
    //        var h = (Behaviour)collider.gameObject.GetComponent("Halo");
    //        var ex = GameObject.Find("BigExplosion");
    //        ex.transform.position=go.transform.position;
    //        var ps=ex.GetComponent<ParticleSystem>();
    //        ps.Play();
    //        GameObject.Destroy(go);
    //        h.enabled=false;
    //    }
    //}
    //void SetupOther()
    //{
    //    for (int i = 0; i < deta.Length; i++)
    //    {
    //        var ob = GameObject.Find("Sphere"+ i);
    //        Renderer rd=ob.GetComponent<Renderer>();
    //        var d=1.0f-deta[i]*0.1f;
    //        var c=rd.material.color;
    //        c.a = d;
    //        rd.material.color = c;
    //        rd.material.SetFloat("_Metallic",d);
    //    }
    //}
}
