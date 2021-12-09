using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorController : MonoBehaviour
{
    [SerializeField] Animator doorAnim;
    [SerializeField] string doorOpenAnim = "ElevatorOpen";
    [SerializeField] string doorCloseAnim = "ElevatorClose";
    bool doorOpen = false;
    bool isDoorBlock = false;
    public IEnumerator AutomaticDoorclosing()
    {
        doorOpen = true;
        yield return new WaitForSeconds(8);
        CheckDoorStatus();
    }
    IEnumerator DoorStuck()
    {
        yield return new WaitForSeconds(2);
        CheckDoorStatus();
    }
    public void PlayDoorAnimate()
    {
        if(!doorOpen)
        {
            doorAnim.Play(doorOpenAnim, 0, 0.0f);
            StartCoroutine(AutomaticDoorclosing());
        }
    }

    void CheckDoorStatus()
    {
        if(!isDoorBlock)
        {
            doorAnim.Play(doorCloseAnim, 0, 0.0f);
            doorOpen = false;
        }
        else
            StartCoroutine(DoorStuck());
    }
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isDoorBlock = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDoorBlock = false;
        }
    }
}
