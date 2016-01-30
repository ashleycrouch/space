using UnityEngine;
using System.Collections;

public class wireManager : MonoBehaviour
{
    public wireButton current;
    
    // Use this for initialization
    void Start()
    {
        current = null;
    }

    public void newCurrent(wireButton ncur)
    {
        current.disconnect();
        current = ncur;
    }
}
