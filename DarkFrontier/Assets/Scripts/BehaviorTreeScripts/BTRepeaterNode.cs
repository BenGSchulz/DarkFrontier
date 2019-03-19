using UnityEngine;
using System.Collections;

public class BTRepeaterNode: BTDecoratorNode {

    public BTRepeaterNode(BehaviorTree t, BTNode child) : base(t, child) {
    }

    public override Result Execute() {
        Debug.Log("Child returned: " + Child.Execute());
        return Result.Running;
    }
}
