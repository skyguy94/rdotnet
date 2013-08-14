﻿[<NUnit.Framework.TestFixture>]
module RDotNet.ActivePatternsTest

open NUnit.Framework

let engineName = "RDotNetTest"

[<TestFixtureSetUp>]
let setUpFixture () =
   RDotNet.Tests.Helper.SetEnvironmentVariables ()
   let engine = REngine.CreateInstance (engineName)
   engine.Initialize ()

[<TestFixtureTearDown>]
let tearDownFixture () =
   match REngine.GetInstanceFromID (engineName) with
      | null -> ()
      | engine -> engine.Dispose ()

[<SetUp>]
let setUp () =
   match REngine.GetInstanceFromID (engineName) with
      | null -> failwith "engine not found"
      | engine -> engine.Evaluate ("""rm(list=ls())""") |> ignore

[<Test>]
let ``match CharacterVector pattern`` () =
   let engine = REngine.GetInstanceFromID (engineName)
   match engine.Evaluate ("""LETTERS""") with
   | CharacterVector (_) -> ()
   | _ -> Assert.Fail ("not matched")

[<Test>]
let ``match ComplexVector pattern`` () =
   let engine = REngine.GetInstanceFromID (engineName)
   match engine.Evaluate ("""1i""") with
   | ComplexVector (_) -> ()
   | _ -> Assert.Fail ("not matched")

[<Test>]
let ``match IntegerVector pattern`` () =
   let engine = REngine.GetInstanceFromID (engineName)
   match engine.Evaluate ("""1L""") with
   | IntegerVector (_) -> ()
   | _ -> Assert.Fail ("not matched")

[<Test>]
let ``match LogicalVector pattern`` () =
   let engine = REngine.GetInstanceFromID (engineName)
   match engine.Evaluate ("""TRUE""") with
   | LogicalVector (_) -> ()
   | _ -> Assert.Fail ("not matched")

[<Test>]
let ``match NumericVector pattern`` () =
   let engine = REngine.GetInstanceFromID (engineName)
   match engine.Evaluate ("""1""") with
   | NumericVector (_) -> ()
   | _ -> Assert.Fail ("not matched")