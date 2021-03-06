using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindAnimation : MonoBehaviour
{
    public Transform door;
    public Transform startPos;
    public Transform endPos;

    public float speed = 3f;
    public float margin = 0.001f;

    public bool opening;
    public bool moving = true;

    public AudioSource audioSource;

    public void Open()
    {
        door.position = Vector3.MoveTowards(door.position, endPos.position, speed * Time.deltaTime);

        if ((door.position - endPos.position).sqrMagnitude < margin * margin)
        {
            moving = false;
            audioSource.Stop();
        }
    }

    public void Close()
    {
        door.position = Vector3.MoveTowards(door.position, startPos.position, speed * Time.deltaTime);

        if ((door.position - startPos.position).sqrMagnitude < margin * margin)
        {
            moving = false;

            audioSource.Stop();

        }
    }

    private void Update()
    {
        if (!moving)
            return;

        if (opening)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !moving)
        {
            audioSource.Play();

            opening = true;
            moving = true;
        }
    }
    /*
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                opening = false;
                moving = true;
            }

        }*/
}
