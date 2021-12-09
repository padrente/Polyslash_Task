using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCasting : MonoBehaviour
{
    [SerializeField] int rayLength = 5;
    [SerializeField] LayerMask layerMaskInteract;
    [SerializeField] string excludeLayerName = null;
    [SerializeField] Image crosshair = null;
    [SerializeField] KeyCode interactKey = KeyCode.E;

    bool isCrosshairActive;
    bool doOnce;

    private ButtonPusher raycastedObj;

    const string interactableTag = "Buttons";


    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if(Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            if(hit.collider.CompareTag(interactableTag))
            {
                if(!doOnce)
                {
                   raycastedObj = hit.collider.gameObject.GetComponent<ButtonPusher>();
                   CrosshairActive(true);
                }

                isCrosshairActive = true;
                doOnce = true;

                if(Input.GetKeyDown(interactKey))
                    StartCoroutine(raycastedObj.PushButtonAnimation());
            }
        }
        else
        {
            if(isCrosshairActive)
            {
                CrosshairActive(false);
                doOnce = false;
            }
        }
    }

    void CrosshairActive(bool on)
    {
        if(on && !doOnce)
            crosshair.color = Color.red;
        else
        {
            crosshair.color = Color.white;
            isCrosshairActive = false;
        }
    }
}
