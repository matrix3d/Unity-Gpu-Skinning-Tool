using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mytest : MonoBehaviour
{
    public GameObject game1;
    public GameObject game2;
    public Button btn;
    public Button btn2;
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(() =>
        {
            init(game1);
        });
        btn2.onClick.AddListener(() =>
        {
            init(game2);
        });
    }

    void init(GameObject g)
    {
        for(var i = 0; i < 1000; i++)
        {
            var a= Instantiate(g);
            a.transform.position = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
