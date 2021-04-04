using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    // public InputAction inputAction;
    private int jumpsRemaining = 2;
    public Vector3 jump;
    public float jumpForce = 2000.0f;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        jump = new Vector3(0.0f, 2.0f, 0.0f);

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText(){
        countText.text = "Count: " + count.ToString();
        if(count >= 36) {
            winTextObject.SetActive(true);
        }
    }

    private void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        // if(Input.GetKeyDown(KeyCode.Space) && (jumpsRemaining > 0)){

        //     rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        //     jumpsRemaining -= 1;
        // }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Pickup")){
            other.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }
    }

    void OnCollisionStay(){
        jumpsRemaining = 2;
    }

    private void OnJump() {
        jumpsRemaining -= 1;
        if (jumpsRemaining >= 1) {
            Debug.Log(jumpsRemaining + " jumps remaining");
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        }
        else {
            Debug.Log("Out of jumps :(");
        }
    }

}
