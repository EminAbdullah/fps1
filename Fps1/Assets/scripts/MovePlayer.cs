using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public int speed=10;
    const float gravity = 9.8f;
    CharacterController CharacterController;
    Vector3 moveVector;
    // Start is called before the first frame update
    void Start()
    {
        MouseSabitleme();
        CharacterController = GetComponent<CharacterController>();
    }

    private void MouseSabitleme()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        moveVector = new Vector3(Input.GetAxis("Horizontal")*speed*Time.deltaTime, 0,Input.GetAxis("Vertical")*speed*Time.deltaTime);
        moveVector =transform.TransformDirection(moveVector);//locale çevirir
        if (!CharacterController.isGrounded)
        {
            moveVector.y -= gravity * Time.deltaTime;
        }
        CharacterController.Move(moveVector);
    }
}
