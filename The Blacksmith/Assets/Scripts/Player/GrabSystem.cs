using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSystem : MonoBehaviour
{
    [Header("Grab Settings")]
    public float grabDistance = 3f;           
    public float minHoldDistance = 1f;        
    public float maxHoldDistance = 4f;        
    public float moveSpeed = 10f;             
    public float rotateSpeed = 100f;          
    public float scrollSpeed = 2f;            
    public LayerMask grabbableLayer;          

    [Header("References")]
    public Camera playerCamera;              
    public Transform holdPosition;            

    // Private variables
    private Rigidbody grabbedObject;
    private bool isGrabbing = false;
    private float currentHoldDistance;        

    void Start()
    {
        currentHoldDistance = (minHoldDistance + maxHoldDistance) / 2f; 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))      
        {
            TryGrabObject();
        }
        else if (Input.GetMouseButtonUp(0))   
        {
            if (isGrabbing)
            {
                ReleaseItem();
            }
        }

        if (isGrabbing && grabbedObject != null)
        {
            AdjustHoldDistance();
            MoveObject();
        }
    }

    void TryGrabObject()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); 

        if (Physics.Raycast(ray, out hit, grabDistance, grabbableLayer))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null && !rb.isKinematic)
            {
                grabbedObject = rb;
                isGrabbing = true;

                grabbedObject.useGravity = false;
                
                grabbedObject.drag = 10f;
                grabbedObject.angularDrag = 10f;

                currentHoldDistance = Mathf.Clamp(Vector3.Distance(playerCamera.transform.position, hit.point), minHoldDistance, maxHoldDistance);
            }
        }
    }

    void AdjustHoldDistance()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        currentHoldDistance = Mathf.Clamp(currentHoldDistance + scrollInput * scrollSpeed, minHoldDistance, maxHoldDistance);
    }

    void MoveObject()
    {
        if (grabbedObject == null || holdPosition == null)
            return;

        Vector3 targetPosition = playerCamera.transform.position + playerCamera.transform.forward * currentHoldDistance;
        grabbedObject.velocity = (targetPosition - grabbedObject.position) * moveSpeed;

        if (Input.GetKey(KeyCode.Q))
            grabbedObject.AddTorque(Vector3.up * -rotateSpeed * Time.deltaTime, ForceMode.VelocityChange);
        if (Input.GetKey(KeyCode.E))
            grabbedObject.AddTorque(Vector3.up * rotateSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }

    public void ReleaseItem()
    {
        if (grabbedObject != null)
        {
            grabbedObject.useGravity = true;
            
            grabbedObject.drag = 0f;
            grabbedObject.angularDrag = 0.05f;
            
            grabbedObject.velocity = grabbedObject.velocity * 0.5f; 
        }

        grabbedObject = null;
        isGrabbing = false;
    }

    public bool IsGrabbing()
    {
        return grabbedObject != null;
    }

    public ItemIdentifier GetHeldItem()
    {
        return grabbedObject != null ? grabbedObject.GetComponent<ItemIdentifier>() : null;
    }
}
