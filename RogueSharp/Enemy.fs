module Enemy

open Types

let defaultEnemy = {
  Enemy.currentPosition = defaultPosition
  previousPosition = defaultPosition
  attack = 1
  health = 10
  state = Idle }

let bat = { defaultEnemy with health = 1 }

let createEnemy enemyType position =
    match enemyType with
    | Bat -> { bat with currentPosition = position; previousPosition = position }

let moveEnemies world = world