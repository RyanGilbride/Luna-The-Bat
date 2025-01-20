using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        // Calculate the repeat width using the BoxCollider's size
        repeatWidth = GetComponent<BoxCollider>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the background has moved past the repeat threshold
        if (transform.position.x < startPos.x - repeatWidth)
        {
            // Move the background seamlessly to the start position + repeatWidth
            transform.position += new Vector3(repeatWidth, 0, 0);
        }
    }
}
