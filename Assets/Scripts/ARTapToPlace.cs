using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlace : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private GameObject ARObjectToPlace;
    [SerializeField] private TrackableType trackableTypes = TrackableType.FeaturePoint | TrackableType.PlaneWithinPolygon;
    void Update()
    {
        // Check if there are no touches
        if (Input.touchCount <= 0)
            return;

        // Get the first touch
        Touch touch = Input.GetTouch(0);

        // Check if the touch just began and if it's over a UI element
        if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            // Raycast against planes and feature points
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            // Perform the raycast
            if (raycastManager.Raycast(touch.position, hits, trackableTypes))
            {
                // Raycast hits are sorted by distance, so the first one will be the closest hit
                ARRaycastHit hit = hits[0];

                // Instantiate the AR object at the hit position and rotation
                GameObject aRObject = Instantiate(ARObjectToPlace, hit.pose.position, hit.pose.rotation);

                //Set random rotation in Y-Axis
                aRObject.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
            }
        }
    }
}
