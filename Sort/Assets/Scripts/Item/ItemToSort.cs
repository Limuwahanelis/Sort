using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToSort : MonoBehaviour
{
    public int value;
    public Transform handle;
    private Transform positionToFollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(positionToFollow!=null)
        {
            transform.position = positionToFollow.position;
        }
    }

    public void SetPositionTOFollow(Transform position)
    {
        positionToFollow = position;
    }


}
