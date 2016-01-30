using UnityEngine;
using System.Collections;

public class wireManager : MonoBehaviour
{
    public wireButton current;
    public wireButton check;
    
    // Use this for initialization
    void Start()
    {
        //group = GetComponentsInChildren<wireButton>();
        current = null;
    }

    // Update is called once per frame
    void Update()
    {
        check = GetComponentInChildren<wireButton>();
        if(check.pressed == true)
        {
            current = check;
            check.disconnect();
        }
    }
}
