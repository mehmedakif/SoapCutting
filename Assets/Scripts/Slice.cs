using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Harici olarak EzySlice kutuphanesi kullanildi.
using EzySlice;
using System;
    /*********************
    * 
    * Slice sinifi ikiye bolecek olan obje ile kullanilir.
    * Objeden iki adet parca kopyalayip oyun ekler.
    * Orjinal objeyi siler.
    * 
    **********************/
public class Slice : MonoBehaviour
{
    //cutPointMaterial=Olusturulan parcalarin materyali olarak kullanilir. Odunun ic kismi olarak gorunur.
    public Material cutPointMaterial;
    public GameObject soap;
    public LayerMask mask;
    float volumeOfRight;
    float volumeOfLeft;

    void Update()
    {
        Vector3 overlapPosition = transform.position;
        overlapPosition.z = transform.position.z + 1f; ;
        
        //Kesilecek nesneler diziye alinir.
        Collider[] objectsToSlice = Physics.OverlapBox(overlapPosition, new Vector3(0f,0f,0f), transform.rotation, mask);
            foreach (Collider nesne in objectsToSlice)
            {
                SlicedHull slicedObject = Cut(nesne.GetComponent<Collider>().gameObject, cutPointMaterial);
                GameObject rightPart = slicedObject.CreateUpperHull(nesne.gameObject, cutPointMaterial);
                GameObject leftPart = slicedObject.CreateLowerHull(nesne.gameObject, cutPointMaterial);
                
                AddCollider(rightPart);
                AddCollider(leftPart);

                volumeOfRight = rightPart.GetComponent<Collider>().bounds.size.x * rightPart.GetComponent<Collider>().bounds.size.z * rightPart.GetComponent<Collider>().bounds.size.y;
                volumeOfLeft = leftPart.GetComponent<Collider>().bounds.size.x * leftPart.GetComponent<Collider>().bounds.size.z * leftPart.GetComponent<Collider>().bounds.size.y;
                
            if (volumeOfRight < volumeOfLeft)
                {
                    leftPart.layer = 8;
                    AddRigidBody(rightPart);
                }
            else
                {
                    rightPart.layer = 8;
                    AddRigidBody(leftPart);
                }
            Destroy(nesne.gameObject);
            }

    }

    private void AddCollider(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
    }

    private void AddRigidBody(GameObject obj)
    {
        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
        obj.GetComponent<Rigidbody>().AddExplosionForce(100, obj.transform.position, 180);
    }

    private SlicedHull Cut(GameObject obj, Material mat = null) 
    {
        return obj.Slice(transform.position, transform.up, mat);
    }
}
