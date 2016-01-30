using UnityEngine;
using System.Collections;

public class wireManager : MonoBehaviour
{
    public wireButton current;
    public wireButton[] group;
    
    // Use this for initialization
    void Start()
    {
        group = GetComponentsInChildren<wireButton>();
        current = null;
    }

    // Update is called once per frame
    void Update()
    {
        //if(GetComponentInChildren<wireButton>().pressed)
        //{
        //    current = 
        //}

    }
}
