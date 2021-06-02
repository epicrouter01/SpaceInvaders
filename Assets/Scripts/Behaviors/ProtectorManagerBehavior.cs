﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorManagerBehavior : MonoBehaviour
{
    [SerializeField] private int protectorLife = 0;
    [SerializeField] private ProtectorBehavior[] protectors = new ProtectorBehavior[4];
    // Start is called before the first frame update
    void Start()
    {
        setUp();
    }

    public void setUp()
    {
        setLifes();
        updateProtectors();
    }

    private void setLifes()
    {
        foreach (ProtectorBehavior protector in protectors)
            protector.Lifes = protectorLife;
    }

    public void updateProtectors()
    {
        foreach (ProtectorBehavior protector in protectors)
            protector.gameObject.SetActive(protector.Lifes > 0);
    }
}