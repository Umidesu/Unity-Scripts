using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public GameObject Player;

    public float speed = 2.0f;
    public float jumpHeight = 2.0f;
    public float jumpDuration = 1.0f;

    private float baseTime;

    private float startTime;
    private float defaultHeigth;

    private bool isGrounded = true;

    private string activeLine = "center";

    private Vector3 position;
    private Vector3 direction = new Vector3(0, 0, 1);

    // Start is called before the first frame update
    private void Start()
    {
        // set base location
        position = Player.transform.position;

        baseTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) { turnRight(); }
        if (Input.GetKeyDown(KeyCode.Q)) { turnLeft(); }
        if (Input.GetKeyDown(KeyCode.D)) { tiltRight(); }
        if (Input.GetKeyDown(KeyCode.A)) { tiltLeft(); }

        jump();
        idleMovemnt();
        Player.transform.position = position;
    }

    void turnRight()
    {
        Player.transform.Rotate(new Vector3(0, 90, 0));

        if (direction.z == 1)
        {
            direction.z = 0;
            direction.x = 1;
        } else if (direction.x == 1)
        {
            direction.x = 0;
            direction.z = -1;
        } else if (direction.z == -1)
        {
            direction.z = 0;
            direction.x = -1;
        } else if (direction.x == -1)
        {
            direction.x = 0;
            direction.z = 1;
        }
    }

    void turnLeft()
    {
        Player.transform.Rotate(new Vector3(0, 270, 0));

        if (direction.z == 1)
        {
            direction.z = 0;
            direction.x = -1;
        }
        else if (direction.x == 1)
        {
            direction.x = 0;
            direction.z = 1;
        }
        else if (direction.z == -1)
        {
            direction.z = 0;
            direction.x = 1;
        }
        else if (direction.x == -1)
        {
            direction.x = 0;
            direction.z = -1;
        }
    }

    void tiltRight()
    {
        switch (activeLine)
        {
            case "center":
                activeLine = "right";
                position.x += 1;
                break;
            case "left":
                position.x += 1;
                activeLine = "center";
                break;
        }
    }

    void tiltLeft()
    {
        switch (activeLine)
        {
            case "center":
                activeLine = "left";
                position.x -= 1;
                break;
            case "right":
                position.x -= 1;
                activeLine = "center";
                break;
        }
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;

            defaultHeigth = Player.transform.position.y;
            startTime = Time.time;

            position.y = defaultHeigth + jumpHeight;
        }

        if ((Mathf.Floor(Time.time) - Mathf.Floor(startTime)) == jumpDuration && !isGrounded)
        {
            isGrounded = true;
            position.y = defaultHeigth;
        }
    }

    // Moves character with constant speed
    void idleMovemnt()
    {
        position += direction * speed * (Time.time - baseTime);
        baseTime = Time.time;
    }

    // Basic square function
    public float sqr(float num)
    {
        return num * num;
    }
}