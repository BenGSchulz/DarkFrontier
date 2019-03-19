using UnityEngine;
using System.Collections;

public class BTDecoratorNode: BTNode {
    // returns the single child that this BTNode has
    public BTNode Child { get; set; }

    // constructs the decorator with the behavior tree and the child
    public BTDecoratorNode(BehaviorTree t, BTNode c) : base(t) {
        Child = c;
    }
}
