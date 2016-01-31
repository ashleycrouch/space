using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Switches : Task {

    public circuitButton butt;
    public bool desiredState;

    protected override void Update()
    {
        if(butt.GetComponent<Toggle>().isOn == desiredState)
        {
            this.Complete();
        }
        base.Update();
    }

    public override void Reset()
    {
        butt.GetComponent<Toggle>().isOn = !desiredState;
        base.Reset();
    }
}
