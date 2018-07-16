module Interface

open Graphics
open System
open Types

let drawInterface world =
  resetCursor()
  clearRows([0; 1; 2])
  Console.WriteLine("Health: {0}", world.player.health)
  Console.WriteLine("State: {0}", world.player.state)
  Console.WriteLine("Turn: {0}", world.turns)