using System;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody rb;
    private const float G = 0.006674f;
    private float foceGravity;
    public static List<Gravity> otherObjectList;
    [SerializeField] private bool isPlanet = false;
    [SerializeField] private int orbitSpeed = 1000;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherObjectList == null)
        {
            otherObjectList = new List<Gravity>();
        }
        otherObjectList.Add(this);
        if (!isPlanet)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 diraction = rb.position - otherRb.position;
        float distance = diraction.magnitude;
        foceGravity = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);
        Vector3 foce =  foceGravity * diraction.normalized;
        other.rb.AddForce(foce);
    }

    private void FixedUpdate()
    {
        foreach (Gravity obj in otherObjectList)
        {
            if (obj != this)
            {
                Attract(obj);
            }
        }
    }
}
