using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlipAttackPoints : MonoBehaviour
{
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
    }

    void Flip()
    {
        Vector3 flipped = transform.position;
        flipped.x *= -1f;

        if (playerMovement.isFlipped)
        {
            transform.position = flipped;
            transform.Rotate(0f, 180f, 0f);
        } 
        else if (!playerMovement.isFlipped)
        {
            transform.position = flipped;
            transform.Rotate(0f, 180f, 0f);
        } 
    }
}
