// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BlueJay.App.Performance;

BenchmarkRunner.Run<EventSuite>(new DebugInProcessConfig());