using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterComparingState : SorterState
{
    private float angleError = 2f;
    private float angleBiggerError = 5f;

    Coroutine corV;
    Coroutine corH;
    bool skipV = false;
    bool skipH = false;
    private float normalVerticalSpeed;
    private float targetVerticalAngle;
    private float targetHorizontalAngle;

    private float verticalAngle;
    private float horizontalAngle;

    private ItemToSort item1;
    private ItemToSort item2;

    private Vector3 direction1;
    private Vector3 direction2;

    private Vector3 tmpPos1;
    private Vector3 tmpPos2;

    bool rotateTo1Item;
    bool rotateTo2Item;
    bool lookForward;
    public SorterComparingState(Sorter sorter, ItemToSort item1, ItemToSort item2) : base(sorter)
    {
        direction1 = item1.transform.position - sorter.transform.position;
        direction2 = item2.transform.position - sorter.transform.position;
        this.item1 = item1;
        this.item2 = item2;
        rotateTo1Item = true;
        normalVerticalSpeed = sorter.headVerticalRotationSpeed;
        SetPositionToRotateTo(this.item1.transform.position);

    }
    public override void Update()
    {
        if (rotateTo1Item)
        {
            if(Rotate())
            {
                if (normalVerticalSpeed != 0)
                {

                    sorter.headVerticalRotationSpeed = normalVerticalSpeed;
                }
                rotateTo1Item = false;
                SetPositionToRotateTo(item2.transform.position);
                rotateTo2Item = true;

            }
        }
        if(rotateTo2Item)
        {
            if(Rotate())
            {
                if (normalVerticalSpeed != 0)
                {

                    sorter.headVerticalRotationSpeed = normalVerticalSpeed;
                }
                rotateTo2Item = false;
                lookForward = true;
                SetPositionToRotateTo(sorter.lookForwardPos.position);

            }
        }
        if (lookForward)
        {
            if (Rotate())
            {
                lookForward = false;
                if(normalVerticalSpeed!=0)
                {

                    sorter.headVerticalRotationSpeed = normalVerticalSpeed;
                }
                sorter.ChangeState(new SorterIdleState(sorter));
                sorter.sortingAlgorithm.MoveToNextStep();
            }
        }
    }

    void SetPositionToRotateTo(Vector3 pos)
    {
        tmpPos1 = new Vector3(pos.x, sorter.transform.position.y, pos.z);
        direction1 = tmpPos1 - sorter.transform.position;
        targetVerticalAngle = Vector3.SignedAngle(sorter.transform.forward, direction1, Vector3.up);

        tmpPos2 = new Vector3(sorter.transform.position.x, pos.y, pos.z);
        direction2 = tmpPos2 - sorter.transform.position;
        targetHorizontalAngle = Vector3.SignedAngle(sorter.transform.forward, direction2, Vector3.right)-50f; // without -50f character looks too low

        if (pos != sorter.lookForwardPos.position)
        {
            if (Mathf.Abs(pos.x - sorter.transform.position.x) <= 1f)
            {
                normalVerticalSpeed = sorter.headVerticalRotationSpeed;
                sorter.headVerticalRotationSpeed = sorter.headVerticalRotationSpeed / 2;
            }
        }
        corH= sorter.StartCoroutine(timerHCor());
        corV=sorter.StartCoroutine(timerVCor());
    }

    bool Rotate()
    {
        bool rotated = true;
        if (Mathf.Abs(verticalAngle - targetVerticalAngle) > ((Time.timeScale > 3) ? angleBiggerError : angleError))
        {
            rotated = false;
            if(verticalAngle>targetVerticalAngle)
            {
                verticalAngle -= Time.deltaTime * sorter.headVerticalRotationSpeed;
            }
            else
            {
                verticalAngle += Time.deltaTime * sorter.headVerticalRotationSpeed;
            }
            sorter.anim.SetFloat("vertical angle", verticalAngle);
        }
        else
        {
            verticalAngle = targetVerticalAngle;
        }
        if(skipV)
        {
            verticalAngle = targetVerticalAngle;
        }
        if (Mathf.Abs(horizontalAngle - targetHorizontalAngle) > ((Time.timeScale > 4) ? angleBiggerError : angleError))
        {
            rotated = false;

            if (horizontalAngle > targetHorizontalAngle)
            {
                horizontalAngle -= Time.deltaTime * sorter.headHorizontalRotationSpeed;
            }
            else
            {
                horizontalAngle += Time.deltaTime * sorter.headHorizontalRotationSpeed;
            }
            sorter.anim.SetFloat("horizontal angle", horizontalAngle);
        }
        else
        {
            horizontalAngle = targetHorizontalAngle;
        }
        if(skipH)
        {
            horizontalAngle = targetHorizontalAngle;
        }
        if (rotated)
        {
            sorter.StopCoroutine(corV);
            sorter.StopCoroutine(corH);
            skipH = false;
            skipV = false;
            return true;

        }
        return false;
    }

    IEnumerator timerVCor()
    {
        yield return new WaitForSeconds(1.5f);
        skipV = true;

    }
    IEnumerator timerHCor()
    {
        yield return new WaitForSeconds(1.5f);
        skipH = true;
    }
}
