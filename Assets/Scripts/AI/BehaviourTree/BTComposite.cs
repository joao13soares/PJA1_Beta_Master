using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTComposite : BTNode
{
    [SerializeField] protected List<BTNode> children;
}
