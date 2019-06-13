(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)

(* Lien du puzzle : https://www.codingame.com/training/medium/network-cabling
  Cliquer sur Resoudre, et l'énoncé du problème se trouve à gauche de l'écran.
  Possibilité de choisir F# comme langage. *)

open System
open System.Collections.Generic

let N = int(Console.In.ReadLine())
let listX = new List<int64>()
let listY = new List<int64>()
for i in 0 .. N - 1 do
    let token = (Console.In.ReadLine()).Split [|' '|]
    //eprintfn "%A" token
    let X = int64(token.[0])
    let Y = int64(token.[1])
    listX.Add(X)
    listY.Add(Y)

let arrayX = listX.ToArray()
let arrayY = listY.ToArray()

let sumDist (x:int64) (A:array<int64>) = 
    (* Somme totale des distances des éléments d'une liste par rapport à un entier x *)
    Array.fold (fun s t -> s + abs (t-x)) 0L A

let findMedian (AY:array<int64>) = 
    (* Recherche dichotomique de la valeur médiane d'un tableau d'entiers 64 bits non trié *)
    (* Fonction utilisée pour calculer la longueur totale minimale des cables de ramification à poser *)
    let mutable a = Array.min AY
    let mutable b = Array.max AY
    while (b-a)>=1L do
        let c = (a+b)/2L
        let difL = (sumDist (c-1L) AY) - (sumDist c AY)
        let difR = (sumDist c AY) - (sumDist (c+1L) AY)
        if difL*difR>0L then 
            if difL>0L then a<-c+1L
            elif difL<0L then b<-c-1L
        else
            a<-c
            b<-c
    eprintfn "Finalement : a=%i b=%i" a b
    a

(* Longueur du cable principal à poser *)
let lengthCableX = (Array.max arrayX) - (Array.min arrayX)

(* Write an action using printfn *)
(* To debug: eprintfn "Debug message" *)

(* Affichage en sortie standard du résultat calculé *)

printfn "%i" <| lengthCableX + (sumDist (findMedian arrayY) arrayY)
