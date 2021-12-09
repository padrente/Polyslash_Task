using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public int actualFloor = 9;
    public bool isMoving = false;
    int nextFloor;
    string animToPlay;
    [SerializeField] Animator elevetorObj = null;
    [SerializeField] ElevatorDoorController doorControll = null;

    IEnumerator MovingBlock()
    {
        isMoving = true;
        yield return new WaitForSeconds(3.2f);    
        doorControll.PlayDoorAnimate();
        isMoving = false;
    } 

    public void OneFloorUp()
    {
        isMoving = true;
        nextFloor = actualFloor + 1;
        animToPlay = actualFloor+"to"+nextFloor;
        elevetorObj.Play(animToPlay, 0, 0.0f);
        actualFloor = nextFloor;
        StartCoroutine(MovingBlock());

    }
    public void OneFloorDown()
    {
        isMoving = true;
        nextFloor = actualFloor - 1;
        animToPlay = actualFloor+"to"+nextFloor;
        elevetorObj.Play(animToPlay, 0, 0.0f);
        actualFloor = nextFloor;
        StartCoroutine(MovingBlock());
    }

    public IEnumerator ElevetorGoesUp(int targetFloor)
    {
        isMoving = true;
        int iLimit = targetFloor - actualFloor;
        for (int i = 0; i < iLimit; i++)
        {
            nextFloor = actualFloor + 1;
            animToPlay = actualFloor+"to"+nextFloor;
            elevetorObj.Play(animToPlay, 0, 0.0f);
            actualFloor = nextFloor;
            yield return new WaitForSeconds(3);
        }
        doorControll.PlayDoorAnimate();
        isMoving = false;
    }
    public IEnumerator ElevetorGoesDown(int targetFloor)
    {
        isMoving = true;
        int iLimit = actualFloor - targetFloor;
        for (int i = 0; i < iLimit; i++)
        { 
            nextFloor = actualFloor - 1;
            animToPlay = actualFloor+"to"+nextFloor;
            elevetorObj.Play(animToPlay, 0, 0.0f);
            actualFloor = nextFloor;
            yield return new WaitForSeconds(3);
        }
        doorControll.PlayDoorAnimate();
        isMoving = false;
    }
}
