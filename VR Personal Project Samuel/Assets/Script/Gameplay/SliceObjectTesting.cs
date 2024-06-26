using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceObjectTesting : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask slicableLayer;
    public Material crossSectionMaterial;
    public float cutForce = 10;
    public Transform debugposition;

    [SerializeField]
    private LayerMask sliceObjectLayer =10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

  /* 
   // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position,out RaycastHit hit, slicableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }
  */
    public void ManualRayCastCall()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, slicableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);
        if(hull != null)
        {
            //Debug.Log(hull);
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetUpSlicedComponent(upperHull,target, velocity);

            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetUpSlicedComponent(lowerHull,target, velocity);
            upperHull.GetComponent<Rigidbody>().AddExplosionForce(cutForce, lowerHull.transform.position, 1);
            //Destroy(target);
        }
       /* Debug.Log("Tried to slice an object");
        Debug.Log(target.name) ;*/
    }

    public void SetUpSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);

    }
    public void SetUpSlicedComponent(GameObject slicedObject, GameObject parentRef, Vector3 normal)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        SlicedObjectLifetime lifetime = slicedObject.AddComponent<SlicedObjectLifetime>();

        lifetime.StartLifeTime();
        slicedObject.layer = sliceObjectLayer;
      
        slicedObject.transform.localPosition = parentRef.transform.TransformPoint(parentRef.transform.localPosition);

        //Debug.Log(slicedObject.transform.position);
        collider.convex = true;
        
        rb.AddForce(normal * 50);
       
    }
   


}
