using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionFollower : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _distance = 1.0f;

    private bool _isCenter = false;
    private Vector3 velocity = Vector3.zero;

   

    // Update is called once per frame
    void Update()
    {
        if (!_isCenter)
        {
            Vector3 targetPosition = FindTargetPosition();

            MoveTowards(targetPosition);

            if (ReachedPosition(targetPosition))
            {
                _isCenter = true;
            }
        }
        else
        {
            if (dotProduct())
            {
                _isCenter = false;
            }
        }
    }

    private bool dotProduct()
    {
        //Get the angle between where the player look and the position of this object 
        Vector3 toObject = transform.position - _cameraTransform.position;
        float dotProduct = Vector3.Dot(_cameraTransform.forward, toObject.normalized);

        //IF the angle is greater than 30* then return true;
        if(dotProduct < 0.866f)
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    private Vector3 FindTargetPosition()
    {
        //Get the  position in front of the player.
        return _cameraTransform.position + (_cameraTransform.forward * _distance);
    }

    private void MoveTowards(Vector3 targetPosition)
    {

        //transform.position += (targetPosition - transform.position) * 0.025f;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.3f);
        transform.LookAt(_cameraTransform);
        transform.Rotate(new Vector3(0,180,0), Space.Self);
    }

    private bool ReachedPosition(Vector3 targetPosition)
    {
        return Vector3.Distance(targetPosition, transform.position) < 0.1f;
    }




}
