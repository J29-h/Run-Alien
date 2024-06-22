using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    float player_direction = 0;
    public float xmin, xmax;
    Animator playerAnim;
    Vector3 startPosition;
    bool hasKey = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        player_direction = Input.GetAxisRaw("Horizontal");
        if ((player_direction < 0 && transform.position.x > xmin) || (player_direction > 0 && transform.position.x < xmax))
        {
            playerAnim.SetBool("Is Idle", false);
            playerAnim.SetBool("Is Walking", true);
            transform.Translate(Vector2.right * player_direction * speed * Time.deltaTime);
        }
        else
        {
            playerAnim.SetBool("Is Walking", false);
            playerAnim.SetBool("Is Idle", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            transform.position = startPosition;
        }
        else if (other.gameObject.name.Equals("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
        }

        else if (other.gameObject.name.Equals("Star"))
        {
            Destroy(other.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("EndPoint") && hasKey)
        {
            Debug.Log("Finish");
        }
    }
}
