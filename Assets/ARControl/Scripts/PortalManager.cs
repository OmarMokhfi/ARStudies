using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject MainCamera;

    public GameObject Sponza;

    private Material[] SponzaMaterials;
    
    void Start()
    {

        SponzaMaterials = Sponza.GetComponent<Renderer>().sharedMaterials;
        
    }

    // Update is called once per frame
    void OnTriggerStay (Collider collider)
    {
        Vector3 camPositionInPortalSpace = transform.InverseTransformPoint(MainCamera.transform.position);
     
        if(camPositionInPortalSpace.y < 0.5f){
            //Disable Stencil
            for (int i = 0; i < SponzaMaterials.Length; i++){ 
                
                SponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Always);
            }
                
        }
        else{
            //Enable Stencil
            for (int i = 0; i < SponzaMaterials.Length; i++){
                
                SponzaMaterials[i].SetInt("_StencilComp", (int) CompareFunction.Equal);
            }

        }
    }
}
