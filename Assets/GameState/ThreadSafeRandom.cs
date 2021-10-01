using System;
using System.Threading;

/// <summary>
/// Uses ThreadLocal to hold the instance(s) of the base Random so we don't cross thread boundaries.
/// This class is intended to be created once and shared.
/// 
/// This combines Jon Skeet's solution (https://codeblog.jonskeet.uk/2009/11/04/revisiting-randomness/) with the idea of having it be an instance that inherits from Random from Alexey.Petriashev (https://stackoverflow.com/questions/3049467/is-c-sharp-random-number-generator-thread-safe)
/// </summary>
public class ThreadSafeRandom : Random
{
    // These 2 static global values are used to lock and create every other Random instance and keep them from using the same seed.
    private static readonly Random _globalRandom = new Random();
    private static readonly object _globalLock = new object();

    private readonly ThreadLocal<Random> _threadLocalRandom = new ThreadLocal<Random>(NewRandom);

    /// <summary>
    /// Creates a new instance of Random. The seed is derived
    /// from a global (static) instance of Random, rather
    /// than time.
    /// </summary>
    public static Random NewRandom()
    {
        lock (_globalLock)
        {
            return new Random(_globalRandom.Next());
        }
    }

    /// <inheritdoc [cref="System.Random"] />
    public override int Next() => _threadLocalRandom.Value.Next();

    /// <inheritdoc [cref="System.Random"] />
    public override int Next(int maxValue) => _threadLocalRandom.Value.Next(maxValue);

    /// <inheritdoc [cref="System.Random"] />
    public override int Next(int minValue, int maxValue) => _threadLocalRandom.Value.Next(minValue, maxValue);

    /// <inheritdoc [cref="System.Random"] />
    public override void NextBytes(byte[] buffer) => _threadLocalRandom.Value.NextBytes(buffer);

    /// <inheritdoc [cref="System.Random"] />
    public override double NextDouble() => _threadLocalRandom.Value.NextDouble();
}