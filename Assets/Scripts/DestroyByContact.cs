﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    
	void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Boundary")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }
}
