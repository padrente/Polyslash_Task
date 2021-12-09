using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonPusher : MonoBehaviour
{
    [SerializeField] Animator buttonAnim = null;

    [SerializeField] string pushingButtonAnim = "PresingButton";

    [SerializeField] float waitTimer = 1f;
    [SerializeField] bool pauseInteraction;
    public int floorInfo;
    [SerializeField] ElevatorDoorController elevatorDoorObj = null;
    [SerializeField]ElevatorController elevetorController =null;
    

    IEnumerator PauseWhileAnimetePlay()
    {
        pauseInteraction = true;
        yield return new WaitForSeconds(waitTimer);
        pauseInteraction = false;
    }

    public void PushButtonAnimation()
    {
        if(!pauseInteraction || !elevetorController.isMoving)
        {
            buttonAnim.Play(pushingButtonAnim, 0, 0.0f);
            
            if(elevetorController.actualFloor == this.floorInfo)
            {
                elevatorDoorObj.PlayDoorAnimate();
                StartCoroutine(PauseWhileAnimetePlay());
            }
            else if(elevetorController.actualFloor + 1 == this.floorInfo)
            {
                elevetorController.OneFloorUp();
            }
            else if(elevetorController.actualFloor - 1 == this.floorInfo)
            {
                elevetorController.OneFloorDown();
            }
            else if(elevetorController.actualFloor + 1 < this.floorInfo)
            {
                StartCoroutine(elevetorController.ElevetorGoesUp(floorInfo));
            }
            else if(elevetorController.actualFloor - 1 > this.floorInfo)
            {
                StartCoroutine(elevetorController.ElevetorGoesDown(floorInfo));
            }
            else
                Debug.Log("Coś poszło nie tak "+elevetorController.actualFloor+" "+floorInfo);
        }
    }

}
