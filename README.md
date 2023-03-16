# MartianNavigator

List of assumptions made to come up with this solution:
  1.	Repeatable initial position (I assume that robots are landed sequentially so they may be landed on the same initial position).
  2.	Final positions become Occupied positions (Instruction which leads a robot to an occupied position is ignored and skipped).
  3.	Initial position of a newly landed robot CANNOT be equal to any of previously landed robots’ final positions, this robot gets status “ImpossibleLanding” and further processing for that robot is stopped and his info is not printed out to the console.
