using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorController : MonoBehaviour
{
    public AudioSource elevatorAudio;
    [SerializeField] AudioClip asOpen;
    [SerializeField] AudioClip asClose;
    [SerializeField] AudioClip asBell;
    [SerializeField] Animator doorAnim;
    [SerializeField] string doorOpenAnim = "ElevatorOpen";
    [SerializeField] string doorCloseAnim = "ElevatorClose";
    public bool doorOpen = false;
    public bool buttonPushed = false;
    bool isDoorBlock = false;
    public IEnumerator AutomaticDoorclosing()
    {
        doorOpen = true;
        yield return new WaitForSeconds(4);
        if(!buttonPushed)
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
            elevatorAudio.PlayOneShot(asBell);
            doorAnim.Play(doorOpenAnim, 0, 0.0f);
            elevatorAudio.PlayOneShot(asOpen);
            StartCoroutine(AutomaticDoorclosing());
        }
    }

    public void CheckDoorStatus()
    {
        if(!isDoorBlock)
        {
            doorAnim.Play(doorCloseAnim, 0, 0.0f);
            elevatorAudio.PlayOneShot(asClose);
            doorOpen = false;
            buttonPushed = false;
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
