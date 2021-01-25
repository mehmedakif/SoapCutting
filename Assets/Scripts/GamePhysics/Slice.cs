using UnityEngine;
// Harici olarak EzySlice kutuphanesi kullanildi.
using EzySlice;
    /*********************
    * 
    * Slice sinifi ikiye bolecek olan obje ile kullanilir.
    * Objeden iki adet parca kopyalayip oyuna ekler.
    * Orjinal objeyi siler.
    * 
    **********************/
public class Slice : MonoBehaviour
{
    //cutPointMaterial=Olusturulan parcalarin materyali olarak kullanilir. Sabunun ic kismi olarak gorunur.
    public Material cutPointMaterial;
    public LayerMask mask;
    float volumeOfRight;
    float volumeOfLeft;
    public Vector3 explotion = new Vector3(10f,18f,1);

    void Update()
    {
        Vector3 overlapPosition = transform.position;
        overlapPosition.z = transform.position.z + 2f; ;
        
        //Kesilecek nesneler diziye alinir.
        Collider[] objectsToSlice = Physics.OverlapBox(overlapPosition, new Vector3(2f,0f,0.1f), transform.rotation, mask);

            foreach (Collider nesne in objectsToSlice)
            {
                SlicedHull slicedObject = Cut(nesne.GetComponent<Collider>().gameObject, cutPointMaterial);

                GameObject rightPart = slicedObject.CreateUpperHull(nesne.gameObject, cutPointMaterial);
                GameObject leftPart = slicedObject.CreateLowerHull(nesne.gameObject, cutPointMaterial);
                
                AddCollider(rightPart);
                AddCollider(leftPart);

                volumeOfRight = MeasureVolume(rightPart);
                volumeOfLeft = MeasureVolume(leftPart);

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
        obj.GetComponent<Rigidbody>().AddExplosionForce(1000, transform.position*2, 10);
        //obj.GetComponent<Rigidbody>().AddForce(new Vector3(obj.transform.position.x*2,transform.position.y*5,transform.position.z*2));

    }

    private float MeasureVolume(GameObject item) 
    {
        return item.GetComponent<Collider>().bounds.size.x * item.GetComponent<Collider>().bounds.size.z * item.GetComponent<Collider>().bounds.size.y;
    }

    private SlicedHull Cut(GameObject obj, Material mat = null) 
    {
        if (MeasureVolume(obj) > 1) 
        {
            return obj.Slice(transform.position, transform.up, mat);
        }
        //TODO End of the game will be implemented.
        return obj.Slice(transform.position, transform.forward, mat);
    }
}
