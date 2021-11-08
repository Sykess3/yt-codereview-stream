using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Source.Models.DataStructures
{
    /// <summary>
    /// Массив на n количество элементов в котором n + 1 элемент стает первым, а предыдущий первый удаляется 
    /// </summary>
    public class ConstraintCircularArray<T> : IEnumerable<T>
    {
        private readonly T[] _array;
        private int _indexOfLastAddedElement;

        public ConstraintCircularArray(int n)
        {
            _array = new T[n];
            FillArrayByDefaultData();
        }

        public void Add(T element)
        {
            if (_indexOfLastAddedElement + 1 == _array.Length) 
                _indexOfLastAddedElement = -1;

            _indexOfLastAddedElement++;
            _array[_indexOfLastAddedElement] = element;
        }

        public T[] GetAll() => (T[])_array.Clone();

        private void FillArrayByDefaultData()
        {
            for (int i = 0; i < _array.Length; i++) 
                _array[i] = default;
        }

        public IEnumerator<T> GetEnumerator() => _array.Cast<T>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _array.GetEnumerator();
    }
}