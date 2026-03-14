# RubixCube Lesson Plan ```(<140 min)```

### Part 1: Setup ```(60 min)```

0) Set up project from git (5 min)

1) Unity editor (15 min)
    - important pannels
        - hierarchy
        - inspector
        - folders
    - editor camera rotations
    - object focus
    - panning

2) Make materials (note make unlit shader) (5 min)

3) Make Rubix Cube Model (15 min)
    - make unit cube
    - Remove Quad colliders
    - duplicate to make full cube

4) Camera (15 min)
    - CameraController Script, rotate on right mouse down

5) Skybox (5 min)
    - disorienting default background.
    - 6 sided skybox
    - Window/Rendering/Lighting

### Break ```(5-10 min)```

### Part 2: Cube ```(40 min)```
1) Raycast -> get the object clicked (10 min)

2) Rotation function (15 min)
    - FromToRotation
    - OverlapBoxNonAlloc
    - AngleAxis * pivot.rotation
    - Set transform rotation directly

3) Animate rotation (15 min)
    - Import PrimeTween
    - _isRotating
    - refactor rotation

### Part 3: Solving ```(15 min)```

1) Move Tracking (10 min)
    - Stack<(Vector2, float)>
    - add tracking code

2) Solving (5 min)
    - Coroutines
    - yield return null

3) Canvas Setup

### Part 4: Polish ```(15 min)```

1) Add rotation setter (5 min)

2) update canvas with rotation options (10 min)

    


