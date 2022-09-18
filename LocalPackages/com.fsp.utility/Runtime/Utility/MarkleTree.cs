using System;

namespace fsp.utility
{
    public unsafe struct MarkleTree : IDisposable
    {
        private bool isDirty;
        private Unity.Collections.NativeList<int> array;
        private int m_hashCode;

        public int HashCode
        {
            get
            {
                if (isDirty)
                {
                    // 排序
                    QuickSort(0, array.Length - 1);

                    int count = array.Length;
                    int* hashs = stackalloc int[count];
                    for (int i = 0; i < count; i++)
                    {
                        hashs[i] = array[i];
                    }

                    while (count > 1)
                    {
                        int half = (int) Math.Ceiling((double) count / 2); //向上取整 一半个数
                        int index = 0;
                        for (int i = 0; i < half; i++)
                        {
                            int hash1 = hashs[2 * i];
                            int hash2;
                            if (2 * i + 1 >= count)
                                hash2 = hash1;
                            else
                            {
                                hash2 = hashs[2 * i + 1];
                            }

                            int hashthis = Utility.GetStringHashCode($"{hash1}{hash2}");
                            hashs[index++] = hashthis;
                        }
                        count = half;
                    }

                    m_hashCode = hashs[0];
                    isDirty = false;
                }
                return m_hashCode;
            }
        }

        public MarkleTree(int size = 10)
        {
            m_hashCode = 0;
            array = new Unity.Collections.NativeList<int>(size, Unity.Collections.Allocator.Persistent);
            isDirty = false;
        }

        public void Add(int value)
        {
            array.Add(value);
            isDirty = true;
        }

        public void Clear()
        {
            isDirty = false;
            m_hashCode = 0;
            array.Clear();
        }

        public void Dispose()
        {
            array.Dispose();
        }

        private void QuickSort(int left, int right)
        {
            if (left < right)
            {
                int i = Division(left, right);
                //对枢轴的左边部分进行排序
                QuickSort(i + 1, right);
                //对枢轴的右边部分进行排序
                QuickSort(left, i - 1);
            }
        }

        private int Division(int left, int right)
        {
            while (left < right)
            {
                int num = array[left]; //将首元素作为枢轴
                if (num > array[left + 1])
                {
                    array[left] = array[left + 1];
                    array[left + 1] = num;
                    left++;
                }
                else
                {
                    int temp = array[right];
                    array[right] = array[left + 1];
                    array[left + 1] = temp;
                    right--;
                }
            }

            return left; //指向的此时枢轴的位置
        }
    }
}