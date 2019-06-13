(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System
open System.Collections.Generic

type Direction = 
    |NORTH
    |EAST
    |SOUTH
    |WEST

type Priority = 
    |HORAIRE
    |ANTIHORAIRE

let changeDirection dir prio =
    match (dir,prio) with
    | (SOUTH,HORAIRE) -> WEST
    | (WEST,HORAIRE) -> NORTH
    | (NORTH,HORAIRE) -> EAST
    | (EAST,HORAIRE) -> SOUTH
    | (SOUTH,ANTIHORAIRE) -> EAST
    | (WEST,ANTIHORAIRE) -> SOUTH
    | (NORTH,ANTIHORAIRE) -> WEST
    | (EAST,ANTIHORAIRE) -> NORTH

let changePriority prio =
    match prio with
    | HORAIRE -> ANTIHORAIRE
    | ANTIHORAIRE -> HORAIRE

let nextPoint (x,y) direction = 
    match direction with
    | SOUTH -> (x+1,y)
    | WEST -> (x,y-1)
    | NORTH -> (x-1,y)
    | EAST -> (x,y+1)

let token = (Console.In.ReadLine()).Split [|' '|]
let L = int(token.[0])
let C = int(token.[1])

let rows = new List<string>()

for i in 0 .. L - 1 do
    let row = Console.In.ReadLine()
    rows.Add(row)
    ()

let carte = Array2D.init L C (fun i j -> (rows.Item i).[j])
eprintfn " Grille de taille %i x %i " L C
eprintfn "%A" carte

(* Write an action using printfn *)
(* To debug: eprintfn "Debug message" *)

(* Find the position of the case Start, case Suicide and 2 cases Teleporteurs *)

let mutable xStart = -1
let mutable yStart = -1
let mutable xSuicide = -1
let mutable ySuicide = -1
let mutable xTel1 = -1
let mutable yTel1 = -1
let mutable xTel2 = -1
let mutable yTel2 = -1

eprintfn "======= INFOS INITIALES ======="

for i = 0 to L*C-1 do
    let x = i/C
    let y = i%C
    if carte.[x,y]='@' then
        xStart <- x
        yStart <- y
        eprintfn "START : (%i, %i)" x y
    if carte.[x,y]='$' then
        xSuicide <- x
        ySuicide <- y
        eprintfn "SUICIDE : (%i, %i)" x y
    if carte.[x,y]='T' then
        if xTel1= -1 then
            xTel1<-x
            yTel1<-y
            eprintfn "TELEPORTEUR 1 : (%i, %i)" x y
        else
            xTel2<-x
            yTel2<-y
            eprintfn "TELEPORTEUR 2 : (%i, %i)" x y

(* Initialisation *)

eprintfn "======= INITIALISATION ======="

let mutable pointBender = (xStart,yStart)
let mutable direction = SOUTH
let mutable priority = ANTIHORAIRE
let mutable modeCasseur = false

let mutable bouclePresent = false

let listStates = new List<int*int*Direction*Priority*bool>()
listStates.Add((xStart,yStart,direction,priority,modeCasseur))

let listDirections = new List<Direction>()

(* Répétition de la boucle tant que le robot n'atteint pas la cabine suicide ou
qu'une boucle infinie n'ait pas été détectée *)
while (fst(pointBender)<>xSuicide || snd(pointBender)<>ySuicide) && (not bouclePresent) do
    eprintfn "---------------\nPoint courant : %A" pointBender
    let mutable nextPointFree = false
    let mutable nbTestsFailed = 0
    
    while (not nextPointFree) do
        let nextPotentialPoint = nextPoint pointBender direction
        let carac = carte.[fst(nextPotentialPoint), snd(nextPotentialPoint)]
        if (carac = '#') || (carac = 'X' && (not modeCasseur)) then
            if nbTestsFailed = 0 then
                if priority = ANTIHORAIRE then direction <- SOUTH else direction <- WEST
            else
                direction <- changeDirection direction priority
            nbTestsFailed <- nbTestsFailed+1
            eprintfn "Obstacle %A situé sur la case %A : direction -> %A " carac nextPotentialPoint direction
        else
            nextPointFree <- true
            eprintfn "%A + %A -> %A" pointBender direction nextPotentialPoint
            pointBender <- nextPotentialPoint
            listDirections.Add(direction)
    
    let content = carte.[fst(pointBender), snd(pointBender)]
    if (content = 'X') && modeCasseur then
        carte.[fst(pointBender), snd(pointBender)] <- ' '
        listStates.Clear() (* Réinitialisation de la liste des états car la carte a été modifiée *)
        eprintfn "L'obstacle 'X' vient d'être cassé."
    elif content = 'S' then
        direction <- SOUTH
        eprintfn "Direction modifiée directement à : %A" direction
    elif content = 'E' then
        direction <- EAST
        eprintfn "Direction modifiée directement à : %A" direction
    elif content = 'N' then
        direction <- NORTH
        eprintfn "Direction modifiée directement à : %A" direction
    elif content = 'W' then
        direction <- WEST
        eprintfn "Direction modifiée directement à : %A" direction
    elif content = 'B' then
        modeCasseur <- not modeCasseur
        eprintfn "Mode Casseur passé à : %A" modeCasseur
    elif content = 'I' then
        priority <- changePriority priority
        eprintfn "Priorité des directions passé dans le sens %A" priority
    elif content = 'T' then
        if pointBender = (xTel1,yTel1) then
            pointBender <- (xTel2,yTel2)
            eprintfn "Téléportation au point %A" pointBender
        else
            pointBender <- (xTel1,yTel1)
            eprintfn "Téléportation au point %A" pointBender
    
    let state = (fst(pointBender),snd(pointBender),direction,priority,modeCasseur)
    if listStates.Contains(state) then
        bouclePresent <- true
        eprintfn "/!\ /!\ Presence d'une boucle infinie !!!!"
        
    else
        listStates.Add(state)

eprintfn "===== Directions enregistrées ====="
listDirections.ForEach (fun e -> eprintfn "%A" e)

eprintfn "===== Sortie Standard ====="
        
if bouclePresent then printfn "LOOP"
else
    listDirections.ForEach (fun e -> printfn "%A" e)
