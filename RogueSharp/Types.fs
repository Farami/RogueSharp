module Types

open System


type Position = { x:int; y:int }
let defaultPosition = { x = 0; y = 0 }
type State = Idle | Attack | Open

type Entity =
  {
  currentPosition:Position 
  previousPosition:Position
  health:int
  attack:int
  state:State
  }

type EnemyType = Bat

type Player = Entity
type Enemy = Entity

type TileType = Floor | Door | Wall | Enemy of EnemyType | Exit

type Tile = 
  { position:Position; tile:char; tileType:TileType }
  override this.ToString() = sprintf "%c" this.tile
type World = { tiles:Tile list; player:Player; turns:int; enemies:Enemy list }

type Input = Up | Down | Left | Right | OpenDoor | Attack | Quit

type GameState = World of World | NextLevel | QuitGame

let setPlayer world player = { world with player = player }
let setPlayerState world state = setPlayer world { world.player with state = state }
let setPlayerIdle world = setPlayerState world State.Idle
let setPlayerPosition player newPosition = { player with currentPosition = newPosition; previousPosition = player.currentPosition }