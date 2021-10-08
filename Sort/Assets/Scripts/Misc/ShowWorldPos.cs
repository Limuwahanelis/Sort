using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShowWorldPos : MonoBehaviour
{
    public Vector3 worldP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        worldP = transform.position;
    }
}
