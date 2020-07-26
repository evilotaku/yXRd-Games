using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TapToThrow : MonoBehaviour
{
    public GameObject itemToThrow; //prefab
    public ARSessionOrigin sessionOrigin;
    GameObject throwItem = null; //instantiated prefab
    public float throwForce = 500;
    public Transform ARCamTransform;
    public bool thrown = false;
    private float startHoldTime;


    // Start is called before the first frame update
    void Start()
    {
        ARCamTransform = Camera.main.transform;
        
    }

    // Update is called once per frame
    void Update()
    {



        checkTouch();
        //placeAfterTouch();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            throwItem = Instantiate(itemToThrow, ARCamTransform.position + ARCamTransform.forward, Quaternion.identity);
            thrown = false;
            Debug.Log("Created ThrowableItem");
        }


    }

    private void FixedUpdate()
    {
        /*
        if (thrown == false)
        {
            ARCamTransform.DetachChildren();
            throwItem.GetComponent<Rigidbody>().AddForce(ARCamTransform.forward.normalized*throwForce);
            thrown= true;
        }
        */
    }


    private void placeAfterTouch()
    {
        if (Input.touchCount > 0)
        {

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                /* var targetPosition = Camera.main.transform.position
                 // Place it 60cm in front of the camera
                 + Camera.main.transform.forward * 0.6f
                 // additionally move it "up" 20cm perpendicular to the view direction
                 + Camera.main.transform.up * 0.2f;cb
                 */
                Vector3 pos = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z + 1);
                throwItem = Instantiate(itemToThrow, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + ARCamTransform.forward, Quaternion.identity);
               
        
                throwItem.GetComponent<Rigidbody>().useGravity = false;

                thrown = false;
                Debug.Log("Created ThrowableItem");

            }
        }
    }

    private void checkTouch()
    {
        if (Input.touchCount > 0)
        {

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startHoldTime = Time.time;
                /* var targetPosition = Camera.main.transform.position
                 // Place it 60cm in front of the camera
                 + Camera.main.transform.forward * 0.6f
                 // additionally move it "up" 20cm perpendicular to the view direction
                 + Camera.main.transform.up * 0.2f;cb
                 */
                Debug.Log("Created ThrowableItem");

            }


            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                throwItem = Instantiate(itemToThrow, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + new Vector3(0,0,0.5f), Quaternion.identity);
                throwItem.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * (Time.time - startHoldTime)*1000f);
                Debug.Log(Time.time - startHoldTime);
                thrown = false;
            }
        }
    }
}

   

