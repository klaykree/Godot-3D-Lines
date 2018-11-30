### Instructions:
- Add DrawLine3D.gd to the Godot [AutoLoad list](http://docs.godotengine.org/en/3.0/getting_started/step_by_step/singletons_autoload.html)  
- Call `DrawLine3D.DrawLine`, `DrawLine3D.DrawRay`, or `DrawLine3D.DrawCube` from any script  

OR  

- Add an instance of the `DrawLine3D` to the scene:  
```
var LineDrawer = preload("res://DrawLine3D.gd").new() #In 'global' scope  
...
add_child(LineDrawer) #At some point before calling the desired draw function(s) e.g. in '_ready()'
```
- Call `LineDrawer.DrawLine`, `LineDrawer.DrawRay`, or `LineDrawer.DrawCube` from the script that created the `DrawLine3D` node

`DrawLine` parameters: start point, end point, color, time (optional)  
`DrawRay` parameters: start point, velocity (direction and magnitude), color, time (optional)  
`DrawCube` parameters: start point, half extents (float), color, time (optional)  

#### Examples
- `DrawLine3D.DrawLine(Vector3(0, 0, 0), Vector3(1, 2, 0), Color(0, 0, 1), 1.5)` Draws a line from (0, 0, 0) to (1, 2, 0) colored blue and will last for 1.5 seconds
- `DrawLine3D.DrawCube(Vector3(0, 1, 0), 0.5, Color(1, 0, 0))` Draws a cube at (0, 1, 0) with a half extent size of 0.5 and colored red

![Cube example](/CubeExample.png?raw=true "Cube example")
