using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Move : MonoBehaviour
{
    public GameObject referenceObjectForKnife;
    private Touch touch;
    private float speedModifier;
    private float speedModifierSoap;
    private int touchCount;
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
        speedModifier = 0.015f;
        speedModifierSoap = 0.005f;
    }

    // Update is called once per frame

    void Update()
    {
        //Dokunma durumları.
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchCount = Input.touchCount;
            //Tek parmak ile yapılan hareketler.
            if (touchCount == 1)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    if (transform.gameObject.layer != 8)
                    {
                        transform.position = new Vector3(
                        transform.position.x + touch.deltaPosition.x * speedModifier,
                        transform.position.y,
                        transform.position.z + touch.deltaPosition.y * speedModifier);
                    }
                    else
                    {
                        transform.position = new Vector3(
                        transform.position.x,
                        transform.position.y,
                        transform.position.z + touch.deltaPosition.y * speedModifierSoap);
                    }
                }

                Vector3 pos = transform.position;
                pos.x = Mathf.Clamp(transform.position.x, -3, 3);
                pos.z = Mathf.Clamp(transform.position.z, -2, 3);
                transform.position = pos;
            }
            //Cift parmak ile yapılan hareketler.
            else if (touchCount == 2)
            {
                if (transform.gameObject.layer != 8)
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
    }

    void OnCollisionEnter(Collision collision)
    {
        transform.gameObject.layer = 8;
    }
    private void OnCollisionExit(Collision collision)
    {
        transform.gameObject.layer = 0;
    }
}
