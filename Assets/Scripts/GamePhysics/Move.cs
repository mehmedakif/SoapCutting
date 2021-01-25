using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Move : MonoBehaviour
{
    public GameObject referenceObjectForKnife;
    private Touch touch;
    private bool isKnifeFree = true;
    private float speedModifier;
    private float speedModifierSoap;
    private int touchCount;
    //private RaycastHit raydata;
    float x = 2.0f;
    float y = 1.4f;
    float z = 1.3f;
    float refX;
    float refY;
    float refZ;
    // Start is called before the first frame update
    void Start()
    {

        refX = referenceObjectForKnife.transform.position.x;
        refY = referenceObjectForKnife.transform.position.y;
        refZ = referenceObjectForKnife.transform.position.z;
        
        x = refX + 1.4f;
        y = refY + 0.5f;
        z = refZ + 2.0f;

        transform.position = new Vector3(x, y, z) * 1.0f;
        speedModifier = 0.25f;
        speedModifierSoap = 0.15f;
    }

    // Update is called once per frame

    void Update()
    {
            touch = Input.GetTouch(0);
            touchCount = Input.touchCount;
        //Tek parmak ile yapılan hareketler.
        if (touchCount == 1)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    if (isKnifeFree)
                    {
                        transform.position = new Vector3(
                        transform.position.x + touch.deltaPosition.x * speedModifier * Time.deltaTime,
                        transform.position.y,
                        transform.position.z + touch.deltaPosition.y * speedModifier * Time.deltaTime);
                    }
                    else
                    {
                    transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    transform.position.z + touch.deltaPosition.y * speedModifierSoap * Time.deltaTime);

                }

            }

                Vector3 pos = transform.position;
                pos.x = Mathf.Clamp(transform.position.x, -3, 3);
                pos.z = Mathf.Clamp(transform.position.z, -2, 3);
                transform.position = pos;
            }
        //Cift parmak ile yapılan hareketler.
        else if (touchCount == 2 )
        {
            if(isKnifeFree)
            {
                //Açısal hareket.
                if (transform.rotation.z < 10 && transform.rotation.z > -10)
                {
                    transform.Rotate(0.0f, 0.0f, -touch.deltaPosition.x / 30, Space.Self);
                }
                //Yükseklik hareketi
                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y + touch.deltaPosition.y / 10 * speedModifier,
                    transform.position.z);

                }
            }      
        }
    }
    private void OnTriggerStay(Collider other)
    {

        isKnifeFree = false;
        
    }

    private void OnTriggerExit(Collider other)
    {
        isKnifeFree = true;
    }
}
