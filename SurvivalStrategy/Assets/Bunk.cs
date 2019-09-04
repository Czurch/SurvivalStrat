﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunk : MonoBehaviour
{
  //The Bunk class holds one survivor, it may not be reused until that survivor dies.
  public GameObject bunk_prefab;
  public bool isOccupied;
  public SurvivorHolder occupant;
  // Start is called before the first frame update
  void Start()
    {
      isOccupied = false;
      occupant = null;
    }

  public void Bind(SurvivorHolder s)
  {
    isOccupied = true;
    occupant = s;
    occupant.bunk = this;
  }

  public void Unbind()
  {
    occupant = null;
    isOccupied = false;
  }

  public void CreateNewBunk(Vector3 position, Quaternion rotation)
  {
    Instantiate(bunk_prefab, position, rotation);
  }
}