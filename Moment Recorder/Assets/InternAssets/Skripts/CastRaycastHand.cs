using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastRaycastHand : MonoBehaviour
{
    [Header("References")]

    // Stores the line renderer used in the hand as a pointer
    [SerializeField] private GameObject lineRendererObject;
    private LineRenderer renderLine;

    [Header("Line Properties")]

    // Information about the pointer
    [SerializeField] private float lineWidth = 0.1f;
    [SerializeField] private float lineMaxLength = 1f;

    // Stores the current pointed game object
    private GameObject target;

    // Store the diferent types of tags for the game object in the scene
    [SerializeField] private string startSceneButton;

    // Start is called before the first frame update
    void Start()
    {
        // Line renderer setup
        renderLine = lineRendererObject.GetComponent<LineRenderer>();
        Vector3[] startLinePositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        renderLine.SetPositions(startLinePositions);
        renderLine.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Define each frame how the line should look like
        RenderLine(lineRendererObject.transform.position, lineRendererObject.transform.forward, lineMaxLength);
    }

    // Manage the direction of the current line renderer
    private void RenderLine(Vector3 position, Vector3 direction, float length)
    {
        // Setup raycast hit
        RaycastHit raycastHit;

        // Setup raycast
        Ray lineRendererOut = new Ray(position, direction);

        // Declared an end position variable for the line renderer
        Vector3 endPosition = position + (length * direction);

        // Run the raycast
        if (Physics.Raycast(lineRendererOut, out raycastHit))
        {
            // Update the line render with the new end position
            endPosition = raycastHit.point;

            // Set the Game Object beeing hitted
            target = raycastHit.collider.gameObject;

            //Debug.Log("Hitting " + target.name);

            if (target.CompareTag(startSceneButton))
            {
                // If any of the back triggers is pressed
                if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.Touch)      >= 0.5f ||
                   OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch)    >= 0.5f )
                {
                    target.GetComponentInParent<SceneEnterScript>().PlayScene();
                }
                Debug.Log("Hitting " + startSceneButton);
            }
        }

        // Update the raycast stops
        renderLine.SetPosition(0, position);
        renderLine.SetPosition(1, endPosition);
    }
}
