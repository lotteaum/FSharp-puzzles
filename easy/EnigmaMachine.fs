(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)

(* Lien : https://www.codingame.com/training/easy/encryptiondecryption-of-enigma-machine

  Cliquer sur Resoudre, et l'énoncé se trouve à gauche
  Choisir F# comme langage de programmation *)

open System

let Operation = Console.In.ReadLine()
let pseudoRandomNumber = int(Console.In.ReadLine())
let _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

let mutable rotors = List.empty
for i in 0 .. 3 - 1 do
    let rotor = Console.In.ReadLine()
    rotors <- List.append rotors [rotor]

let message = Console.In.ReadLine()

let find_index elt (l : string) : int = 
    let mutable ind = 0
    while ind<l.Length && l.[ind]<>elt do
        ind <- ind+1
    ind

let encodeMessageWithNumber (_message : string) (n1 : int) (_alphabet : string) (coef:int) = 
    let mutable newString = ""
    for i in 0 .. _message.Length-1 do
        let c = _message.[i]
        let indice = find_index c _alphabet
        let mutable newIndice = (indice+(n1+i)*coef)%_alphabet.Length
        if newIndice<0 then newIndice <- newIndice+26
        let newC = _alphabet.[newIndice]
        
        newString <- sprintf "%s%c" newString newC
    newString

let encodeMessageWithRotor (_message:string) (_alphabet:string) (_rotor:string) =
    (*Pour décoder avec le rotor : inverser les paramètres 2 et 3
    ie l'alphabet et le rotor*)    
    let mutable newString = ""
    for i in 0 .. _message.Length-1 do
        let c = _message.[i]
        let indice = find_index c _alphabet
        let newC = _rotor.[indice]
        newString <- sprintf "%s%c" newString newC
    newString

eprintfn "%A" Operation
(*eprintfn "%A" _alphabet
eprintfn "%A" rotors*)
eprintfn "%A" message

let mutable newMessage = message

if Operation="ENCODE" then
    newMessage <- encodeMessageWithNumber newMessage pseudoRandomNumber _alphabet 1
    for i = 0 to 2 do
        newMessage <- encodeMessageWithRotor newMessage _alphabet rotors.[i]

else
    for i = 2 downto 0 do
        newMessage <- encodeMessageWithRotor newMessage rotors.[i] _alphabet
    newMessage <- encodeMessageWithNumber newMessage pseudoRandomNumber _alphabet -1

(* Write an action using printfn *)
(* To debug: eprintfn "Debug message" *)

printfn "%s" newMessage
