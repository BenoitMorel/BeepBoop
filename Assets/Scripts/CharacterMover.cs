using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float jumpSpeed = 10.0f;
    [SerializeField] float gravity = 1.0f;
    float _currentVelocityY = 0.0f;


    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        // horizontal move
        Vector3 relativeMove = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {   
            relativeMove.x = -moveSpeed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            relativeMove.x = moveSpeed * Time.deltaTime;
        }
        transform.localPosition += relativeMove;

        // jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentVelocityY = jumpSpeed;
        }
        transform.localPosition += new Vector3(0.0f, _currentVelocityY * Time.deltaTime, 0.0f);
        _currentVelocityY -= gravity * Time.deltaTime;
        if (transform.localPosition.y < 0.0f)
        {
            _currentVelocityY = 0.0f;
            Vector3 pos = transform.localPosition;
            pos.y = 0.0f;
            transform.localPosition = pos;
        }



    }
}
