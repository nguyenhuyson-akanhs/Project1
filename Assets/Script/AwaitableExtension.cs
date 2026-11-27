using System;
using UnityEngine;

public static class AwaitableExtension
{
    /// <summary>
    /// Đợi cho đến khi điều kiện (condition) trả về true.
    /// </summary>
    public static async Awaitable WaitUntil(Func<bool> condition)
    {
        // Vòng lặp liên tục kiểm tra điều kiện mỗi khung hình
        while (!condition())
        {
            // Đợi đến khung hình tiếp theo (Tương đương yield return null)
            await Awaitable.NextFrameAsync();
        }
    }
}