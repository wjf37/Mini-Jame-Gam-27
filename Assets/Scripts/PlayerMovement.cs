using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 6;
    [SerializeField] GameObject interactionBox;
    [SerializeField] GameObject invisWallText;
    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();    
    }

    void Update(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement);
        //+x = z 90 -x = z -90 +y = z 180 -y = z 0
        if (movement != Vector2.zero)
        {
            FaceDirection();
        }
    }

    private void FaceDirection(){
        //float angle = Mathf.Atan2(movement.x, -movement.y) * Mathf.Rad2Deg;
        //interactionBox.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            if (movement.x > 0)
            {
                interactionBox.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            else
            {
                interactionBox.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            }
        }
        else
        {
            if (movement.y > 0)
            {
                // Up
                interactionBox.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            }
            else
            {
                // Down
                interactionBox.transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(ShowInvisWallText());
        }
    }

    IEnumerator ShowInvisWallText()
    {
        invisWallText.SetActive(true);
        yield return new WaitForSeconds(3);
        invisWallText.SetActive(false);
    }
}
