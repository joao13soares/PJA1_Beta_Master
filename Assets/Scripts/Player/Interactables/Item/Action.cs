using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Action : MonoBehaviour
{
      [SerializeField]
      public string actionName;
      public abstract void RespectiveAction(GameObject itemObject);
}
