using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceshipBaseState
{
    public abstract void EnterState(SpaceshipStateControl spaceship);
    public abstract void UpdateState(SpaceshipStateControl spaceship);
}
