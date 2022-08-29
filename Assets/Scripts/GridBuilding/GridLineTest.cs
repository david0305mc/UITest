using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLineTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer line = gameObject.AddComponent<LineRenderer>();
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, new Vector3(100, 100, 100));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
