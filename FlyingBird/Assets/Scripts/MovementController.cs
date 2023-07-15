using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed = 1f;
    Transform trans;
    bool move = true;
    public Camera cam;

    void Start()
    {
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move == true)
        {
            trans.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if (cam != null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(cam.transform.position, 1f);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Stop"))
                {
                    move = false;
                }
            }
        }
    }
}
