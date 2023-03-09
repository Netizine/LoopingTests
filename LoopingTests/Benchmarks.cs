using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using FakeItEasy;
using LoopingTests;

namespace LoopingTests;

[MemoryDiagnoser(false)]
public class Benchmarks
{
    [Params(100, 1000, 10000)]
    public int Count { get; set; }

    private static User[] Users;

    [GlobalSetup]
    public void Setup()
    {
        Users = Enumerable.Range(1, Count)
            .Select(x => A.Fake<User>(y => y.WithArgumentsForConstructor(new object[] { $"Fake{x}", $"User{x}", $"fake{x}@faked.com" }))).ToArray();
    }

    [Benchmark]
    public void NormalLoop()
    {
        for (int i = 0; i < Users.Length; i++)
        {
            var user = Users[i];
            user.GetFirstName();
        }
    }

    [Benchmark]
    public void NormalLoopWithSpan()
    {
        var span = Users.AsSpan();
        for (int i = 0; i < span.Length; i++)
        {
            var user = span[i];
            user.GetFirstName();
        }
    }

    [Benchmark]
    public void ForEachLoop()
    {
        for (int i = 0; i < Users.Length; i++)
        {
            var user = Users[i];
            user.GetFirstName();
        }
    }

    [Benchmark]
    public void ForEachLoopWithSpan()
    {
        var span = Users.AsSpan();
        for (int i = 0; i < span.Length; i++)
        {
            var user = span[i];
            user.GetFirstName();
        }
    }

    [Benchmark]
    public void FasterLoop()
    {
        ref var searchSpace = ref MemoryMarshal.GetArrayDataReference(Users);
        for (int i = 0; i < Users.Length; i++)
        {
            var user = Unsafe.Add(ref searchSpace, i);
            user.GetFirstName();
        }
    }

    [Benchmark]
    public void FastestLoop()
    {
        ref var start = ref MemoryMarshal.GetArrayDataReference(Users);
        ref var end = ref Unsafe.Add(ref start, Users.Length);

        while (Unsafe.IsAddressLessThan(ref start, ref end))
        {
            start.GetFirstName();
            start = ref Unsafe.Add(ref start, 1);
        }
    }
}
