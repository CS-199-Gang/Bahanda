using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    private PlugGrabbable plug; 

    public void Occupy(PlugGrabbable plug) {
        this.plug = plug;
    }

    public void Release() {
        plug = null;
    }

    public bool IsOccupied() {
        return plug != null;
    }
}
