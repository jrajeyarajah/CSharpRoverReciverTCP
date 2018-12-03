### Receiver
Rover’s status is represented by a combination of x and y co-ordinates and a letter presenting one of the four cardinal compass points. The playground is divided up into a grid. An example position might be 0, 0, N, which means the rover is in the bottom left corner and facing North.

The rover listens to port 9999 and executes instructions if there is no obstacle in the way.
The rover receives following letters as instruction ‘L’, ‘R’ and ‘M’. ‘L’ and ‘R’ makes the rover spin 90 degrees left or right on the spot. ‘M’ means move forward one grid point and maintain the same heading.

First instruction sets the playground, it will be the upper-right coordinates of the playground. **10 10** (two numeric value separated by space.)
Second instruction creates the rover, it will be placed in the position and face the direction. 5 5 N (two numeric value and letter separated by space.)
Followed by instruction to move. E.g LMRMLMRMRMLMMRM

Example instructions
8 8
6 7 N
LMLMLMLMM
5 7 E
MMRMMRMRRM