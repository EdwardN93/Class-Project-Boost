using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Oscilator : MonoBehaviour
{

    UnityEngine.Vector3 startingPosition;
    [SerializeField] UnityEngine.Vector3 movementVector;
    [SerializeField] float period = 2f;
    float movementFactor;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        StartOscilator();
    }

    void StartOscilator()
    {
        if(period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period; // continually growing over time
        
        const float tau = Mathf.PI * 2;  //constant value of PI * 2 => 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); //Going from -1 to 1
        // Debug.Log(rawSinWave);

        movementFactor = (rawSinWave + 1f) / 2f;    //Recalculate to go from 0 to 1

        UnityEngine.Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
