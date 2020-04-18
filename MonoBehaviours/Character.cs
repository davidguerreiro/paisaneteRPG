﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract class means it cannot be instantiated, and therefore must be inherited by a subclass.
public abstract class Character : MonoBehaviour {

    public int hitPoints;                                   // Current hit points value.
    public int maxHitPoints;                                // Maximun hit points value.

    // Start is called before the first frame update
    void Start() {
            
    }

    // Update is called once per frame
    void Update() {
        
    }
}