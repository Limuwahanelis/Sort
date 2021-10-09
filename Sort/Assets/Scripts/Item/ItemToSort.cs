using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToSort : MonoBehaviour
{
    public bool isUsed=false;
    public int value;
    public Transform handle;
    private Transform _positionToFollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_positionToFollow!=null)
        {
            transform.position = _positionToFollow.position;
        }
    }

    public void SetPositionToFollow(Transform position)
    {
        _positionToFollow = position;
    }


}
