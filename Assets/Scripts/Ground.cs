using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameManager gm;
   private void OnMouseDown()
   {
    gm.OnClickGround(this.transform);
   // Debug.LogFormat("name {0}", this.name);
   }
}
