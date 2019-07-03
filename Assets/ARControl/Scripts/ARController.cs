using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleARCore;

#if UNITY_EDITOR
using input = GoogleARCore.InstantPreviewInput;
#endif

public class ARController : MonoBehaviour
{
    //list contains planes that arcore detected in the current frame
    private List<TrackedPlane> m_newTrackedPlanes = new List<TrackedPlane>();
    public GameObject GridPrefab;

    public GameObject Portal;

    public GameObject ARCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
        //check ARCore session status
        if(Session.Status != SessionStatus.Tracking){
            return;
        }
        //remplis list avec les planes detecteded by arcore on the current frame 
        Session.GetTrackables<TrackedPlane>(m_newTrackedPlanes,TrackableQueryFilter.New);

        for(int i = 0; i<m_newTrackedPlanes.Count; ++i){
            GameObject grid = Instantiate(GridPrefab, Vector3.zero, Quaternion.identity, transform);

            grid.GetComponent<GridVisualiser>().Initialize(m_newTrackedPlanes[i]);
        }
        
        //check if user touched the screen
        Touch touch = new Touch();
        if (Input.touchCount < 1 || Input.GetTouch(0).phase != TouchPhase.Began)
        {
            return;
        }
        
        //check if the user touched any of the planes
        TrackableHit hit;
        if (Frame.Raycast(touch.position.x, touch.position.y, TrackableHitFlags.PlaneWithinPolygon, out hit))
        {
            //place the portal on the top of the plane we touched
            
            //enable the portal
            Portal.SetActive(true);
            
            //create a new anchor
            Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);
            
            //position of the portal same as the hit
            Portal.transform.position = hit.Pose.position;
            Portal.transform.rotation = hit.Pose.rotation;
            
            //portal facing the camera
            Vector3 cameraPosition = ARCamera.transform.position;
            
            //portal rotate only around the Y axis
            cameraPosition.y = hit.Pose.position.y;
            
            //rotate the portal to face the camera
            Portal.transform.LookAt(cameraPosition, Portal.transform.up);

            //arcore understand and keep update the anchor and portal
            Portal.transform.parent = anchor.transform;



        }


    }

}
