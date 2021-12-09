using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public int actualFloor = 9;
    // public int[] floors = new int[9] {1, 2, 3, 4, 5, 6, 7, 8, 9};
    // public string[] floorsAnimDown = new string[8] {"9to8", "8to7", "7to6", "6to5", "5to4", "4to3", "3to2", "2to1" };
    // public string[] floorsAnimUp = new string[8] {"1to2", "2to3", "3to4", "4to5", "5to6", "6to7", "7to8", "8to9"};
    bool isMoving = false;
    int nextFloor;
    string animToPlay;
    [SerializeField] Animator elevetorObj = null;
    [SerializeField] ElevatorDoorController doorControll = null;

    IEnumerator MovingBlock()
    {
        isMoving = true;
        yield return new WaitForSeconds(3.2f);
        isMoving = false;
        doorControll.PlayDoorAnimate();
    } 

    public void OneFloorUp()
    {
        nextFloor = actualFloor + 1;
        animToPlay = actualFloor+"to"+nextFloor;
        elevetorObj.Play(animToPlay, 0, 0.0f);
        actualFloor = nextFloor;
        StartCoroutine(MovingBlock());

    }
    public void OneFloorDown()
    {
        nextFloor = actualFloor - 1;
        animToPlay = actualFloor+"to"+nextFloor;
        elevetorObj.Play(animToPlay, 0, 0.0f);
        actualFloor = nextFloor;
        StartCoroutine(MovingBlock());
    }

    public IEnumerator ElevetorGoesUp(int targetFloor)
    {
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
    }
    public IEnumerator ElevetorGoesDown(int targetFloor)
    {
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
    }
}
