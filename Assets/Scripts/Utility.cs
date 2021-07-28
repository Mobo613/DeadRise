using System.Collections;
using System.Collections.Generic;

public static class Utility
{
    /// <summary>
    /// Fisher-Yates Shuffle�㷨
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
            // ��ȡ���������ΧΪi�����鳤��
            int randomIndex = rpng.Next(i, array.Length);
            // ����i���������λ�ý��н���
            T temp = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = temp;
        }

        return array;
    }    
}
