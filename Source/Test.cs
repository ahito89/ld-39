using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	private float startY = 0;
     private float fogStart = 0;
     private float fogEnd = 0;

     void Awake()
     {
         startY = transform.position.y;
         fogStart = RenderSettings.fogStartDistance;
         fogEnd = RenderSettings.fogEndDistance;
     }

     void Update()
     {
         RenderSettings.fogStartDistance = fogStart + transform.position.y - startY;
         RenderSettings.fogEndDistance = fogEnd + transform.position.y - startY;
     }
}
