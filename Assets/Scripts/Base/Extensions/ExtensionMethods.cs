using System.Collections;
using System.Collections.Generic;


namespace Base.Extensions {
    public static class ExtensionMethods {

        /// <summary>
        /// Adds the given item to the list according to standard sorting rules. Do not use on unsorted lists.
        /// </summary>
        /// <param name="list">The list to take values</param>
        /// <param name="item">The item that should be added.</param>
        /// <returns>The index in the list where the item was inserted.</returns>
        public static int AddInPlace<T>(this List<T> list, T item) {
            int index = list.BinarySearch(item);
            if (index < 0) index = ~index; // BinarySearch hacks multiple return values with 2's complement.
            list.Insert(index, item);
            return index;
        }

        /// <summary>
        /// 亂寫的dequeue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue"></param>
        /// <param name="action">要還傳出的物件</param>
        /// <returns>有就回傳true，沒有就false</returns>
        public static bool TryDequeue<T>(this Queue<T> queue, out T item) {
            if(queue.Count > 0) {
                item = queue.Dequeue();
                return true;
            }else {
                item = default(T);
                return false;
            }
        }

    }
}