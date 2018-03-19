module DotNetCorePrint

let x = 50
let y = 30
let z = x + y

printfn "x: %i" x
printfn "y: %i" y
printfn "z: %i" z

let square x = x * x


let numbers = [1 .. 10]


let squares = List.map square numbers


printfn "N^2 = %A"squares


open System

Console.ReadKey(true)