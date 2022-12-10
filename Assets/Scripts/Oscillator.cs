using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    // To tweak manually
    // [SerializeField] [Range(0, 1)] float movementFactor;

    Vector3 startingPosition;
    float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // dont compare float with directly 0f
        if (period <= Mathf.Epsilon) { return; }

        const float tau = Mathf.PI * 2; // constant value of 6.283
        float cycles = Time.time / period; // continually growing over time
        float rawSinWave = Mathf.Sin(cycles * tau); // range(-1, 1) 

        movementFactor = (rawSinWave + 1f) / 2f; // range(0, 1)

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;    
    }
}
