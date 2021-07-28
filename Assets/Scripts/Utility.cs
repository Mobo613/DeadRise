using System.Collections;
using System.Collections.Generic;

public static class Utility
{
    /// <summary>
    /// Fisher-Yates Shuffle算法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="seed"></param>
    /// <returns></returns>
    public static T[] ShuffleArray<T> (T[] array, int seed)
    {
        System.Random rpng = new System.Random(seed);

        for(int i = 0; i < array.Length - 1; ++i)
        {
            // 获取随机数，范围为i到数组长度
            int randomIndex = rpng.Next(i, array.Length);
            // 将第i个数和随机位置进行交换
            T temp = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = temp;
        }

        return array;
    }    
}
